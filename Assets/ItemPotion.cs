using UnityEngine;
using System.Collections;

public class ItemPotion : Item {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Use(){
		EventQueue.AddMessage (owner.memberName + " drank a potion");
		EventQueue.AddEvent (owner.gameObject, -20, DamageTypes.Physical);
		owner.turnAvailable = false;
		owner.RemoveItem (this);
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
