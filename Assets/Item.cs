using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Item : MonoBehaviour {

	public PartyMember owner;
	public Sprite sprite;

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
