using UnityEngine;
using System.Collections;

public class camShake : MonoBehaviour {

	public float shakeScale = 0.0f;
	public float xScale = 0.0f;
	public float yScale = 0.0f;

	public float time = 300.0f;

	private float incPerSec;

	// Use this for initialization
	void Start () {
		incPerSec = 10 / time;
	}
	
	// Update is called once per frame
	void Update () {
		float xOff = shakeScale * Mathf.PerlinNoise (Time.time * xScale, 0.0f);
		float yOff = shakeScale * Mathf.PerlinNoise (0.0f, Time.time * yScale);
		xOff -= shakeScale / 2;
		yOff -= shakeScale / 2;
		var pos = gameObject.transform.localPosition;
		pos.x = xOff;
		pos.y = yOff;
		gameObject.transform.localPosition = pos;

		shakeScale += incPerSec * Time.deltaTime;
		xScale += (incPerSec / 2) * Time.deltaTime;
		yScale += (incPerSec / 2) * Time.deltaTime;

	}
}
