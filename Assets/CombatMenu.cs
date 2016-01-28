using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CombatMenu : MonoBehaviour {

	public GameObject target;
	public static CombatMenu instance;
	public static bool displayed = false;
	public Player.ActionDelegate selectedAction;

	// Use this for initialization
	void Start () {
		instance = this;
		displayed = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public static void Display(){
		displayed = true;
		instance.transform.parent.GetComponent<Canvas> ().enabled = true;
		int i = 0;
		foreach(Player.ActionDelegate actionDelegate in Player.instance.Actions()){
			
			Vector3 newPosition = instance.transform.position;
			newPosition.y += i * -50;
			GameObject buttonObject = Instantiate (Resources.Load ("ActionButton"), newPosition, Quaternion.identity) as GameObject;
			buttonObject.transform.parent = instance.transform;
			Button button = buttonObject.GetComponent<Button>();
			button.transform.Find ("Text").GetComponent<Text> ().text = actionDelegate.Method.Name;

			Player.ActionDelegate capturedDelegate = actionDelegate;

			button.onClick.AddListener( delegate {
				instance.SelectAction(capturedDelegate); } );

//			button.onClick.AddListener (delegate {
//				print ("selected index:" + i);
//			});
//
			i++;
		}
	}

	public static void Hide(){
		displayed = false;
		instance.transform.parent.GetComponent<Canvas> ().enabled = false;
	}

	public void SelectAction(Player.ActionDelegate a){
		selectedAction = a;
		print ("selected action is " + a.Method.Name);
		foreach (GameObject enemy in RoomController.instance.enemies) {
			enemy.GetComponent<Collider2D> ().enabled = true;
		}
		EventQueue.AddMessage ("select target");
	}

	public static void SelectTarget(GameObject target){
		instance.selectedAction(target);
		foreach (GameObject enemy in RoomController.instance.enemies) {
			enemy.GetComponent<Collider2D> ().enabled = false;
		}
	}
}
