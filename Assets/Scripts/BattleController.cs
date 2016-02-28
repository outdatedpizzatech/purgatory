using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleController : MonoBehaviour {

	public static BattleController instance;
	public static bool inCombat;
	public bool combatMenuDisplayed;
	public List<Turnable> turnables = new List<Turnable>();

	// Use this for initialization
	void Start () {
		instance = this;
	}

	public static void AdvanceTurns(){
		float minTurn = 9999999;

		foreach (Turnable turnable in instance.turnables) {
			if (turnable.turn < minTurn) {
				minTurn = turnable.turn;
			}
		}

		foreach (Turnable turnable in instance.turnables) {
			turnable.IncrementTurn (-minTurn);
		}

		Timeline.instance.turnables = instance.turnables;
		Timeline.Generate ();
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

					foreach (Turnable turnable in turnables) {
						if(turnable.Ready()){
							activePartyMember = turnable.GetComponent<PartyMember>();
							if (activePartyMember == null) {
								activeBaddie = turnable.GetComponent<Baddie> ();
							}
							break;
						}
					}

					if (activePartyMember != null) {
						if (!combatMenuDisplayed) {
							activePartyMember.TurnActive ();
							combatMenuDisplayed = true;
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
			if (EventQueue.instance.actionEvents.Count < 1) {
				Timeline.Hide ();
				GameController.ExitEncounter ();
				turnables.Clear ();
			}
		}
	}

	public static void StartBattle(){

		inCombat = true;
		EventQueue.AddMessage ("You encounter some baddies");
		foreach (PartyMember partyMember in PartyMember.members) {
			partyMember.turnable.turn = Random.Range(0, 100f);
			instance.turnables.Add(partyMember.turnable);
		}
		foreach (GameObject baddie in RoomController.instance.enemies) {
			baddie.GetComponent<Turnable>().turn = Random.Range(0, 100f);
			instance.turnables.Add(baddie.GetComponent<Turnable>());
		}

	}

	public static void ExecuteAction(Ability ability, PartyMember partyMember, GameObject target){
		ability.Perform(partyMember, target);
		partyMember.turnable.ResetTurn();
	}

	public static void ExecuteAction(Item item, PartyMember partyMember, GameObject target){
		bool success = item.Use(partyMember, target);
		if(success) partyMember.turnable.ResetTurn();
	}

}
