using UnityEngine;
using System.Collections;
using System;

public class AbilityRiposte : Ability {

	// Use this for initialization
	public override bool Perform (PartyMember originator, GameObject target) {
		EventQueue.AddMessage (originator.beingName + " done used riposte!");
		EventQueue.AddLambda (() => {
			originator.buffs.Add (new BuffRiposte());
		});
		return(true);
	}

	public override string Name () {
		return("Riposte");
	}

	public override string SpriteName(){
		return("button_fire");
	}

	public override string Description(){
		return("Riposte attacks");
	}
}
