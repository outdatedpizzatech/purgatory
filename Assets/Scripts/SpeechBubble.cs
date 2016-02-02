using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class SpeechBubble : MonoBehaviour {

	[TextArea(3,10)]
	public string[] textToDisplay;
	public List<ActionEvent> actionEvents;
	public bool[] hasEvents;
	public float maxTimeBetweenCharacters;
	public bool dismissable;
	public bool dismissesSelf = false;
	public float setWidth;
	public float setHeight;
	public bool freezesGameOnDisplay;
	public string[][] cursorInstructions;
	public bool isMainBubble;
	
	private int textBubbleIndex;
	private float currentTimeBetweenCharacters;
	private Text text;
	private int textIndex;
	private RectTransform bubble;
	private float maxWidth;
	private float maxHeight;
	private float maxPostFinishDelay = 2f;
	private float currentPostFinishDelay;
	private GameObject arrow;
//	private bool cursorStale;
	private ArrayList cursors;
	public bool done;
	public Vector3 initialLocation;
	
	public static SpeechBubble mainBubble;
	
	public static bool inFreezeState;

	// Use this for initialization
	void Start () {
		mainBubble = this;
		text = transform.Find ("Text").GetComponent<Text>();
		text.text = "";
		bubble = GetComponent<RectTransform>();
		GameObject speech = GameObject.Find ("Speech");
		transform.parent = speech.transform;
		arrow = transform.Find ("Arrow").gameObject;
		inFreezeState = freezesGameOnDisplay;
		cursors = new ArrayList();
		textToDisplay = new string[] { };
		initialLocation = transform.position;
		transform.position = new Vector3 (9999, 9999, 9999);
	}
	
	public void Activate(){
		inFreezeState = freezesGameOnDisplay;
		GameController.Freeze ();
		transform.position = initialLocation;
	}
	
	// Update is called once per frame
	void Update () {
		currentTimeBetweenCharacters += Time.deltaTime;
		if (Initialized ()) {
			transform.position = initialLocation;
			if (currentTimeBetweenCharacters >= maxTimeBetweenCharacters && textIndex < textToDisplay [textBubbleIndex].Length) {
				GameController.Freeze ();
				done = false;
				text.text += textToDisplay [textBubbleIndex] [textIndex];
				textIndex++;
				currentTimeBetweenCharacters = 0;
				if (!isMainBubble) {
					if (setWidth == 0 || setHeight == 0) {
						if (maxWidth < text.preferredWidth + 40)
							maxWidth = text.preferredWidth + 40;
						if (maxHeight < text.preferredHeight + 30)
							maxHeight = text.preferredHeight + 30;
					} else {
						maxWidth = setWidth;
						maxHeight = setHeight;
					}
					bubble.sizeDelta = new Vector2 (maxWidth, maxHeight);
				}
			}
			if (dismissesSelf && Finished ()) {
				if (currentPostFinishDelay > maxPostFinishDelay) {
					DismissMe ();
				} else {
					currentPostFinishDelay += Time.deltaTime;		
				}
			}
			UpdateArrow ();
		}
	}
	
	void UpdateArrow(){
		arrow.SetActive (DoneWithPage () && !Finished ());
	}


	public static void AddMessage(string message, bool hasEvent){
		Array.Resize (ref mainBubble.textToDisplay, mainBubble.textToDisplay.Length + 1);
		Array.Resize (ref mainBubble.hasEvents, mainBubble.textToDisplay.Length + 1);
		mainBubble.textToDisplay[mainBubble.textToDisplay.Length - 1] = message;
		mainBubble.hasEvents[mainBubble.textToDisplay.Length - 1] = hasEvent;
	}

	public static void AddMessage(string message){
		AddMessage (message, false);
	}
	
	public void DismissMe(){
		if(dismissable && Finished()){
			done = true;
			textIndex = 0;
			textBubbleIndex = 0;
			text.text = "";
			if(freezesGameOnDisplay) inFreezeState = false;
			textToDisplay = new string[] {};
			transform.position = new Vector3(9999, 9999, 9999);
			GameController.Unfreeze ();
		}
	}
	
	public void AdvanceMe(){
		if(Finished()){
			DismissMe ();
		}else if(DoneWithPage ()){
			ShowNextPage();
		}
	}
	
	private void ShowNextPage(){
		textIndex = 0;
		textBubbleIndex++;
		text.text = "";
		foreach(GameObject cursor in cursors){
			Destroy (cursor);
		}
		cursors.Clear ();
	}	
	private bool DoneWithPage(){
		return(textIndex >= textToDisplay[textBubbleIndex].Length);
	}
	
	public bool Finished(){
		return(textBubbleIndex == (textToDisplay.Length - 1) && DoneWithPage ());
	}

	public bool Initialized(){
		return(textToDisplay.Length > 0);
	}
}
