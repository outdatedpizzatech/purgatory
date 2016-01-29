using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PartyMember : MonoBehaviour, IAttackable {

	public int health;
	public int maxHealth;
	public int magic;
	public int maxMagic;
	public static List<PartyMember> members = new List<PartyMember>();
	public bool turnAvailable;
	public delegate void ActionDelegate(PartyMember originator, GameObject target);
	public string memberName;
	public List<Ability> abilityList = new List<Ability>();
	public Job job;

	// Use this for initialization
	void Start () {
		health = maxHealth;
		magic = maxMagic;
		members.Add (this);
		turnAvailable = true;
		SetJob ();
		SetAbilities ();
	}

	void SetJob(){
		int randomValue = UnityEngine.Random.Range(0, Job.jobs.Count);
		job = Job.jobs[randomValue];
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
		EventQueue.AddMessage (memberName + " sustains " + damage + " damage", 1);
		health -= damage;
	}

	public void DestroyMe(){

	}

	public void SetAbilities(){
		foreach(Type jobType in job.Abilities()){
			abilityList.Add ((Ability)Activator.CreateInstance(jobType));
		}
	}

	public int Health(){
		return(health);
	}
}
