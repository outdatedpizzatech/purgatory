using UnityEngine;
using System.Collections;

public class ItemPotion : Item {

	public new int cost = 150;
	public new string itemName = "Potion";
	public new string description = "Heals 20 HP to its user";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Use(){
		owner.heldItems.Remove (this);
		owner.turnAvailable = false;
		EventQueue.AddMessage (owner.memberName + " drank a potion");
		EventQueue.AddEvent (owner.gameObject, -20, DamageTypes.Physical);
		Destroy (gameObject);
	}

	public override string Name(){
		return("Potion");
	}

	public override string Description(){
		return("Heals user for 20 HP");
	}

	public override int Cost(){
		return(150);
	}
}
