using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class JobBlackMage : Job {

	public override List<Type> Abilities(){
		List<Type> list = new List<Type> ();
		list.Add (typeof(AbilityFire));
		return(list);
	}

	public override string Name(){
		return("Black Mage");
	}
}
