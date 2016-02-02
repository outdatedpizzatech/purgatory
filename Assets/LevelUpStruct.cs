using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public struct LevelUpStruct
{
	public delegate void Perform(PartyMember partyMember);
	public string name;
	public int cost;
	public Perform performer;

	public LevelUpStruct(string inputName, int inputCost, Perform inputPerformer)
	{
		name = inputName;
		cost = inputCost;
		performer = inputPerformer;
	}
}