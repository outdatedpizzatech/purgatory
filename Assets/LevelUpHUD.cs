using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelUpHUD : MonoBehaviour {

	public static LevelUpHUD instance;
	public static PartyMember selectedPartyMember;

	// Use this for initialization
	void Start () {
		instance = this;
		selectedPartyMember = null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Show(){
		GameController.EnterLevelUpMenu ();
		EventQueue.AddMessage ("select a party member");

		foreach (PartyMember partyMember in PartyMember.members) {
			partyMember.GetComponent<Collider2D> ().enabled = true;
		}
	}

	public static void ShowAbilitiesForPartyMember(PartyMember partyMember){
		selectedPartyMember = partyMember;
		int i = 0;
		foreach(LevelUpStruct levelUpStruct in partyMember.job.LevelUps()){
			GameObject buttonObject = Instantiate (Resources.Load ("LevelUpButton"), Vector3.zero, Quaternion.identity) as GameObject;
			buttonObject.transform.parent = instance.transform;
			buttonObject.transform.position = new Vector3(100 + (i * 70), 500, buttonObject.transform.position.z);
			buttonObject.transform.localScale = new Vector3 (1f, 1f, 1);
			Button button = buttonObject.GetComponent<Button>();
			button.transform.Find ("Text").GetComponent<Text> ().text = levelUpStruct.name;

			LevelUpStruct.Perform capturedPerform = levelUpStruct.performer;
			int capturedIndex = i;

			button.onClick.AddListener( delegate {
				ExecuteLevelUp(capturedPerform, capturedIndex); } );

			i++;
		}
	}

	public static void ExecuteLevelUp(LevelUpStruct.Perform perform, int selectedSlot){
		perform (selectedPartyMember);
		selectedPartyMember.UpdateLevelUpSlot (selectedSlot);
	}
}
