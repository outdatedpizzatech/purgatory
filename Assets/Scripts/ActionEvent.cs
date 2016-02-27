using UnityEngine;
using System;
using System.Collections;

public class ActionEvent {

	public GameObject attackable;
	public int damage;
	public DamageTypes damageType;
	public bool destroy;
	public string text;
	public bool executed;
	public bool combatMenu;
	public PartyMember partyMember;
	public Action lambda;

	public void Execute(){
		if (combatMenu) {
			CombatMenu.Display (partyMember);	
		} else if (text != null) {
			SpeechBubble.AddMessage (text);
		} else {
			if (lambda != null) {
				lambda ();
			}else if (attackable != null) {
				IAttackable v = attackable.GetComponent (typeof(IAttackable)) as IAttackable;
				if (!destroy) {
					v.ReceiveHit (damage, damageType);
				} else {
					v.DestroyMe ();
				}
			}

		}
		executed = true;
	}

	public bool Finished(){
		if (text == null) {
			return(true);
		}else{
			return(SpeechBubble.mainBubble.done);
		}
	}

}
