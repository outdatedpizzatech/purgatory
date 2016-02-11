using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Timeline : MonoBehaviour {

	public List<GameObject> slots = new List<GameObject>();
	public List<Turnable> turnables;
	public static Timeline instance;
	public Canvas canvas;

	// Use this for initialization
	void Start () {
		instance = this;
		foreach (Transform slot in transform) {
			slots.Add (slot.gameObject);
		}
		canvas = GetComponent<Canvas> ();
		Hide ();
	}

	public static void Hide(){
		instance.canvas.enabled = false;
	}

	public static void Show(){
		instance.canvas.enabled = true;
	}

	public static void Generate(){
		Show ();

		List<TimelineStruct> items = new List<TimelineStruct> ();

		foreach (Turnable turnable in instance.turnables) {
			TimelineStruct timelineStruct = new TimelineStruct (turnable.turn, turnable);
			items.Add (timelineStruct);
		}

		List<TimelineStruct> futureItems = new List<TimelineStruct> ();

		for (int g = 0; g < instance.slots.Count; g++) {
			foreach (Turnable turnable in instance.turnables) {
				TimelineStruct timelineStruct = new TimelineStruct (turnable.turn + (turnable.maxTurn * (g + 1)), turnable);
				futureItems.Add (timelineStruct);
			}
		}

		items.AddRange (futureItems);

		items.Sort(delegate(TimelineStruct x, TimelineStruct y)
			{
				return x.turn.CompareTo(y.turn);
			});

		int i = 0;

		foreach (GameObject slot in instance.slots) {
			slot.GetComponent<Image> ().sprite = items[i].turnable.sprite;
			i++;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public struct TimelineStruct
	{
		public float turn;
		public Turnable turnable;

		public TimelineStruct(float inputTurn, Turnable inputTurnable)
		{
			turn = inputTurn;
			turnable = inputTurnable;
		}
	}

}
