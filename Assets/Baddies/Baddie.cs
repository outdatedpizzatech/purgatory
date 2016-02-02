using UnityEngine;
using System.Collections;

public class Baddie : MonoBehaviour, IAttackable {

	public int health;

	// Use this for initialization
	void Start () {
		health = 10;
	}

	public void DestroyMe(){
		EventQueue.AddMessage (Name() + " is destroyed", 1);
		RoomController.instance.enemies.Remove (gameObject);
		Destroy (gameObject);
	}

	public void ReceiveHit(int damage, DamageTypes damageType){
		if (gameObject != null) {
			int index = EventQueue.AddMessage (Name () + " surstains " + damage + " damage");
			health -= damage;

			if (health < 1) {
				EventQueue.AddDestroy (gameObject, index + 1);
			}
		}
	}

	public virtual string Name(){
		return("NoName");
	}

	public int Health(){
		return(health);
	}

	void OnMouseDown() {
		CombatMenu.SelectTarget (gameObject);
	}

	public virtual void DoAction(){
	}
}
