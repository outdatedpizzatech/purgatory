using UnityEngine;
using System.Collections;

public class Corgi : MonoBehaviour, IAttackable {

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

	public string Name(){
		return("Corgi-sama");
	}

	public int Health(){
		return(health);
	}

	public void Test(){
		print ("test");
	}

	void OnMouseDown() {
		CombatMenu.SelectTarget (gameObject);
	}

	public void DoAction(){
		int randomValue = Random.Range (0, PartyMember.members.Count - 1);
		PartyMember target = PartyMember.members [randomValue];
		int damage = Random.Range (1, 10);
		target.health -= damage;
		EventQueue.AddMessage (Name() + " bites " + target.memberName + "!");
		EventQueue.AddEvent (target.gameObject, damage, DamageTypes.Physical);
	}
}
