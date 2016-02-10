using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelUpHUD : MonoBehaviour {

	public static LevelUpHUD instance;
	public static PartyMember selectedPartyMember;
	public static int selectedIndex;
	public static LevelUpStruct selectedLevelUpStruct;
	public Vector3 confirmButtonPosition;
	public static List<GameObject> buttonList;
	public Canvas canvas;
	private ActionButton selectedButton;


	// Use this for initialization
	void Start () {
		instance = this;
		selectedPartyMember = null;
		canvas = GetComponent<Canvas> ();
		confirmButtonPosition = instance.transform.Find ("ConfirmLevelUp").position;
		instance.transform.Find ("ConfirmLevelUp").position = new Vector3(9999, 9999, 0);
		buttonList = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Show(){
		GameController.EnterLevelUpMenu ();
		Prompt.SetText("Select a party member");
	}

	public void Close(){
		ObjectTooltip.Hide ();
		GameController.ExitLevelUpMenu ();
		PartyMember.UnselectAll ();
		instance.transform.Find ("ConfirmLevelUp").position = new Vector3(9999, 9999, 0);
		Prompt.Clear ();

		DestroyButtons ();
	}

	public static void DestroyButtons(){
		foreach(GameObject button in buttonList){
			Destroy (button);
		}
		buttonList = new List<GameObject> ();
	}

	public static void ShowAbilitiesForPartyMember(PartyMember partyMember){
		ObjectTooltip.Hide ();
		DestroyButtons ();
		Prompt.SetText("Select a level up");
		selectedPartyMember = partyMember;
		int i = 0;
		foreach(LevelUpStruct levelUpStruct in partyMember.job.LevelUps()){
			GameObject buttonObject = Instantiate (Resources.Load ("ActionButton"), Vector3.zero, Quaternion.identity) as GameObject;
			ActionButton actionButton = buttonObject.GetComponent<ActionButton> ();
			buttonObject.transform.parent = instance.transform;
			buttonObject.transform.position = new Vector3(70 + (i * 70), 400, buttonObject.transform.position.z);
			buttonObject.transform.localScale = new Vector3 (1f, 1f, 1);
			Button button = buttonObject.GetComponent<Button>();
			actionButton.sprite = Resources.Load<Sprite> ("Sprites/" + levelUpStruct.spriteName);
			if (selectedPartyMember.HasLevelUpSlot (i)) {
				actionButton.startText = "LEARNED";
			}else{
				actionButton.startText = levelUpStruct.cost.ToString();
			}

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
		buttonObject.GetComponent<ActionButton> ().Highlight ();
		instance.selectedButton = buttonObject.GetComponent<ActionButton> ();
	}

	public static void ClearButtonHighlights(){
		foreach(GameObject button in buttonList){
			button.GetComponent<ActionButton> ().UnHighlight ();
		}
		instance.selectedButton = null;
	}

	public static void ConfirmLevelUp(LevelUpStruct levelUpStruct, int inputSelectedIndex){
		selectedIndex = inputSelectedIndex;
		selectedLevelUpStruct = levelUpStruct;
		ObjectTooltip.Show (levelUpStruct);
		if(levelUpStruct.cost <= PartyMember.currency && !selectedPartyMember.HasLevelUpSlot (selectedIndex)){
			instance.transform.Find ("ConfirmLevelUp").position = instance.confirmButtonPosition;
		}else{
			instance.transform.Find ("ConfirmLevelUp").position = new Vector3(9999, 9999, 0);
		}
		Prompt.Clear ();
	}

	public void ExecuteLevelUp(){
		ObjectTooltip.Hide ();
		selectedLevelUpStruct.performer (selectedPartyMember);
		selectedPartyMember.UpdateLevelUpSlot (selectedIndex);
		transform.Find ("ConfirmLevelUp").position = new Vector3(9999, 9999, 0);
		Prompt.Clear ();
		PartyMember.currency -= selectedLevelUpStruct.cost;
		selectedButton.SetText ("LEARNED");
		ClearButtonHighlights ();
	}
}
