using UnityEngine;
using System.Collections;

public class BackgroundColourScript : MonoBehaviour {

	public Color start;
	public Color middle;
	public Color end;
	public float timeMiddle = 270.0f;
	public float timeEnd = 300.0f;
	private SpriteRenderer sprite;
	private float timePassed = 0.0f;

	// Use this for initialization
	void Start () {
		sprite = gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		timePassed += Time.deltaTime;
		if (timePassed < timeMiddle) {
			Color color = start;
			color = Color.Lerp(start, middle, timePassed/timeMiddle);
			sprite.color = color;
		} else {
			Color color = middle;
			color = Color.Lerp (middle, end, (timePassed-timeMiddle)/(timeEnd-timeMiddle));
			sprite.color = color;
		}
	}
}
