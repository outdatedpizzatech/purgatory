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
	public Item selectedItem;
	public bool giveItem;
	private Button attackButton;
	private Button abilityButton;
	private Button itemButton;
	private enum SelectionModes
	{
		ActionTarget,
		ItemTarget,
		None
	}
	private SelectionModes selectionMode;

	// Use this for initialization
	void Start () {
		ObjectTooltip.Hide ();
		selectionMode = SelectionModes.None;
		instance = this;
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
		ObjectTooltip.Hide ();
		instance.selectionMode = SelectionModes.None;
		instance.attackButton.gameObject.SetActive (false);
		instance.abilityButton.gameObject.SetActive (false);
		instance.itemButton.gameObject.SetActive (false);

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
		instance.selectionMode = SelectionModes.None;
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
		ObjectTooltip.Hide ();
		selectionMode = SelectionModes.None;
		itemButton.gameObject.SetActive (true);
		attackButton.gameObject.SetActive (true);
		abilityButton.gameObject.SetActive (true);
		ClearActionButtonHighlights();

		foreach(PartyMember partyMember in PartyMember.members){
			partyMember.HideOverlay ();
		}

		activePartyMember.ShowOverlay ();

		attackButton.onClick.RemoveAllListeners ();
		abilityButton.onClick.RemoveAllListeners ();
		itemButton.onClick.RemoveAllListeners ();

		itemButton.onClick.AddListener (delegate {
			ClearActionButtonHighlights();
		});

		attackButton.onClick.AddListener (delegate {
			ClearActionButtonHighlights();
		});

		abilityButton.onClick.AddListener (delegate {
			ClearActionButtonHighlights();
		});

		attackButton.onClick.AddListener (delegate {
			HighlightButton (attackButton.GetComponent<ActionButton>());
		});

		abilityButton.onClick.AddListener (delegate {
			HighlightButton (abilityButton.GetComponent<ActionButton>());
		});

		itemButton.onClick.AddListener (delegate {
			HighlightButton (itemButton.GetComponent<ActionButton>());
		});

		attackButton.onClick.AddListener (delegate {
			SelectAbility (activePartyMember.abilities[0]);
		});

		abilityButton.onClick.AddListener (delegate {
			this.ShowAbilities();
		});

		itemButton.onClick.AddListener (delegate {
			this.ShowItems();
		});
	}

	public void ShowAbilities(){
		ObjectTooltip.Hide ();
		selectionMode = SelectionModes.None;
		HideSubActions ();
		ClearButtonHighlights ();
		selectedItem = null;	
		Prompt.SetText ("Select an ability");

		int i = 0;
		int xIncrement = 0;
		foreach(Ability ability in activePartyMember.abilities){
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
		ObjectTooltip.Hide ();
		selectionMode = SelectionModes.None;
		HideSubActions ();
		ClearButtonHighlights ();
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
				instance.SelectItem(buttonObject, capturedItem); } );

			button.onClick.AddListener( delegate {
				HighlightButton(actionButton); } );


			i++;
		}

		ShowWeapon ();

	}

	public void ShowWeapon(){
		Item capturedItem = instance.activePartyMember.equipment [ItemTypes.Weapon];

		if (capturedItem != null) {

			Vector3 newPosition = new Vector3 (130, 230 + (3 * 75), 0);


			GameObject buttonObject = Instantiate (Resources.Load ("ActionButton"), newPosition, Quaternion.identity) as GameObject;
			buttons.Add (buttonObject);
			ActionButton actionButton = buttonObject.GetComponent<ActionButton> ();
			actionButton.startText = "WEAPON";
			actionButton.sprite = capturedItem.sprite;
			buttonObject.transform.parent = transform;
			Button button = buttonObject.GetComponent<Button> ();
			button.transform.localScale = new Vector3 (1, 1, 1);

			button.onClick.RemoveAllListeners ();

			button.onClick.AddListener (delegate {
				instance.SelectItem (buttonObject, capturedItem);
			});

			button.onClick.AddListener (delegate {
				HighlightButton (actionButton);
			});

		}
	}

	public static void HighlightButton(ActionButton actionButton){
		ClearButtonHighlights ();
		actionButton.Highlight ();
	}

	public static void ClearButtonHighlights(){
		print ("buttonCount: " + instance.buttons.Count);
		foreach(GameObject button in instance.buttons){
			button.GetComponent<ActionButton> ().UnHighlight ();
		}
	}


	public static void ClearActionButtonHighlights(){
		instance.attackButton.GetComponent<ActionButton> ().UnHighlight ();
		instance.abilityButton.GetComponent<ActionButton> ().UnHighlight ();
		instance.itemButton.GetComponent<ActionButton> ().UnHighlight ();
	}

	public void DoAttack(){
		selectionMode = SelectionModes.None;
		HideSubActions ();
		SelectAbility (activePartyMember.abilities [0]);
	}

	public void SelectAbility(Ability ability){
		selectionMode = SelectionModes.ActionTarget;
		selectedAbility = ability;
		foreach (GameObject enemy in RoomController.instance.enemies) {
			enemy.GetComponent<Baddie> ().EnableClick ();
		}
		foreach(GameObject button in instance.buttons){
			if (button != null) {
				Destroy (button);
			}
		}
		ObjectTooltip.Show (selectedAbility);
		instance.buttons.Clear ();
		Prompt.SetText ("Select a target");
	}

	public void SelectItem(GameObject buttonObject, Item item){
		selectionMode = SelectionModes.ItemTarget;
		selectedItem = item;

		foreach (GameObject enemy in RoomController.instance.enemies) {
			enemy.GetComponent<Baddie> ().EnableClick ();
		}
		foreach(GameObject button in instance.buttons){
			if (button != null) {
				Destroy (button);
			}
		}

		ObjectTooltip.Show (selectedItem);
		instance.buttons.Clear ();
		Prompt.SetText ("Select a target");
	}

	public static void SelectTarget(GameObject target){
		Prompt.Clear();

		if (instance.selectionMode == SelectionModes.ActionTarget) {
			BattleController.ExecuteAction (instance.selectedAbility, instance.activePartyMember, target);
		}else if (instance.selectionMode == SelectionModes.ItemTarget) {
			BattleController.ExecuteAction (instance.selectedItem, instance.activePartyMember, target);
		}
		foreach (GameObject enemy in RoomController.instance.enemies) {
			enemy.GetComponent<Baddie> ().DisableClick ();
		}
		Hide ();
	}

}
