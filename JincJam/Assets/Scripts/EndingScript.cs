using UnityEngine;
using System.Collections;

public class EndingScript : MonoBehaviour {

	public float waitTime = 30.0f;
	
	private float endTime;

	// Use this for initialization
	void Start () {
		endTime = Time.time + waitTime;
		var endScreen = GameObject.FindGameObjectWithTag ("End Screen");
		endScreen.GetComponent<Canvas> ().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > endTime) {
			Application.LoadLevel ("MenuScene");
		}
	}
}
