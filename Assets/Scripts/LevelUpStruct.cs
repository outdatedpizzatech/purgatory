using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public struct LevelUpStruct
{
	public delegate void Perform(PartyMember partyMember);
	public string name;
	public string description;
	public int cost;
	public string spriteName;
	public LevelUpTypes levelUpType;
	public Perform performer;
	public enum LevelUpTypes
	{
		None,
		Boost,
		Ability,
		Enhancement
	}

	public LevelUpStruct(string inputName, string inputDescription, int inputCost, string inputSpriteName, Perform inputPerformer, LevelUpTypes inputLevelUpType)
	{
		name = inputName;
		description = inputDescription;
		cost = inputCost;
		performer = inputPerformer;
		spriteName = inputSpriteName;
		levelUpType = inputLevelUpType;
	}
}