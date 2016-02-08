using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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
	public List<Item> heldItems = new List<Item>();
	public Item weapon;
	public Item armor;
	public Item accessory;
	private Button button;
	private GameObject overlay;

	public void DisableClick(){
		button.enabled = false;
	}

	public void EnableClick(){
		button.enabled = true;
	}

	public void ShowOverlay(){
		overlay.SetActive (true);
	}

	public void HideOverlay(){
		overlay.SetActive (false);
	}

	// Use this for initialization
	void Start () {
		button = GetComponent<Button> ();
		health = maxHealth;
		magic = maxMagic;
		members.Add (this);
		turnAvailable = true;
		overlay = transform.Find ("Overlay").gameObject;
		HideOverlay ();
		SetJob ();
		SetAbilities ();
		SetLevelUps ();
		currency = 500;
		GameObject potion = Instantiate (Resources.Load ("Items/Potion"), transform.position, Quaternion.identity) as GameObject;
		heldItems.Add (potion.GetComponent<Item> ());
		potion.GetComponent<Item> ().owner = this;
		GameObject sword = Instantiate (Resources.Load ("Items/Sword"), transform.position, Quaternion.identity) as GameObject;
		heldItems.Add (sword.GetComponent<Item> ());
		sword.GetComponent<Item> ().owner = this;
		Unselect ();
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

	public static void UnselectAll(){
		foreach (PartyMember partyMember in PartyMember.members) {
			partyMember.Unselect ();
		}
	}


	public void DoClickAction() {
		print ("ya clicked me!");
		UnselectAll ();
		if (GameController.inEncounter) {
			CombatMenu.SelectTarget (gameObject);
		} else if (GameController.inShopMenu) {
			Select ();
			ShopHUD.SelectPartyMember (gameObject);
		}else {
			Select ();
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

	public void Select(){
		transform.Find ("Selector").GetComponent<Image> ().color = new Color (1, 1, 1, 1);
	}

	public void Unselect(){
		transform.Find ("Selector").GetComponent<Image> ().color = new Color (1, 1, 1, 0);
	}

	public int Health(){
		return(health);
	}
}
