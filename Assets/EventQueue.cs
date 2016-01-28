using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventQueue : MonoBehaviour {

	public List<ActionEvent> actionEvents;
	public static EventQueue instance;

	public ActionEvent CurrentEvent(){
		if (actionEvents.Count > 0) {
			return(actionEvents [0]);
		} else {
			return(null);
		}
	}

	// Use this for initialization
	void Start () {
		instance = this;
		actionEvents = new List<ActionEvent> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (CurrentEvent () != null) {
			print ("currentEventFound");
			if (!CurrentEvent ().executed) {
				print ("currentEventExecuted");
				CurrentEvent ().Execute ();
			} else {
				print ("currentEventFinished: " + CurrentEvent().Finished());
				if(CurrentEvent().Finished()){
					print ("currentEventFinishedAndRemoved");
					actionEvents.RemoveAt (0);
				}
			}
		}
	}

	public static int AddEvent(GameObject attackable, int damage, DamageTypes damageType, int index){
		ActionEvent actionEvent = new ActionEvent ();
		actionEvent.attackable = attackable;
		actionEvent.damage = damage;
		actionEvent.damageType = damageType;
		instance.actionEvents.Insert(index, actionEvent);
		return(instance.actionEvents.Count - 1);
	}

	public static int AddDestroy(GameObject attackable, int index){
		ActionEvent actionEvent = new ActionEvent ();
		actionEvent.attackable = attackable;
		actionEvent.destroy = true;
		instance.actionEvents.Insert(index, actionEvent);
		return(instance.actionEvents.Count - 1);
	}

	public static int AddMessage(string message, int index){
		ActionEvent actionEvent = new ActionEvent ();
		actionEvent.text = message;
		instance.actionEvents.Insert(index, actionEvent);
		return(instance.actionEvents.Count - 1);
	}

	public static int AddEvent(GameObject attackable, int damage, DamageTypes damageType){
		return(AddEvent (attackable, damage, damageType, instance.actionEvents.Count));
	}

	public static int AddDestroy(GameObject attackable){
		return(AddDestroy (attackable, instance.actionEvents.Count));
	}

	public static int AddMessage(string message){
		return(AddMessage (message, instance.actionEvents.Count));
	}
}
