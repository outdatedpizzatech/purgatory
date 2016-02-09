using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Prompt : MonoBehaviour {

	public Text text;
	public static Prompt instance;

	// Use this for initialization
	void Start () {
		instance = this;	
		text = GetComponent<Text> ();
		Clear ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void SetText(string inputText){
		instance.text.text = inputText;
	}

	public static void Clear(){
		SetText ("");
	}
}
