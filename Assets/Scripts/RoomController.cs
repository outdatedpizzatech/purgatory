using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RoomController : MonoBehaviour {

	public static int floorNumber;

	public static RoomController instance;

	public List<GameObject> enemies = new List<GameObject>();

	// Use this for initialization
	void Start () {
		instance = this;
		floorNumber = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NextRoom(){
		
		if(Random.value < 1f){
			floorNumber++;

			AddEnemy ();
			AddEnemy ();
			AddEnemy ();
			AddEnemy ();
			AddEnemy ();
			AddEnemy ();

			Transform formation = GameObject.Find ("BaddiesHUD/Formation" + this.enemies.Count).transform;

			int i = 0;
			foreach (GameObject enemy in this.enemies) {
				enemy.transform.position = formation.GetChild (i).position;	
				i++;
			}


			GameController.EnterEncounter ();
		}
	}

	void AddEnemy(){
		GameObject enemyObject;
		if(Random.value < .5f){
			enemyObject = Instantiate (Resources.Load ("Baddies/Corgi"), Vector3.zero, Quaternion.identity) as GameObject;
		}else{
			enemyObject = Instantiate (Resources.Load ("Baddies/Frog"), Vector3.zero, Quaternion.identity) as GameObject;
		}
		enemies.Add (enemyObject);
		enemyObject.transform.parent = GameObject.Find ("BaddiesHUD").transform;
	}


	public List<GameObject> AllEntities(){
		List<GameObject> list = new List<GameObject> ();

//		list.Add (PartyMember.instance.gameObject);
		foreach (GameObject enemy in enemies) {
			list.Add (enemy);
		}
		return(list);
	}
}
