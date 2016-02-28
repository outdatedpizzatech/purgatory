using UnityEngine;
using System.Collections;
using System;

public class BuffRiposte {

	// Use this for initialization
	public int Perform (PartyMember owner, GameObject attacker, int damage) {
		if (UnityEngine.Random.value < 25f) {
			EventQueue.AddMessage (owner.beingName + " ripostes!");
			EventQueue.AddLambda (() => {
				EventQueue.AddEvent (owner.gameObject, attacker, 10, DamageTypes.Physical);
			});
		}
		return(0);
	}
}
