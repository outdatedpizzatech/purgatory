﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class JobWhiteMage : Job {

	public override List<Type> Abilities(){
		List<Type> list = new List<Type> ();
		list.Add (typeof(AbilityHeal));
		return(list);
	}

	public override string Name(){
		return("White Mage");
	}
}