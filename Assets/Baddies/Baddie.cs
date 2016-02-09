using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Baddie : MonoBehaviour, IAttackable {

	public int health;
	private Button button;

	// Use this for initialization
	void Start () {
		health = 10;
		button = GetComponent<Button> ();
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

	public void DoClickAction() {
		CombatMenu.SelectTarget (gameObject);

	}

	public virtual void DoAction(){
	}

	public void DisableClick(){
		button.enabled = false;
	}

	public void EnableClick(){
		button.enabled = true;
	}
}
