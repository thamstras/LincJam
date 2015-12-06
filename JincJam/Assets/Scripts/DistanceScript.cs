using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DistanceScript : MonoBehaviour {

	private GameObject player;
	public float maxDist = 0.0f;
	private Text text;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		text = gameObject.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (player.gameObject == null)
			return;
		if (player.transform.position.x > maxDist)
		{
			maxDist = player.transform.position.x;
		}
		text.text = string.Concat ("Distance Travelled:\n", Mathf.FloorToInt(maxDist/10.0f));
	}
}
