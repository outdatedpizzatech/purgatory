using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthMeter : MonoBehaviour {

	private Image filler;

	// Use this for initialization
	void Start () {
		filler = transform.Find ("Meter").GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		filler.fillAmount = (float)Player.instance.health / (float)Player.instance.maxHealth;
	}
}
