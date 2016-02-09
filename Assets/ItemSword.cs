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
		EventQueue.AddMessage (owner.memberName + " equipped sword");
		owner.turnAvailable = false;
		owner.Equip (this);
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
