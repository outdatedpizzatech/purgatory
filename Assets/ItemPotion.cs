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
		owner.heldItems.Remove (this);
		owner.turnAvailable = false;
		EventQueue.AddMessage (owner.memberName + " drank a potion");
		EventQueue.AddEvent (owner.gameObject, -20, DamageTypes.Physical);
		Destroy (gameObject);
	}
}
