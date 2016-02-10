using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShopHUD : MonoBehaviour {

	public static ShopHUD instance;
	public static PartyMember selectedPartyMember;
	public static Item selectedItem;
	public Vector3 buyButtonPosition;
	public static List<GameObject> buttonList;
	public static List<GameObject> itemList;
	bool inTransaction = false;
	GameObject buyButton;
	GameObject cancelButton;


	// Use this for initialization
	void Start () {
		instance = this;
		selectedPartyMember = null;
		buttonList = new List<GameObject> ();
		itemList = new List<GameObject> ();
		buyButton = transform.Find ("Buy").gameObject;
		cancelButton = transform.Find ("Cancel").gameObject;
		buyButton.SetActive (false);
		cancelButton.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Show(){
		buyButton.SetActive (false);
		cancelButton.SetActive (false);
		ObjectTooltip.Hide ();
		GameController.EnterShopMenu ();

		GameObject itemObject = Instantiate (Resources.Load ("Items/Potion"), new Vector3 (9999, 9999, 0), Quaternion.identity) as GameObject;
		itemList.Add (itemObject);

		itemObject = Instantiate (Resources.Load ("Items/Sword"), new Vector3 (9999, 9999, 0), Quaternion.identity) as GameObject;
		itemList.Add (itemObject);

		ShowItems ();
	}

	public void Close(){
		ObjectTooltip.Hide ();
		GameController.ExitShopMenu ();

		foreach (GameObject item in itemList) {
			Destroy (item);
		}

		itemList.Clear ();
		PartyMember.UnselectAll ();
		inTransaction = false;
		Prompt.Clear ();

		DestroyButtons ();
	}

	public static void DestroyButtons(){
		foreach(GameObject button in buttonList){
			Destroy (button);
		}
		buttonList = new List<GameObject> ();
	}

	public static void ShowItems(){
		DestroyButtons ();
		int i = 0;
		foreach(GameObject item in itemList){
			GameObject actionButton = Instantiate (Resources.Load ("ActionButton"), Vector3.zero, Quaternion.identity) as GameObject;
			actionButton.transform.parent = instance.transform;
			actionButton.transform.position = new Vector3(100 + (i * 70), 500, item.transform.position.z);
			actionButton.transform.localScale = new Vector3 (1f, 1f, 1);
			actionButton.GetComponent<ActionButton> ().sprite = item.GetComponent<Item> ().sprite;
			Button button = actionButton.GetComponent<Button>();

			Item capturedItem = item.GetComponent<Item>();

			button.onClick.AddListener( delegate {
				ConfirmBuy(capturedItem); } );
			button.onClick.AddListener( delegate {
				HighlightButton(button.gameObject); } );

			buttonList.Add (button.gameObject);
			i++;
		}
	}

	public static void HighlightButton(GameObject buttonObject){
		if (!instance.inTransaction) {
			ClearButtonHighlights ();
			buttonObject.GetComponent<ActionButton> ().Highlight ();
		}
	}

	public static void ClearButtonHighlights(){
		foreach(GameObject button in buttonList){
			button.GetComponent<ActionButton> ().UnHighlight ();
		}
	}

	public static void ConfirmBuy(Item item){
		if (!instance.inTransaction) {
			selectedItem = item;
			ObjectTooltip.Show (item);
			ShowBuyButton ();
		}
	}

	public static void SelectPartyMember(GameObject partyMember){
		selectedPartyMember = partyMember.GetComponent<PartyMember> ();

		if (instance.inTransaction) {
			GameObject newItem = Instantiate (selectedItem.gameObject);
			newItem.transform.Find("Image").GetComponent<Image> ().color = Color.white;
			Item item = selectedPartyMember.AddItem (newItem.GetComponent<Item>());
			if (item != null) {
				PartyMember.currency -= selectedItem.Cost ();
				EventQueue.AddMessage ("Purchased " + selectedItem.Name ());
				ClearButtonHighlights ();
				selectedItem = null;
				selectedPartyMember = null;
				PartyMember.UnselectAll ();
				instance.cancelButton.SetActive (false);
				instance.inTransaction = false;
				ObjectTooltip.Hide ();
			} else {
				EventQueue.AddMessage ("can't carry any more!");
			}
		}


	}

	public static void ShowBuyButton(){
		if(selectedItem != null){
			if(selectedItem.Cost() <= PartyMember.currency){
				instance.buyButton.SetActive (true);
			}else{
				instance.buyButton.SetActive (false);
			}
		}
	}

	public void Cancel(){
		inTransaction = false;
		buyButton.SetActive (true);
		cancelButton.SetActive (false);
		Prompt.Clear ();

	}

	public void Purchase(){
		EventQueue.AddMessage ("who will carry it?");
		Prompt.SetText ("select a party member");
		buyButton.SetActive (false);
		cancelButton.SetActive (true);
		inTransaction = true;
	}
}
