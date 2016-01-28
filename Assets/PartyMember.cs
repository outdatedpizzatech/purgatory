using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PartyMember : MonoBehaviour, IAttackable {

	public int health;
	public int maxHealth;
	public int magic;
	public int maxMagic;
	public static List<PartyMember> members = new List<PartyMember>();
	public bool turnAvailable;
	public delegate void ActionDelegate(GameObject target);
	public string memberName;

	// Use this for initialization
	void Start () {
		health = maxHealth;
		magic = maxMagic;
		members.Add (this);
		turnAvailable = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public string Name(){
		return("playur");
	}


	void OnMouseDown() {
		CombatMenu.SelectTarget (gameObject);
	}

	public void ReceiveHit(int damage, DamageTypes damageType){
		print ("player receives hit");
		EventQueue.AddMessage (memberName + " sustains " + damage + " damage", 1);
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
			EventQueue.AddMessage (memberName + " attacks!");
			EventQueue.AddEvent (target, damage, DamageTypes.Physical);
			if(BattleController.inCombat) turnAvailable = false;
		}
	}



	public void Heal(GameObject target){
		if (turnAvailable && !GameController.frozen && magic > 0) {
			magic -= 1;
			int damage = Random.Range (10, 20);
			EventQueue.AddMessage (memberName + " casts heal!");
			EventQueue.AddEvent (target, -damage, DamageTypes.Physical);
			if(BattleController.inCombat) turnAvailable = false;
		}
	}

	public void Fire(GameObject target){
		if (turnAvailable && !GameController.frozen && magic > 0) {
			magic -= 1;
			SpeechBubble.mainBubble.Activate ();
			int damage = Random.Range (10, 20);
			EventQueue.AddMessage (memberName + "casts fire!");
			EventQueue.AddEvent (target, damage, DamageTypes.Fire);
			if(BattleController.inCombat) turnAvailable = false;
		}
	}

	public int Health(){
		return(health);
	}
}
