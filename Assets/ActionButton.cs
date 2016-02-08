using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour {

	public Sprite sprite;
	private Image front;

	// Use this for initialization
	void Start () {
		front = transform.Find ("Front").GetComponent<Image> ();
		front.sprite = sprite;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
