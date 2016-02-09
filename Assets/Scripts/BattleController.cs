using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleController : MonoBehaviour {

	public static BattleController instance;
	public static bool inCombat;
	public bool combatMenuDisplayed;

	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (inCombat) {
			if (RoomController.instance.enemies.Count < 1) {
				EventQueue.AddMessage ("all enemies eliminated");
				int currency = 100;
				EventQueue.AddMessage ("the party finds " + currency + " currency");
				PartyMember.currency += currency;
				BattleController.inCombat = false;
			} else {
				PartyMember activePartyMember = null;
				foreach (PartyMember partyMember in PartyMember.members) {
					if (partyMember.turnAvailable) {
						activePartyMember = partyMember;
						break;
					}
				}
				if (activePartyMember == null && EventQueue.instance.actionEvents.Count < 1) {
					if (RoomController.instance.enemies.Count > 0) {
						foreach (GameObject enemy in RoomController.instance.enemies) {
							enemy.GetComponent<Baddie> ().DoAction ();
						}
					}
					foreach (PartyMember partyMember in PartyMember.members) {
						partyMember.turnAvailable = true;
					}
				} else {
					if (!combatMenuDisplayed && EventQueue.instance.actionEvents.Count < 1) {
						combatMenuDisplayed = true;
						print ("show me the menu");
						EventQueue.AddShowCombatMenu (activePartyMember);
					}
				}
			}
		} else {
			if(EventQueue.instance.actionEvents.Count < 1) GameController.ExitEncounter ();
		}
	}

	public static void StartBattle(){
		inCombat = true;
		EventQueue.AddMessage ("You encounter some baddies");
		foreach (PartyMember partyMember in PartyMember.members) {
			partyMember.turnAvailable = true;
		}
	}

	public static void ExecuteAction(Ability ability, PartyMember partyMember, GameObject target){
		ability.Perform(partyMember, target);
		partyMember.turnAvailable = false;
	}

	public static void ExecuteAction(Item item, PartyMember partyMember, GameObject target){
		bool success = item.Use(partyMember, target);
		if(success) partyMember.turnAvailable = false;
	}

}
