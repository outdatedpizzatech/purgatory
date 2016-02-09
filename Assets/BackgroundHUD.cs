using UnityEngine;
using System.Collections;

public class BackgroundHUD : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.Find ("EnemyField").gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
