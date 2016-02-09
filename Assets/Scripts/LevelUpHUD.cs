﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelUpHUD : MonoBehaviour {

	public static LevelUpHUD instance;
	public static PartyMember selectedPartyMember;
	public static int selectedIndex;
	public static LevelUpStruct selectedLevelUpStruct;
	public Vector3 confirmButtonPosition;
	public Text prompt;
	public static List<GameObject> buttonList;
	public Canvas canvas;


	// Use this for initialization
	void Start () {
		instance = this;
		selectedPartyMember = null;
		canvas = GetComponent<Canvas> ();
		confirmButtonPosition = instance.transform.Find ("ConfirmLevelUp").position;
		prompt = transform.Find ("Prompt").GetComponent<Text>();
		prompt.text = "";
		instance.transform.Find ("ConfirmLevelUp").position = new Vector3(9999, 9999, 0);
		buttonList = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Show(){
		GameController.EnterLevelUpMenu ();
		prompt.text = "Select a party member";
	}

	public void Close(){
		GameController.ExitLevelUpMenu ();
		PartyMember.UnselectAll ();
		instance.transform.Find ("ConfirmLevelUp").position = new Vector3(9999, 9999, 0);
		prompt.text = "";

		DestroyButtons ();
	}

	public static void DestroyButtons(){
		foreach(GameObject button in buttonList){
			Destroy (button);
		}
		buttonList = new List<GameObject> ();
	}

	public static void ShowAbilitiesForPartyMember(PartyMember partyMember){
		DestroyButtons ();
		instance.prompt.text = "Select a level up";
		selectedPartyMember = partyMember;
		int i = 0;
		foreach(LevelUpStruct levelUpStruct in partyMember.job.LevelUps()){
			GameObject buttonObject = Instantiate (Resources.Load ("LevelUpButton"), Vector3.zero, Quaternion.identity) as GameObject;
			buttonObject.transform.parent = instance.transform;
			buttonObject.transform.position = new Vector3(100 + (i * 70), 500, buttonObject.transform.position.z);
			buttonObject.transform.localScale = new Vector3 (1f, 1f, 1);
			Button button = buttonObject.GetComponent<Button>();
			button.transform.Find ("Text").GetComponent<Text> ().text = levelUpStruct.name;

			LevelUpStruct capturedLevelUpStruct = levelUpStruct;
			int capturedIndex = i;

			button.onClick.AddListener( delegate {
				ConfirmLevelUp(capturedLevelUpStruct, capturedIndex); } );
			button.onClick.AddListener( delegate {
				HighlightButton(button.gameObject); } );

			buttonList.Add (button.gameObject);
			i++;
		}
	}

	public static void HighlightButton(GameObject buttonObject){
		ClearButtonHighlights ();
		buttonObject.GetComponent<Image> ().color = Color.yellow;
	}

	public static void ClearButtonHighlights(){
		foreach(GameObject button in buttonList){
			button.GetComponent<Image> ().color = Color.white;
		}
	}

	public static void ConfirmLevelUp(LevelUpStruct levelUpStruct, int inputSelectedIndex){
		selectedIndex = inputSelectedIndex;
		selectedLevelUpStruct = levelUpStruct;
		if(levelUpStruct.cost <= PartyMember.currency && !selectedPartyMember.HasLevelUpSlot (selectedIndex)){
			instance.transform.Find ("ConfirmLevelUp").position = instance.confirmButtonPosition;
		}else{
			instance.transform.Find ("ConfirmLevelUp").position = new Vector3(9999, 9999, 0);
		}
		instance.prompt.text = selectedLevelUpStruct.name + ": " + selectedLevelUpStruct.description + ". Cost: " + selectedLevelUpStruct.cost;
	}

	public void ExecuteLevelUp(){
		selectedLevelUpStruct.performer (selectedPartyMember);
		selectedPartyMember.UpdateLevelUpSlot (selectedIndex);
		transform.Find ("ConfirmLevelUp").position = new Vector3(9999, 9999, 0);
		instance.prompt.text = "";
		PartyMember.currency -= selectedLevelUpStruct.cost;
		ClearButtonHighlights ();
	}
}
