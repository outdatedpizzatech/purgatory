using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RoomController : MonoBehaviour {

	public static RoomController instance;

	public List<GameObject> enemies = new List<GameObject>();

	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NextRoom(){
		
		if(Random.value < 1f){
//			floorNumber++;

			AddEnemy ();
			AddEnemy ();

			int i = 0;

			foreach (GameObject enemy in this.enemies) {
				float negFactor = 1;
				if (i % 2 == 0) negFactor = -1;
				float xPosition;
				if (this.enemies.Count % 2 == 0) {
					xPosition = Mathf.CeilToInt ((float)(i + 1) / 2) * negFactor * 1.5f;
				} else {
					xPosition = Mathf.CeilToInt ((float)i / 2) * negFactor * 1.5f;
				}
				enemy.transform.position = new Vector3(xPosition, -3, 0);
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
