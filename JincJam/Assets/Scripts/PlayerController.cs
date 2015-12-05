using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speedMod = 5.0f;

	public bool onGround = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		float xInput = Input.GetAxis ("Horizontal");
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();
		if (onGround) {
			Vector2 vec = new Vector2 (xInput * speedMod, 0);
			rb.AddForce (vec);
		} else {
			Vector2 vec = new Vector2 (xInput * speedMod * 0.5f, 0);
			rb.AddForce (vec);
		}
		if (Input.GetAxis ("Vertical") >= 0.5f && onGround) {
			rb.velocity += new Vector2(0, 12);
			onGround = false;
		}
		if (gameObject.transform.position.y < -400) {
			//DIE!
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Ground Object" && gameObject.GetComponent<Rigidbody2D>().velocity.y < 0.5f)
			onGround = true;
	}


}
