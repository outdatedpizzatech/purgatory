using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MagicMeter : MonoBehaviour {

	private Image filler;

	// Use this for initialization
	void Start () {
		filler = transform.Find ("Meter").GetComponent<Image> ();
	}

	// Update is called once per frame
	void Update () {
//		filler.fillAmount = (float)PartyMember.instance.magic / (float)PartyMember.instance.maxMagic;
	}
}
