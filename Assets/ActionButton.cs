using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour {

	public Sprite sprite;
	private Image front;
	private Image back;

	// Use this for initialization
	void Start () {
		front = transform.Find ("Front").GetComponent<Image> ();
		back = transform.Find ("Back").GetComponent<Image> ();
		front.sprite = sprite;
		UnHighlight();
	}

	public void Highlight(){
		back.color = Color.yellow;
	}

	public void UnHighlight(){
		back.color = new Color(1,1, 1, .5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
