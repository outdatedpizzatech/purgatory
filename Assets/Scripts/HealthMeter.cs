using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthMeter : MonoBehaviour {

	private PartyMember partyMember;
	private Text text;

	// Use this for initialization
	void Start () {
		partyMember = transform.parent.GetComponent<PartyMember> ();
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = partyMember.HitPoints().ToString();
	}
}
