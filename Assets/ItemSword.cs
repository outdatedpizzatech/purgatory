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

	public override void Use(){
		owner.heldItems.Remove (this);
		if(owner.weapon != null) owner.heldItems.Add (owner.weapon);
		owner.weapon = this;
		owner.turnAvailable = false;
		EventQueue.AddMessage (owner.memberName + " equipped sword");
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
