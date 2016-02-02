using UnityEngine;
using System.Collections;

public class AbilityAttack : Ability {

	// Use this for initialization
	public override void Perform (PartyMember originator, GameObject target) {
		
		int damage = Random.Range (1 + originator.strength, 5 + originator.strength);
		EventQueue.AddMessage (originator.memberName + " attacks!");
		EventQueue.AddEvent (target, damage, DamageTypes.Physical);
	}

	public override string Name () {
		return("Attack");
	}
}
