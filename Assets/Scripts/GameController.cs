using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static bool frozen;
	public static bool inEncounter;
	public static bool gameOver;
	public static bool inLevelUpMenu;

	// Use this for initialization
	void Start () {
		gameOver = false;
		CombatMenu.Hide ();
		Unfreeze ();
	}

	void Update(){
		bool partyIsAlive = false;
		foreach (PartyMember partyMember in PartyMember.members) {
			partyIsAlive = partyMember.health > 0;
			if (partyIsAlive) {
				break;
			}
		}
		if (!gameOver && !partyIsAlive) {
			EnterGameOver ();
		}
		if (gameOver) {
			GameObject.Find ("Combat").GetComponent<Canvas> ().enabled = false;
			GameObject.Find ("Navigation").GetComponent<Canvas> ().enabled = false;
			GameObject.Find ("LevelUp").GetComponent<Canvas> ().enabled = false;	
		} else if (inLevelUpMenu) {
			GameObject.Find ("Combat").GetComponent<Canvas> ().enabled = false;
			GameObject.Find ("Navigation").GetComponent<Canvas> ().enabled = false;
			GameObject.Find ("LevelUp").GetComponent<Canvas> ().enabled = true;
		} else if (inEncounter) {
			GameObject.Find ("Combat").GetComponent<Canvas> ().enabled = true;
			GameObject.Find ("Navigation").GetComponent<Canvas> ().enabled = false;
			GameObject.Find ("LevelUp").GetComponent<Canvas> ().enabled = false;
		} else {
			GameObject.Find ("Combat").GetComponent<Canvas> ().enabled = false;
			GameObject.Find ("Navigation").GetComponent<Canvas> ().enabled = true;
			GameObject.Find ("LevelUp").GetComponent<Canvas> ().enabled = false;
		}
	}

	void EnterGameOver(){
		gameOver = true;
		SpeechBubble.mainBubble.Activate ();
		SpeechBubble.AddMessage ("your adventure has ended.");
	}
	
	public static void Freeze(){
		frozen = true;
	}

	public static void Unfreeze(){
		frozen = false;
	}

	public static void EnterLevelUpMenu(){
		inLevelUpMenu = true;
	}

	public static void ExitLevelUpMenu(){
		inLevelUpMenu = false;
	}

	public static void EnterEncounter(){
		inEncounter = true;
		BattleController.StartBattle ();
	}

	public static void ExitEncounter(){
		inEncounter = false;
	}


	public void Test(){
		print ("test");
	}
}
