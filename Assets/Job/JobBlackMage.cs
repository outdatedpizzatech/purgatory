using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class JobBlackMage : Job {

	public override List<Type> Abilities(){
		List<Type> list = new List<Type> ();
		list.Add (typeof(AbilityAttack));
		list.Add (typeof(AbilityFire));
		list.Add (typeof(AbilityHeal));
		list.Add (typeof(AbilityHeal));
		list.Add (typeof(AbilityFire));
		list.Add (typeof(AbilityHeal));
		list.Add (typeof(AbilityHeal));
		list.Add (typeof(AbilityHeal));
		list.Add (typeof(AbilityHeal));
		list.Add (typeof(AbilityHeal));
		return(list);
	}

	public override string Name(){
		return("Black Mage");
	}

	public override string SpriteName(){
		return("black_mage");
	}
}
