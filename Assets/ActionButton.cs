using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour {

	public Sprite sprite;
	private Image front;
	private Image back;
	private Text text;
	public string startText;

	// Use this for initialization
	void Start () {
		front = transform.Find ("Front").GetComponent<Image> ();
		back = transform.Find ("Back").GetComponent<Image> ();
		text = transform.Find ("Text").GetComponent<Text> ();

		front.sprite = sprite;
		SetText (startText);
		UnHighlight();
	}

	public void Highlight(){
		back.color = Color.yellow;
	}

	public void UnHighlight(){
		back.color = new Color(1,1, 1, .5f);
	}

	public void SetText(string inputText){
		text.text = inputText;
	}
	
	// Update is called once per frame
	void Update () {
		print (back);
	}
}
