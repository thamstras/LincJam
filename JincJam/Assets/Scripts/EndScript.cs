using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndScript : MonoBehaviour {

	public DistanceScript dist;
	public float waitTime;

	private float endTime;
	private Text text;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		endTime = Time.time + waitTime;
	}
	
	// Update is called once per frame
	void Update () {
		string newText = string.Concat ("You made it:\n", Mathf.FloorToInt(dist.maxDist/10.0f), "m\nBefore the balance of light and darkness\noverwhelmed you!");
		text.text = newText;
	}
}
