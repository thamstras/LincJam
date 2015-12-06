using UnityEngine;
using System.Collections;

public class CreditScroller : MonoBehaviour {

	public float speed = 5.0f;
	public float maxTime = 17.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = gameObject.transform.position;
		pos.y += speed * Time.deltaTime;
		gameObject.transform.position = pos;

		if (Time.timeSinceLevelLoad > maxTime) {
			Application.LoadLevel ("MenuScene");
		}
	}
}
