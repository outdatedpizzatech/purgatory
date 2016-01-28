using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleController : MonoBehaviour {

	public static BattleController instance;
	public static bool inCombat;

	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (!Player.turnAvailable && !GameController.frozen && EventQueue.instance.actionEvents.Count < 1) {
			if (RoomController.instance.enemies.Count < 1) {
				SpeechBubble.mainBubble.Activate ();
				SpeechBubble.AddMessage ("all enemies eliminated", false);
				BattleController.inCombat = false;
				GameController.ExitEncounter ();
			} else {
				foreach (GameObject enemy in RoomController.instance.enemies) {
					enemy.GetComponent<Corgi> ().DoAction ();
				}
			}
			Player.turnAvailable = true;
		}
	}

	public static void StartBattle(){
		inCombat = true;
		SpeechBubble.mainBubble.Activate ();
		SpeechBubble.AddMessage ("You encounter some baddies", false);
		Player.turnAvailable = true;
	}


}
