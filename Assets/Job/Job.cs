﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Job {

	public static List<Job> jobs = new List<Job> {
		new JobWarrior(),
		new JobWhiteMage(),
		new JobBlackMage()
	};

	public virtual List<Type> Abilities(){
		return(null);
	}

	public virtual string Name(){
		return("NoJob");
	}
}