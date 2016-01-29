using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthMeter : MonoBehaviour {

	private Image filler;
	public GameObject partyMemberObject;
	private PartyMember partyMember;

	// Use this for initialization
	void Start () {
		partyMember = partyMemberObject.GetComponent<PartyMember> ();
		filler = transform.Find ("Meter").GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		filler.fillAmount = (float)partyMember.health / (float)partyMember.maxHealth;
	}
}
