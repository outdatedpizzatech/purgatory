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
			PartyMember activePartyMember = null;
			foreach (PartyMember partyMember in PartyMember.members) {
				if (partyMember.turnAvailable) {
					activePartyMember = partyMember;
					break;
				}
			}
			if (activePartyMember == null && !GameController.frozen && EventQueue.instance.actionEvents.Count < 1) {
				if (RoomController.instance.enemies.Count < 1) {
					SpeechBubble.AddMessage ("all enemies eliminated", false);
					BattleController.inCombat = false;
					GameController.ExitEncounter ();
				} else {
					foreach (GameObject enemy in RoomController.instance.enemies) {
						enemy.GetComponent<Corgi> ().DoAction ();
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
	}

	public static void StartBattle(){
		inCombat = true;
		EventQueue.AddMessage ("You encounter some baddies");
		foreach (PartyMember partyMember in PartyMember.members) {
			partyMember.turnAvailable = true;
		}
	}


}
