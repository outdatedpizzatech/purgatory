using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EventQueue : MonoBehaviour {

	public List<ActionEvent> actionEvents;
	public static EventQueue instance;
	public delegate void DelegateEvent(PartyMember originator, GameObject target);

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
			if (!CurrentEvent ().executed) {
				CurrentEvent ().Execute ();
			} else {
				if(CurrentEvent().Finished()){
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

	public static int AddLambda(Action lambda, int index){
		ActionEvent actionEvent = new ActionEvent ();
		actionEvent.lambda = lambda;
		instance.actionEvents.Insert(index, actionEvent);
		return(instance.actionEvents.Count - 1);
	}

	public static int AddShowCombatMenu(PartyMember partyMember, int index){
		ActionEvent actionEvent = new ActionEvent ();
		actionEvent.combatMenu = true;
		actionEvent.partyMember = partyMember;
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

	public static int AddLambda(Action lambda){
		return(AddLambda (lambda, instance.actionEvents.Count));
	}

	public static int AddShowCombatMenu(PartyMember partyMember){
		return(AddShowCombatMenu (partyMember, instance.actionEvents.Count));
	}
}
