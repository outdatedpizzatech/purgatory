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

	public static void AdvanceTurns(){
		float minTurn = 9999999;

		foreach (PartyMember partyMember in PartyMember.members) {
			if (partyMember.turn < minTurn) {
				minTurn = partyMember.turn;
			}
		}
		
		foreach (GameObject enemy in RoomController.instance.enemies) {
			Baddie baddie = enemy.GetComponent<Baddie> ();
			if (baddie.turn < minTurn) {
				minTurn = baddie.turn;
			}
		}

		foreach (PartyMember partyMember in PartyMember.members) {
			partyMember.turn -= minTurn;
		}

		foreach (GameObject enemy in RoomController.instance.enemies) {
			Baddie baddie = enemy.GetComponent<Baddie> ();
			baddie.turn -= minTurn;
		}

		print ("advanced counter by " + minTurn);
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
				if(EventQueue.instance.actionEvents.Count < 1){
					PartyMember activePartyMember = null;
					Baddie activeBaddie = null;
					foreach (PartyMember partyMember in PartyMember.members) {
						if (partyMember.Ready()) {
							activePartyMember = partyMember;
							print ("active member!");
							break;
						}
					}
					if (activePartyMember == null) {
						foreach (GameObject enemy in RoomController.instance.enemies) {
							if (enemy.GetComponent<Baddie> ().Ready ()) {
								activeBaddie = enemy.GetComponent<Baddie> ();
								print ("active baddie!");
								break;
							}
						}
					}
					if (activePartyMember != null) {
						if (!combatMenuDisplayed) {
							combatMenuDisplayed = true;
							print ("show me the menu");
							EventQueue.AddShowCombatMenu (activePartyMember);
						}
					}else if (activeBaddie != null) {
						activeBaddie.DoAction ();
					}else{
						AdvanceTurns ();
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
			partyMember.turn = Random.Range(0, 100);
		}
		foreach (GameObject baddie in RoomController.instance.enemies) {
			baddie.GetComponent<Baddie>().turn = Random.Range(0, 100);
		}
	}

	public static void ExecuteAction(Ability ability, PartyMember partyMember, GameObject target){
		ability.Perform(partyMember, target);
		partyMember.ResetTurn();
	}

	public static void ExecuteAction(Item item, PartyMember partyMember, GameObject target){
		bool success = item.Use(partyMember, target);
		if(success) partyMember.ResetTurn();
	}

}
