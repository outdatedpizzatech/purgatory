using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static bool frozen;
	public static bool inEncounter;
	public static bool gameOver;
	public static bool inLevelUpMenu;
	public static bool inShopMenu;

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
			GameObject.Find ("CombatMenu").GetComponent<Canvas> ().enabled = false;
			GameObject.Find ("Navigation").GetComponent<Canvas> ().enabled = false;
			GameObject.Find ("LevelUpHUD").GetComponent<Canvas> ().enabled = false;
			GameObject.Find ("ShopHUD").GetComponent<Canvas> ().enabled = false;	
		} else if (inLevelUpMenu) {
			GameObject.Find ("CombatMenu").GetComponent<Canvas> ().enabled = false;
			GameObject.Find ("Navigation").GetComponent<Canvas> ().enabled = false;
			GameObject.Find ("LevelUpHUD").GetComponent<Canvas> ().enabled = true;
			GameObject.Find ("ShopHUD").GetComponent<Canvas> ().enabled = false;	
		} else if (inEncounter) {
			GameObject.Find ("CombatMenu").GetComponent<Canvas> ().enabled = true;
			GameObject.Find ("Navigation").GetComponent<Canvas> ().enabled = false;
			GameObject.Find ("LevelUpHUD").GetComponent<Canvas> ().enabled = false;
			GameObject.Find ("ShopHUD").GetComponent<Canvas> ().enabled = false;	
		} else if (inShopMenu) {
			GameObject.Find ("CombatMenu").GetComponent<Canvas> ().enabled = false;
			GameObject.Find ("Navigation").GetComponent<Canvas> ().enabled = false;
			GameObject.Find ("LevelUpHUD").GetComponent<Canvas> ().enabled = false;
			GameObject.Find ("ShopHUD").GetComponent<Canvas> ().enabled = true;	
		} else {
			GameObject.Find ("CombatMenu").GetComponent<Canvas> ().enabled = false;
			GameObject.Find ("Navigation").GetComponent<Canvas> ().enabled = true;
			GameObject.Find ("LevelUpHUD").GetComponent<Canvas> ().enabled = false;
			GameObject.Find ("ShopHUD").GetComponent<Canvas> ().enabled = false;	
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

	public static void EnterShopMenu(){
		inShopMenu = true;
	}

	public static void ExitShopMenu(){
		inShopMenu = false;
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
