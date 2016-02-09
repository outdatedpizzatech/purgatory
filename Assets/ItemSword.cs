using UnityEngine;
using System.Collections;

public class ItemSword : Item {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override ItemTypes ItemType(){
		return(ItemTypes.Weapon);
	}

	public override bool Use(PartyMember originator, GameObject target) {
		bool success = false;
		if (originator.gameObject == target) {
			EventQueue.AddMessage (owner.memberName + " equipped sword");
			originator.turnAvailable = false;
			owner.Equip (this);
			success = true;
		} else if (target.GetComponent<Baddie> ()) {
			EventQueue.AddMessage ("there's no need to do that");
		} else if (target.GetComponent<PartyMember>()){
			Item addedItem = target.GetComponent<PartyMember> ().AddItem (this);
			if (addedItem != null) {
				originator.RemoveItem (this);
				originator.turnAvailable = false;
				EventQueue.AddMessage ("handed it over");
				success = true;
			} else {
				EventQueue.AddMessage ("tried to give but failed");
			}
		}
		return(success);
	}

	public override string Name(){
		return("Sword");
	}

	public override string Description(){
		return("I dunno, it's a sword");
	}

	public override int Cost(){
		return(300);
	}
}
