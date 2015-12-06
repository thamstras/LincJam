using UnityEngine;
using System.Collections;

public class DOOMScript : MonoBehaviour {

	public float speed = 5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		var pos = gameObject.transform.position;
		pos.y -= speed * Time.deltaTime;
		gameObject.transform.position = pos;

		var rot = gameObject.transform.rotation.eulerAngles;
		rot.z += 40 * Time.deltaTime;
		gameObject.transform.rotation = Quaternion.Euler (rot);
	}
}
