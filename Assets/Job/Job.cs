using UnityEngine;
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

	public virtual int Strength(){
		return(0);
	}

	public virtual int Agility(){
		return(0);
	}

	public virtual int Magic(){
		return(0);
	}

	public virtual int HitPoints(){
		return(0);
	}

	public virtual int MagicPoints(){
		return(0);
	}

	public virtual List<LevelUpStruct> LevelUps(){
		return(new List<LevelUpStruct>());
	}

	public virtual string SpriteName(){
		return("");
	}
}
