using UnityEngine;
using System.Collections;

public class AbilityFire : Ability {

	// Use this for initialization
	public override bool Perform (PartyMember originator, GameObject target) {
		if (originator.magicPoints > 0) {
			originator.magicPoints -= 1;
			int damage = Random.Range (10, 20);
			EventQueue.AddMessage (originator.beingName + "casts fire!");
			EventQueue.AddEvent (originator.gameObject, target, damage, DamageTypes.Fire);
			return(true);
		} else {
			EventQueue.AddMessage ("need more mp");
			return(false);
		}
	}

	public override string Name () {
		return("Fire");
	}

	public override string SpriteName(){
		return("button_fire");
	}

	public override string Description(){
		return("Attack a target with fire");
	}
}
