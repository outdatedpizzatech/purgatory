using UnityEngine;
using System.Collections;

public class AbilityHeal : Ability {

	// Use this for initialization
	public override bool Perform (PartyMember originator, GameObject target) {
		if (originator.magic > 0) {
			originator.magic -= 1;
			int damage = Random.Range (10, 20);
			EventQueue.AddMessage (originator.memberName + " casts heal!");
			EventQueue.AddEvent (target, -damage, DamageTypes.Physical);
			return(true);
		} else {
			EventQueue.AddMessage ("need more mp");
			return(false);
		}
	}

	public override string Name () {
		return("Heal");
	}

	public override string SpriteName(){
		return("button_heal");
	}

	public override string Description(){
		return("Heal damage to the target");
	}
}
