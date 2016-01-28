using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CombatMenu : MonoBehaviour {

	public GameObject target;
	public static CombatMenu instance;
	public static bool displayed = false;
	public PartyMember.ActionDelegate selectedAction;
	public List<GameObject> buttons = new List<GameObject>();

	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public static void Display(PartyMember partyMember){
		instance.transform.parent.GetComponent<Canvas> ().enabled = true;
		int i = 0;
		foreach(PartyMember.ActionDelegate actionDelegate in partyMember.Actions()){
			
			Vector3 newPosition = instance.transform.position;
			newPosition.y += i * -50;
			GameObject buttonObject = Instantiate (Resources.Load ("ActionButton"), newPosition, Quaternion.identity) as GameObject;
			buttonObject.transform.parent = instance.transform;
			Button button = buttonObject.GetComponent<Button>();
			button.transform.Find ("Text").GetComponent<Text> ().text = actionDelegate.Method.Name;

			PartyMember.ActionDelegate capturedDelegate = actionDelegate;

			button.onClick.AddListener( delegate {
				instance.SelectAction(capturedDelegate); } );

			instance.buttons.Add (buttonObject);

			instance.transform.Find ("PartyMemberName").GetComponent<Text> ().text = partyMember.memberName;

//			button.onClick.AddListener (delegate {
//				print ("selected index:" + i);
//			});
//
			i++;
		}
	}

	public static void Hide(){
		instance.transform.parent.GetComponent<Canvas> ().enabled = false;
		foreach(GameObject button in instance.buttons){
			Destroy (button);
		}
		BattleController.instance.combatMenuDisplayed = false;
	}

	public void SelectAction(PartyMember.ActionDelegate a){
		selectedAction = a;
		print ("selected action is " + a.Method.Name);
		foreach (GameObject enemy in RoomController.instance.enemies) {
			enemy.GetComponent<Collider2D> ().enabled = true;
		}
		foreach (PartyMember partyMember in PartyMember.members) {
			partyMember.GetComponent<Collider2D> ().enabled = true;
		}
		EventQueue.AddMessage ("select target");
	}

	public static void SelectTarget(GameObject target){
		instance.selectedAction(target);
		foreach (GameObject enemy in RoomController.instance.enemies) {
			enemy.GetComponent<Collider2D> ().enabled = false;
		}
		foreach (PartyMember partyMember in PartyMember.members) {
			partyMember.GetComponent<Collider2D> ().enabled = false;
		}
		Hide ();
	}
}
