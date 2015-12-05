using UnityEngine;
using System.Collections;

public class camShake : MonoBehaviour {

	public float shakeScale = 1.0f;
	public float xScale = 1.0f;
	public float yScale = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float xOff = shakeScale * Mathf.PerlinNoise (Time.time * xScale, 0.0f);
		float yOff = shakeScale * Mathf.PerlinNoise (0.0f, Time.time * yScale);
		var pos = gameObject.transform.localPosition;
		pos.x = xOff;
		pos.y = yOff;
		gameObject.transform.localPosition = pos;
	}
}
