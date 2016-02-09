using UnityEngine;
using System.Collections;

public class Ability {

	public virtual bool Perform (PartyMember originator, GameObject target) {
		return(false);
	}

	public virtual string Name () {
		return("NoName");
	}

	public virtual string SpriteName(){
		return("");
	}

	public virtual string Description(){
		return("NoDescription");
	}
}
