using UnityEngine;
using System.Collections;

public class ItemPotion : Item {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public override bool Use(PartyMember originator, GameObject target) {
		bool success = false;
		if (originator.gameObject == target) {
			EventQueue.AddMessage (originator.memberName + " drank a potion");
			EventQueue.AddEvent (target.gameObject, -20, DamageTypes.Physical);
			owner.turnable.ResetTurn();
			owner.RemoveItem (this);
			Destroy (gameObject);
			success = true;
		} else if (target.GetComponent<Baddie> ()) {
			EventQueue.AddMessage ("there's no need to do that");
		} else if (target.GetComponent<PartyMember>()){
			Item addedItem = target.GetComponent<PartyMember> ().AddItem (this);
			if (addedItem != null) {
				originator.RemoveItem (this);
				originator.turnable.ResetTurn();
				EventQueue.AddMessage ("handed it over");
				success = true;
			} else {
				EventQueue.AddMessage ("tried to give but failed");
			}
		}
		return(success);
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
