using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class JobWarrior : Job {

	public override List<Type> Abilities(){
		List<Type> list = new List<Type> ();
		list.Add (typeof(AbilityAttack));
		list.Add (typeof(AbilityHeal));
		list.Add (typeof(AbilityHeal));
		list.Add (typeof(AbilityFire));
		list.Add (typeof(AbilityHeal));
		list.Add (typeof(AbilityHeal));
		list.Add (typeof(AbilityHeal));
		list.Add (typeof(AbilityHeal));
		list.Add (typeof(AbilityHeal));
		list.Add (typeof(AbilityHeal));
		list.Add (typeof(AbilityHeal));
		return(list);
	}

	public override string Name(){
		return("Warrior");
	}

	public override int Strength(){
		return(5);
	}

	public override List<LevelUpStruct> LevelUps(){
		List<LevelUpStruct> levelUps = new List<LevelUpStruct> ();
		levelUps.Add (new LevelUpStruct ("Stats I", "Raises strength by 5", 100, LevelUpStatsI));
		levelUps.Add (new LevelUpStruct ("Stats II", "Raises strength by 20", 300, LevelUpStatsII));
		return(levelUps);
	}

	public void LevelUpStatsI(PartyMember partyMember){
		partyMember.strength += 5;
		EventQueue.AddMessage ("Strength increased by 5");
	}

	public void LevelUpStatsII(PartyMember partyMember){
		partyMember.strength += 20;
	}

	public override string SpriteName(){
		return("warrior");
	}
}
