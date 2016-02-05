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
	public static int currency;
	public int strength;
	public List<List<bool>> levelUps = new List<List<bool>>();

	// Use this for initialization
	void Start () {
		health = maxHealth;
		magic = maxMagic;
		members.Add (this);
		turnAvailable = true;
		SetJob ();
		SetAbilities ();
		SetLevelUps ();
		currency = 500;
	}

	void SetLevelUps(){
		foreach (Job selectedJob in Job.jobs) {
			int count = selectedJob.LevelUps ().Count;
			List<bool> list = new List<bool>();
			foreach (LevelUpStruct levelUpStruct in selectedJob.LevelUps()) {
				list.Add (false);
			}
			levelUps.Add (list);
		}
	}

	void SetJob(){
		int randomValue = UnityEngine.Random.Range(0, Job.jobs.Count);
		job = Job.jobs[randomValue];
		strength = job.Strength();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public string Name(){
		return("playur");
	}


	void OnMouseDown() {
		if (GameController.inEncounter) {
			CombatMenu.SelectTarget (gameObject);
		} else {
			LevelUpHUD.ShowAbilitiesForPartyMember (this);
		}
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

	public void UpdateLevelUpSlot(int i){
		int jobIndex = Job.jobs.IndexOf (job);
		levelUps [jobIndex] [i] = true;
	}

	public bool HasLevelUpSlot(int i){
		int jobIndex = Job.jobs.IndexOf (job);
		return(levelUps [jobIndex] [i]);
	}

	public int Health(){
		return(health);
	}
}
