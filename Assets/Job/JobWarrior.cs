using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class JobWarrior : Job {

	public override List<Type> Abilities(){
		List<Type> list = new List<Type> ();
		list.Add (typeof(AbilityAttack));
		return(list);
	}

	public override string Name(){
		return("Warrior");
	}
}
