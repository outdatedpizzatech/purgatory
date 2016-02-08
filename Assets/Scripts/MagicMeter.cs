using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MagicMeter : MonoBehaviour {

	private Image filler;
	private PartyMember partyMember;

	// Use this for initialization
	void Start () {
		partyMember = transform.parent.GetComponent<PartyMember> ();
		filler = transform.Find ("Meter").GetComponent<Image> ();
	}

	// Update is called once per frame
	void Update () {
		filler.fillAmount = (float)partyMember.magic / (float)partyMember.maxMagic;
	}
}
