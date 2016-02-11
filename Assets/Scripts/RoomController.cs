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
		floorNumber++;

		int baddieLevel = 1;
		int baddieCount = 6;

		for (int c = 0; c < baddieCount; c++) {
			AddEnemy (baddieLevel);
		}

		Transform formation = GameObject.Find ("BaddiesHUD/Formation" + this.enemies.Count).transform;

		int i = 0;
		foreach (GameObject enemy in this.enemies) {
			enemy.transform.position = formation.GetChild (i).position;	
			i++;
		}

		GameController.EnterEncounter ();
	}

	void AddEnemy(int baddieLevel){
		List<string> baddies = Bestiary.instance.baddies[baddieLevel];
		int randomValue = Random.Range (0, baddies.Count);
		print (baddies [randomValue]);
		GameObject enemyObject = Instantiate (Resources.Load ("Baddies/"+ baddies[randomValue]), Vector3.zero, Quaternion.identity) as GameObject;
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
