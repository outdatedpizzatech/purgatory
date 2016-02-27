using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Job {

	public PartyMember partyMember;

	public static List<Job> jobs = new List<Job> {
		new JobWarrior(),
		new JobWhiteMage(),
		new JobBlackMage()
	};

	public void Bootstrap(){
		int i = 0;
		foreach (LevelUpStruct levelUpStruct in LevelUps()) {
			if (levelUpStruct.cost == 0) {
				levelUpStruct.performer (partyMember);
				partyMember.UpdateLevelUpSlot (i);
			}
			i++;
		}
	}

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
