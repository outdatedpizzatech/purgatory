using UnityEngine;
using System.Collections;
using System;

public class AbilityPowerBreak : Ability {

	// Use this for initialization
	public override bool Perform (PartyMember originator, GameObject target) {
		EventQueue.AddMessage (originator.beingName + " done used power break!");
		EventQueue.AddLambda (() => {
			target.GetComponent<Being>().strengthOffset -= 5;
		});
		return(true);
	}

	public override string Name () {
		return("Power Break");
	}

	public override string SpriteName(){
		return("button_fire");
	}

	public override string Description(){
		return("Reduce the target's strength");
	}
}
