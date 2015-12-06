using UnityEngine;
using System.Collections;

public class hidescript : MonoBehaviour {

	bool done = false;

	// Use this for initialization
	void Start () {
		GetComponent<Canvas> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!done) {
			gameObject.GetComponent<Canvas> ().enabled = false;
			done = true;
		}
	}
}
