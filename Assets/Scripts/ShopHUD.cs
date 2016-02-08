using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShopHUD : MonoBehaviour {

	public static ShopHUD instance;
	public static PartyMember selectedPartyMember;
	public static Item selectedItem;
	public Vector3 buyButtonPosition;
	public Text prompt;
	public static List<GameObject> buttonList;
	public static List<GameObject> itemList;


	// Use this for initialization
	void Start () {
		instance = this;
		selectedPartyMember = null;
		buyButtonPosition = instance.transform.Find ("Buy").position;
		prompt = transform.Find ("Prompt").GetComponent<Text>();
		prompt.text = "";
		instance.transform.Find ("Buy").position = new Vector3(9999, 9999, 0);
		buttonList = new List<GameObject> ();
		itemList = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Show(){
		GameController.EnterShopMenu ();
		prompt.text = "Select an item and a party member";

		foreach (PartyMember partyMember in PartyMember.members) {
			partyMember.EnableClick ();
		}

		GameObject itemObject = Instantiate (Resources.Load ("Items/Potion"), new Vector3 (9999, 9999, 0), Quaternion.identity) as GameObject;
		itemList.Add (itemObject);
		ShowItems ();
	}

	public void Close(){
		GameController.ExitShopMenu ();
		instance.transform.Find ("Buy").position = new Vector3(9999, 9999, 0);
		prompt.text = "";
		foreach (PartyMember partyMember in PartyMember.members) {
			partyMember.DisableClick ();
		}

		foreach (GameObject item in itemList) {
			Destroy (item);
		}

		itemList.Clear ();
		PartyMember.UnselectAll ();

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
		instance.prompt.text = "Select an item";
		int i = 0;
		foreach(GameObject item in itemList){
			item.transform.parent = instance.transform;
			item.transform.position = new Vector3(100 + (i * 70), 500, item.transform.position.z);
			item.transform.localScale = new Vector3 (1f, 1f, 1);
			Button button = item.GetComponent<Button>();

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
		ClearButtonHighlights ();
		buttonObject.transform.Find("Image").GetComponent<Image> ().color = Color.yellow;
	}

	public static void ClearButtonHighlights(){
		foreach(GameObject button in buttonList){
			button.transform.Find("Image").GetComponent<Image> ().color = Color.white;
		}
	}

	public static void ConfirmBuy(Item item){
		selectedItem = item;
		ShowBuyButton ();
		instance.prompt.text = selectedItem.Name() + ": " + selectedItem.Description() + ". Cost: " + selectedItem.Cost();
	}

	public static void SelectPartyMember(GameObject partyMember){
		selectedPartyMember = partyMember.GetComponent<PartyMember> ();
		ShowBuyButton ();
	}

	public static void ShowBuyButton(){
		if(selectedItem != null && selectedPartyMember != null){
			if(selectedItem.Cost() <= PartyMember.currency){
				instance.transform.Find ("Buy").position = instance.buyButtonPosition;
			}else{
				instance.transform.Find ("Buy").position = new Vector3(9999, 9999, 0);
			}
		}
	}

	public void Purchase(){
		GameObject newItem = Instantiate (selectedItem.gameObject);
		newItem.transform.Find("Image").GetComponent<Image> ().color = Color.white;
		selectedPartyMember.heldItems.Add (newItem.GetComponent<Item>());
		transform.Find ("Buy").position = new Vector3(9999, 9999, 0);
		instance.prompt.text = "";
		PartyMember.currency -= selectedItem.Cost();
		EventQueue.AddMessage ("Purchased " + selectedItem.Name());
		ClearButtonHighlights ();
		selectedItem = null;
		selectedPartyMember = null;
		PartyMember.UnselectAll ();
	}
}
