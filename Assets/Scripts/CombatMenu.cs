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
	public GameObject backButton;
	public Item selectedItem;
	public GameObject itemActions;
	public bool giveItem;
	private Button attackButton;
	private Button abilityButton;
	private Button itemButton;

	// Use this for initialization
	void Start () {
		instance = this;
		backButton = transform.Find ("Back").gameObject;
		backButton.SetActive (false);
		itemActions = transform.Find ("ItemActions").gameObject;
		itemActions.SetActive (false);
		attackButton = transform.Find ("AttackButton").GetComponent<Button> ();
		abilityButton = transform.Find ("AbilityButton").GetComponent<Button> ();
		itemButton = transform.Find ("ItemButton").GetComponent<Button> ();
		attackButton.gameObject.SetActive (false);
		abilityButton.gameObject.SetActive (false);
		itemButton.gameObject.SetActive (false);
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
		instance.attackButton.gameObject.SetActive (false);
		instance.abilityButton.gameObject.SetActive (false);
		instance.itemButton.gameObject.SetActive (false);

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
		instance.buttons.Clear ();
		BattleController.instance.combatMenuDisplayed = false;
	}

	public static void HideSubActions(){
		print ("clearing buttons!");
		foreach(GameObject button in instance.buttons){
			print ("button iteration");
			if (button != null) {
				print ("clear");
				Destroy (button);
			}
		}
		instance.buttons.Clear ();
	}

	public void ShowActions(){
		foreach(PartyMember partyMember in PartyMember.members){
			partyMember.HideOverlay ();
		}
		activePartyMember.ShowOverlay ();
		attackButton.onClick.RemoveAllListeners ();

		attackButton.onClick.AddListener (delegate {
			SelectAbility (activePartyMember.abilityList[0]);
		});

		attackButton.gameObject.SetActive (true);
		abilityButton.gameObject.SetActive (true);
		itemButton.gameObject.SetActive (true);
	}

	public void ShowAbilities(){
		HideSubActions ();
		ClearButtonHighlights ();
		backButton.SetActive (false);
		itemActions.SetActive (false);
		selectedItem = null;	
		Prompt.SetText ("Select an ability");

		int i = 0;
		int xIncrement = 0;
		foreach(Ability ability in activePartyMember.abilityList){
			if (i != 0) {
				if (xIncrement > 3)
					xIncrement = 0;
				float x = 55 + ((xIncrement) * 75);
				float y = 225 + (Mathf.Floor((i-1)/4) * 75);
				Vector3 newPosition = new Vector3(x, y, 0);
				GameObject buttonObject = Instantiate (Resources.Load ("ActionButton"), newPosition, Quaternion.identity) as GameObject;
				Sprite sprite = Resources.Load<Sprite>("Sprites/" + ability.SpriteName ());
				buttonObject.GetComponent<ActionButton> ().sprite = sprite;
				buttonObject.transform.parent = transform;
				Button button = buttonObject.GetComponent<Button> ();
				button.transform.localScale = new Vector3 (1, 1, 1);
				Ability capturedAbility = ability;

				button.onClick.AddListener (delegate {
					SelectAbility (capturedAbility);
				});

				buttons.Add (buttonObject);
				xIncrement++;

			}
			i++;
		}

	}



	public void ShowItems(){
		HideSubActions ();
		ClearButtonHighlights ();
		backButton.SetActive (false);
		itemActions.SetActive (false);
		selectedItem = null;	
		Prompt.SetText ("Select an item");

		int i = 0;

		foreach(Item item in instance.activePartyMember.heldItems){
			Vector3 newPosition = new Vector3 (130, 230 + (i * 75), 0);

			Item capturedItem = item;

			GameObject buttonObject = Instantiate (Resources.Load ("ActionButton"), newPosition, Quaternion.identity) as GameObject;
			buttons.Add (buttonObject);
			ActionButton actionButton = buttonObject.GetComponent<ActionButton> ();
			actionButton.sprite = capturedItem.sprite;
			buttonObject.transform.parent = transform;
			Button button = buttonObject.GetComponent<Button> ();
			button.transform.localScale = new Vector3 (1, 1, 1);

			button.onClick.RemoveAllListeners ();

			button.onClick.AddListener( delegate {
				instance.SelectItem(capturedItem); } );

			button.onClick.AddListener( delegate {
				HighlightButton(actionButton); } );


			i++;
		}

	}

	public static void HighlightButton(ActionButton actionButton){
		ClearButtonHighlights ();
		actionButton.Highlight ();
	}

	public static void ClearButtonHighlights(){
		foreach(GameObject button in instance.buttons){
			button.GetComponent<ActionButton> ().UnHighlight ();
		}
	}

	public void DoAttack(){
		HideSubActions ();
		SelectAbility (activePartyMember.abilityList [0]);
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
		instance.buttons.Clear ();
//		backButton.SetActive (true);
		itemActions.SetActive (false);
		Prompt.SetText ("Select a target");
	}

	public void SelectItem(Item item){
		selectedItem = item;
		itemActions.SetActive (true);
	}

	public void UseItem(){
		buttons.Remove (selectedItem.gameObject);
		selectedItem.Use ();
		itemActions.SetActive (false);

		Hide ();
	}

	public void SelectGiveItem(){
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
		instance.buttons.Clear();

		Prompt.SetText("Select a party member");

//		Hide ();
	}

	public static void SelectTarget(GameObject target){
		
		instance.backButton.SetActive (false);
		Prompt.SetText ("Select a target");

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
		instance.activePartyMember.RemoveItem (instance.selectedItem);
		targetMember.AddItem (instance.selectedItem);

		EventQueue.AddMessage ("handed it over");

		instance.activePartyMember.turnAvailable = false;

		ClearButtonHighlights ();

		instance.buttons.Remove (instance.selectedItem.gameObject);

		Hide ();
	}
}
