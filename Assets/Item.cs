using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public PartyMember owner;
	public int cost;
	public string itemName;
	public string description;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
	}

	public virtual void Use(){
	}

	public virtual string Name(){
		return("NoName");
	}

	public virtual string Description(){
		return("NoDescription");
	}

	public virtual int Cost(){
		return(0);
	}
}
