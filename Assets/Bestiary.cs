using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Bestiary : MonoBehaviour {
	public IDictionary<int, List<string>> baddies = new Dictionary<int, List<string>>();
	public static Bestiary instance;

	// Use this for initialization
	void Start () {
		instance = this;
		for (int i = 0; i <= 100; i++) {
			baddies [i] = new List<string> ();
		}

		DirectoryInfo dir = new DirectoryInfo("Assets/Resources/Baddies");
		FileInfo[] info = dir.GetFiles("*.prefab");
		foreach (FileInfo f in info) 
		{ 
			string baddieName = f.Name.Replace(".prefab", "");
			GameObject baddieObject = Instantiate(Resources.Load ("Baddies/" + baddieName), Vector3.zero, Quaternion.identity) as GameObject;
			Baddie baddie = baddieObject.GetComponent<Baddie> ();
			int level = baddie.Level ();
			baddies [level].Add (baddieName);
			Destroy (baddieObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
