using UnityEngine;
using System.Collections;

public class Ability {

	public virtual void Perform (PartyMember originator, GameObject target) {
		
	}

	public virtual string Name () {
		return("NoName");
	}
}
