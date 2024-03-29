﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class TimerControllerScript : MonoBehaviour {

	//How many seconds does this timer last?
	public int countTime = 300;
	public Transform doom;

	private Text textBox;
	private float timeRemaining;
	private bool spawned = false;

	string MakeTextString(int t)
	{
		if (t <= 0)
			return "DOOM";
		int a = t / 60;
		int b = t % 60;
		string partA = a.ToString ();
		string partB = b.ToString ();
		if (partA.Length < 2)
			partA = string.Concat("0", partA);
		if (partB.Length < 2)
			partB = "0" + partB;
		return string.Concat (partA, ":", partB);
	}

	// Use this for initialization
	void Start () {
		textBox = GetComponent<Text> ();
		timeRemaining = countTime;
	}
	
	// Update is called once per frame
	void Update () {
		timeRemaining -= Time.deltaTime;

		textBox.text = MakeTextString (Mathf.CeilToInt (timeRemaining));
		if (timeRemaining < 30.0f) {
			textBox.color = Color.red;
		}
		if (timeRemaining < 0.0f && spawned == false)
		{
			Instantiate (doom, GameObject.FindGameObjectWithTag ("Player").transform.position + new Vector3 (0, 1900, -1), Quaternion.identity);
			spawned = true;
		}
	}
}
