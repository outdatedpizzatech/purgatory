using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CombatMenu : MonoBehaviour {

	public static CombatMenu instance;
	public static bool displayed = false;
	public PartyMember.ActionDelegate selectedAction;
	public Ability selectedAbility;
	public List<GameObject> buttons = new List<GameObject>();
	public List<GameObject> items = new List<GameObject>();
	public PartyMember activePartyMember;
	public Text prompt;
	public GameObject backButton;

	// Use this for initialization
	void Start () {
		prompt = transform.Find ("Prompt").GetComponent<Text> ();
		instance = this;
		prompt.text = "";
		backButton = transform.Find ("Back").gameObject;
		backButton.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public static void Display(PartyMember partyMember){
		instance.activePartyMember = partyMember;
		instance.transform.parent.GetComponent<Canvas> ().enabled = true;
		instance.ShowActions ();
	}

	public static void Hide(){
		instance.transform.parent.GetComponent<Canvas> ().enabled = false;
		foreach(GameObject button in instance.buttons){
			if (button != null) {
				Destroy (button);
			}
		}
		foreach(GameObject item in instance.items){
			if (item != null) {
				item.transform.position = new Vector3 (9999, 9999, 0);
			}
		}
		BattleController.instance.combatMenuDisplayed = false;
	}

	public void ShowActions(){
		backButton.SetActive (false);
		prompt.text = "";
		int i = 0;
		foreach(Ability ability in activePartyMember.abilityList){

			Vector3 newPosition = transform.position;
			newPosition.y += i * -50;
			GameObject buttonObject = Instantiate (Resources.Load ("ActionButton"), newPosition, Quaternion.identity) as GameObject;
			buttonObject.transform.parent = transform;
			Button button = buttonObject.GetComponent<Button>();
			button.transform.Find ("Text").GetComponent<Text> ().text = ability.Name();

			Ability capturedAbility = ability;

			button.onClick.AddListener( delegate {
				SelectAbility(capturedAbility); } );

			buttons.Add (buttonObject);

			transform.Find ("PartyMemberName").GetComponent<Text> ().text = activePartyMember.memberName;

			i++;
		}

		foreach(Item item in instance.activePartyMember.heldItems){
			Vector3 newPosition = instance.transform.position;
			newPosition.y += i * -50;
			item.transform.parent = instance.transform;
			item.transform.position = newPosition;
			Button button = item.GetComponent<Button>();

			Item capturedItem = item;

			button.onClick.RemoveAllListeners ();

			button.onClick.AddListener( delegate {
				instance.SelectItem(capturedItem); } );

			instance.items.Add (item.gameObject);

			instance.transform.Find ("PartyMemberName").GetComponent<Text> ().text = instance.activePartyMember.memberName;

			i++;
		}
	}

	public void SelectAbility(Ability ability){
		selectedAbility = ability;
		foreach (GameObject enemy in RoomController.instance.enemies) {
			enemy.GetComponent<Collider2D> ().enabled = true;
		}
		foreach (PartyMember partyMember in PartyMember.members) {
			partyMember.GetComponent<Collider2D> ().enabled = true;
		}
		foreach(GameObject button in instance.buttons){
			if (button != null) {
				Destroy (button);
			}
		}
		foreach(GameObject item in instance.items){
			if (item != null) {
				item.transform.position = new Vector3 (9999, 9999, 0);
			}
		}
		backButton.SetActive (true);
		prompt.text = "select target";
	}

	public void SelectItem(Item item){
		print ("using item!");
		item.Use ();
		Hide ();
	}

	public static void SelectTarget(GameObject target){
		instance.backButton.SetActive (false);
		instance.prompt.text = "";
		BattleController.ExecuteAction (instance.selectedAbility, instance.activePartyMember, target);
		foreach (GameObject enemy in RoomController.instance.enemies) {
			enemy.GetComponent<Collider2D> ().enabled = false;
		}
		foreach (PartyMember partyMember in PartyMember.members) {
			partyMember.GetComponent<Collider2D> ().enabled = false;
		}
		Hide ();
	}
}
