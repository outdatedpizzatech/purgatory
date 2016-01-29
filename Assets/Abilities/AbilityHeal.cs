﻿using UnityEngine;
using System.Collections;

public class AbilityHeal : Ability {

	// Use this for initialization
	public override void Perform (PartyMember originator, GameObject target) {
		if (originator.magic > 0) {
			originator.magic -= 1;
			int damage = Random.Range (10, 20);
			EventQueue.AddMessage (originator.memberName + " casts heal!");
			EventQueue.AddEvent (target, -damage, DamageTypes.Physical);
		}
	}

	public override string Name () {
		return("Heal");
	}
}
