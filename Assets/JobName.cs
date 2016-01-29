using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class JobName : MonoBehaviour {

	Text text;
	PartyMember partyMember;
	public GameObject partyMemberObject;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		partyMember = partyMemberObject.GetComponent<PartyMember> ();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = partyMember.job.Name ();
	}
}
