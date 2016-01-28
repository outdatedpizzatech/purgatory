using UnityEngine;
using System.Collections;

public class ActionEvent {

	public GameObject attackable;
	public int damage;
	public DamageTypes damageType;
	public bool destroy;
	public string text;
	public bool executed;

	public void Execute(){
		if (text != null) {
			SpeechBubble.AddMessage (text);
		} else {
			if (attackable != null) {
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
