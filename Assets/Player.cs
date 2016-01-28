using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour, IAttackable {

	public int health;
	public int maxHealth;
	public int magic;
	public int maxMagic;
	public static Player instance;
	public static bool turnAvailable;
	public delegate void ActionDelegate(GameObject target);

	// Use this for initialization
	void Start () {
		health = maxHealth;
		magic = maxMagic;
		instance = this;
		turnAvailable = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public string Name(){
		return("playur");
	}


	void OnMouseDown() {
		GameObject.Find ("Combat").GetComponent<Canvas>().enabled = true;
		GameObject.Find ("Combat").transform.Find ("CombatMenu").GetComponent<CombatMenu> ().target = gameObject;
		Transform menuTransform = CombatMenu.instance.transform;
		menuTransform.position = Camera.main.WorldToScreenPoint (transform.position);
	}

	public void ReceiveHit(int damage, DamageTypes damageType){
		print ("player receives hit");
		EventQueue.AddMessage ("you sustain " + damage + " damage", 1);
		health -= damage;
	}

	public void DestroyMe(){

	}

	public List<ActionDelegate> Actions(){
		List<ActionDelegate> actionList = new List<ActionDelegate>();
		actionList.Add (Attack);
		actionList.Add (Heal);
		actionList.Add (Fire);

		return(actionList);
	}


	public void Attack(GameObject target){
		if (turnAvailable && !GameController.frozen) {
			SpeechBubble.mainBubble.Activate ();
			int damage = Random.Range (1, 10);
			EventQueue.AddMessage ("you attack!");
			EventQueue.AddEvent (target, damage, DamageTypes.Physical);
			if(BattleController.inCombat) turnAvailable = false;
		}
	}



	public void Heal(GameObject target){
		if (turnAvailable && !GameController.frozen && Player.instance.magic > 0) {
			Player.instance.magic -= 1;
			int damage = Random.Range (10, 20);
			EventQueue.AddMessage ("you cast heal!");
			EventQueue.AddEvent (target, -damage, DamageTypes.Physical);
			if(BattleController.inCombat) turnAvailable = false;
		}
	}

	public void Fire(GameObject target){
		if (turnAvailable && !GameController.frozen && Player.instance.magic > 0) {
			Player.instance.magic -= 1;
			SpeechBubble.mainBubble.Activate ();
			int damage = Random.Range (10, 20);
			EventQueue.AddMessage ("you cast fire!");
			EventQueue.AddEvent (target, damage, DamageTypes.Fire);
			if(BattleController.inCombat) turnAvailable = false;
		}
	}

	public int Health(){
		return(health);
	}
}
