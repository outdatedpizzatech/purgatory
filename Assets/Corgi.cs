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
		GameObject.Find ("Combat").GetComponent<Canvas>().enabled = true;
		GameObject.Find ("Combat").transform.Find ("CombatMenu").GetComponent<CombatMenu> ().target = gameObject;
		Transform menuTransform = CombatMenu.instance.transform;
		menuTransform.position = Camera.main.WorldToScreenPoint (transform.position);
	}

	public void DoAction(){
		int damage = Random.Range (1, 10);
		Player.instance.health -= damage;
		EventQueue.AddMessage (Name() + " bites!");
		EventQueue.AddEvent (Player.instance.gameObject, damage, DamageTypes.Physical);
	}
}
