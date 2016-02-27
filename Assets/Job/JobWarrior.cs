using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class JobWarrior : Job {

	public override List<Type> Abilities(){
		List<Type> list = new List<Type> ();
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

	public override int Agility(){
		return(1);
	}

	public override int Magic(){
		return(1);
	}

	public override int HitPoints(){
		return(50);
	}

	public override int MagicPoints(){
		return(10);
	}

	public override List<LevelUpStruct> LevelUps(){
		List<LevelUpStruct> levelUps = new List<LevelUpStruct> ();
		levelUps.Add (new LevelUpStruct ("Boost I", "STR + 2, Max HP + 10", 100, "button_strength_up_1", BoostI, LevelUpStruct.LevelUpTypes.Boost));
		levelUps.Add (new LevelUpStruct ("Boost II", "STR + 5, Max HP + 30", 300, "button_strength_up_2", BoostII, LevelUpStruct.LevelUpTypes.Boost));
		levelUps.Add (new LevelUpStruct ("Boost III", "STR + 15, Max HP + 70", 1000, "button_strength_up_2", BoostIII, LevelUpStruct.LevelUpTypes.Boost));
		levelUps.Add (new LevelUpStruct ("Boost IV", "STR + 30, Max HP + 150", 2000, "button_strength_up_2", BoostIV, LevelUpStruct.LevelUpTypes.Boost));
		levelUps.Add (new LevelUpStruct ("Boost V", "STR + 75, Max HP + 300", 5000, "button_strength_up_2", BoostV, LevelUpStruct.LevelUpTypes.Boost));
		levelUps.Add (new LevelUpStruct ("Power Break", "Reduces enemy strength by 5%", 100, "button_strength_up_2", PowerBreak, LevelUpStruct.LevelUpTypes.Ability));
		return(levelUps);
	}

	public void BoostI(PartyMember partyMember){
		partyMember.strength += 2;
		EventQueue.AddMessage ("Strength increased by 2");
		partyMember.maxHitPoints += 10;
		EventQueue.AddMessage ("Max HP increased by 10");
	}

	public void BoostII(PartyMember partyMember){
		partyMember.strength += 5;
		EventQueue.AddMessage ("Strength increased by 5");
		partyMember.maxHitPoints += 30;
		EventQueue.AddMessage ("Max HP increased by 30");
	}

	public void BoostIII(PartyMember partyMember){
		partyMember.strength += 15;
		EventQueue.AddMessage ("Strength increased by 15");
		partyMember.maxHitPoints += 70;
		EventQueue.AddMessage ("Max HP increased by 70");
	}

	public void BoostIV(PartyMember partyMember){
		partyMember.strength += 30;
		EventQueue.AddMessage ("Strength increased by 30");
		partyMember.maxHitPoints += 150;
		EventQueue.AddMessage ("Max HP increased by 150");
	}

	public void BoostV(PartyMember partyMember){
		partyMember.strength += 75;
		EventQueue.AddMessage ("Strength increased by 75");
		partyMember.maxHitPoints += 300;
		EventQueue.AddMessage ("Max HP increased by 300");
	}

	public void PowerBreak(PartyMember partyMember){
		partyMember.abilities.Add (new AbilityPowerBreak());
		EventQueue.AddMessage ("Learned Power Break!");
	}

	public override string SpriteName(){
		return("warrior");
	}
}
