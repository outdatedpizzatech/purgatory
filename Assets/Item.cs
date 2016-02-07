using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public PartyMember owner;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
	}

	public virtual ItemTypes ItemType(){
		return(ItemTypes.Consumable);
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
