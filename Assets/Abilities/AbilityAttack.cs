using UnityEngine;
using System.Collections;

public class AbilityAttack : Ability {

	// Use this for initialization
	public override bool Perform (PartyMember originator, GameObject target) {
		
		int damage = Random.Range (1 + originator.strength, 5 + originator.strength);
		EventQueue.AddMessage (originator.beingName + " attacks!");
		EventQueue.AddEvent (originator.gameObject, target, damage, DamageTypes.Physical);
		return(true);
	}

	public override string Name () {
		return("Attack");
	}
	public override string SpriteName(){
		return("button_attack");
	}

	public override string Description(){
		return("Attack a target with your equipped weapon");
	}
}
