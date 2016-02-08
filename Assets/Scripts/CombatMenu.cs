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
	public Item selectedItem;
	public GameObject itemActions;
	public List<GameObject> buttonList;
	public bool giveItem;

	// Use this for initialization
	void Start () {
		prompt = transform.Find ("Prompt").GetComponent<Text> ();
		instance = this;
		prompt.text = "";
		backButton = transform.Find ("Back").gameObject;
		backButton.SetActive (false);
		itemActions = transform.Find ("ItemActions").gameObject;
		itemActions.SetActive (false);
		buttonList = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public static void Display(PartyMember partyMember){
		instance.activePartyMember = partyMember;
		instance.GetComponent<Canvas> ().enabled = true;
		instance.ShowActions ();
	}

	public static void Hide(){
		instance.itemActions.SetActive (false);
		instance.GetComponent<Canvas> ().enabled = false;
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
		ClearButtonHighlights ();
		backButton.SetActive (false);
		itemActions.SetActive (false);
		selectedItem = null;
		prompt.text = "";
		int i = 0;
		foreach(Ability ability in activePartyMember.abilityList){

			Vector3 newPosition = transform.position;
			newPosition.y += (i * 50);
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
			newPosition.y += (i * 50);
			item.transform.parent = instance.transform;
			item.transform.position = newPosition;
			Button button = item.GetComponent<Button>();

			Item capturedItem = item;

			button.onClick.RemoveAllListeners ();

			button.onClick.AddListener( delegate {
				instance.SelectItem(capturedItem); } );


			button.onClick.AddListener( delegate {
				HighlightButton(button.gameObject); } );

			instance.items.Add (item.gameObject);

			item.transform.parent = GameObject.Find ("CombatMenu").transform;

			instance.transform.Find ("PartyMemberName").GetComponent<Text> ().text = instance.activePartyMember.memberName;

			buttonList.Add (button.gameObject);

			i++;
		}
	}

	public static void HighlightButton(GameObject buttonObject){
		ClearButtonHighlights ();
		buttonObject.transform.Find("Image").GetComponent<Image> ().color = Color.yellow;
	}

	public static void ClearButtonHighlights(){
		foreach(GameObject button in instance.buttonList){
			button.transform.Find("Image").GetComponent<Image> ().color = Color.white;
		}
	}

	public void SelectAbility(Ability ability){
		giveItem = false;
		selectedAbility = ability;
		foreach (GameObject enemy in RoomController.instance.enemies) {
			enemy.GetComponent<Collider2D> ().enabled = true;
		}
		foreach (PartyMember partyMember in PartyMember.members) {
			partyMember.EnableClick ();
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
		itemActions.SetActive (false);
		prompt.text = "select target";
	}

	public void SelectItem(Item item){
		selectedItem = item;
		itemActions.SetActive (true);
	}

	public void UseItem(){
		buttonList.Remove (selectedItem.gameObject);
		selectedItem.Use ();
		itemActions.SetActive (false);

		Hide ();
	}

	public void SelectGiveItem(){
		print ("why u no select?");
		giveItem = true;
		foreach (PartyMember partyMember in PartyMember.members) {
			if(activePartyMember != partyMember) partyMember.EnableClick();
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
		itemActions.SetActive (false);
		prompt.text = "select a party member";

//		Hide ();
	}

	public static void SelectTarget(GameObject target){
		
		instance.backButton.SetActive (false);
		instance.prompt.text = "";

		if (instance.giveItem) {
			GiveItem (target);
		} else {
			BattleController.ExecuteAction (instance.selectedAbility, instance.activePartyMember, target);
		}
		foreach (GameObject enemy in RoomController.instance.enemies) {
			enemy.GetComponent<Collider2D> ().enabled = false;
		}
		foreach (PartyMember partyMember in PartyMember.members) {
			partyMember.DisableClick ();
		}
		Hide ();
	}

	public static void GiveItem(GameObject target){
		instance.itemActions.SetActive (false);

		PartyMember targetMember = target.GetComponent<PartyMember> ();
		targetMember.heldItems.Add (instance.selectedItem);
		instance.activePartyMember.heldItems.Add (instance.selectedItem);
		instance.selectedItem.owner = targetMember;

		EventQueue.AddMessage ("handed it over");

		instance.activePartyMember.turnAvailable = false;

		ClearButtonHighlights ();

		instance.buttonList.Remove (instance.selectedItem.gameObject);

		Hide ();
	}
}
