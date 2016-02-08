using UnityEngine;
using System.Collections;

public class AbilityFire : Ability {

	// Use this for initialization
	public override void Perform (PartyMember originator, GameObject target) {
		if (originator.magic > 0) {
			originator.magic -= 1;
			int damage = Random.Range (10, 20);
			EventQueue.AddMessage (originator.memberName + "casts fire!");
			EventQueue.AddEvent (target, damage, DamageTypes.Fire);
		}
	}

	public override string Name () {
		return("Fire");
	}

	public override string SpriteName(){
		return("button_fire");
	}
}
