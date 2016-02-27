using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Baddie : Being, IAttackable {

	public int hitPoints;
	private Button button;
	public Turnable turnable;

	public virtual int Level(){
		return(0);
	}

	void Awake(){
		turnable = gameObject.AddComponent<Turnable> ();
	}

	// Use this for initialization
	void Start () {
		hitPoints = 10;
		button = GetComponent<Button> ();
		turnable.sprite = transform.Find ("Body").GetComponent<Image> ().sprite;
	}

	public void DestroyMe(){
		EventQueue.AddMessage (Name() + " is destroyed", 1);
		RoomController.instance.enemies.Remove (gameObject);
		BattleController.instance.turnables.Remove (turnable);
		Destroy (gameObject);
	}

	public void ReceiveHit(int damage, DamageTypes damageType){
		if (gameObject != null) {
			int index = EventQueue.AddMessage (Name () + " surstains " + damage + " damage");
			hitPoints -= damage;

			if (hitPoints < 1) {
				EventQueue.AddDestroy (gameObject, index + 1);
			}
		}
	}

	public virtual string Name(){
		return("NoName");
	}

	public int HitPoints(){
		return(hitPoints);
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
