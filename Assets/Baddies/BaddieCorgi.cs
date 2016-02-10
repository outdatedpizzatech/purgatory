using UnityEngine;
using System.Collections;

public class BaddieCorgi : Baddie {

	public override int Level(){
		return(1);
	}

	public override string Name(){
		return("Corgi-sama");
	}

	public override void DoAction(){
		int randomValue = Random.Range (0, PartyMember.members.Count);
		PartyMember target = PartyMember.members [randomValue];
		int damage = Random.Range (1, 10);
		EventQueue.AddMessage (Name() + " bites " + target.memberName + "!");
		EventQueue.AddEvent (target.gameObject, damage, DamageTypes.Physical);
	}
}
