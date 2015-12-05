using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private enum playerState {STATE_GROUND, STATE_JUMP_UP, STATE_JUMP_DOWN, STATE_WALL_UP, STATE_WALL_DOWN};

	private playerState currentState;

	public float speedMod = 5.0f;

	public bool onGround = false;

	void TimetoDie()
	{
		//NYI
		//Spawn death splat and remove player.
		//Make sure to detach camera.
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		float xInput = Input.GetAxis ("Horizontal");
		float jumpAxis = Input.GetAxis ("Vertical");
		bool jumpInput = (jumpAxis >= 0.5f);
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();
		Vector2 vec;

		switch (currentState) {
		case playerState.STATE_GROUND:
			vec = new Vector2 (xInput * speedMod, 0);
			rb.AddForce (vec);
			if (jumpInput)
			{
				rb.velocity += new Vector2(0, 12);
				currentState = playerState.STATE_JUMP_UP;
			}
			break;
		case playerState.STATE_JUMP_UP:
			vec = new Vector2 (xInput * speedMod * 0.5f, 0);
			rb.AddForce (vec);
			if (rb.velocity.y <= 0.0f)
				currentState = playerState.STATE_JUMP_DOWN;
			break;
		case playerState.STATE_JUMP_DOWN:
			vec = new Vector2 (xInput * speedMod * 0.5f, 0);
			rb.AddForce (vec);
			break;
		case playerState.STATE_WALL_UP:
			//NYI
			if (rb.velocity.y <= 0.0f)
				currentState = playerState.STATE_WALL_DOWN;
			break;
		case playerState.STATE_WALL_DOWN:
			//NYI
			break;
		default:
			StaticUtils.PANIC("Illegal State in playerController.currentState!");
			break;
		}
		if (gameObject.transform.position.y < -400) {
			//DIE!
		}

	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (GetComponent<Rigidbody2D> ().velocity.magnitude > 25.0f)
			TimetoDie ();
		Ray myRay = gameObject.transform.position - collision.gameObject.transform.position;
		RaycastHit rayHit;
		Physics.Raycast (myRay, out rayHit);
		Vector3 normal = rayHit.normal;
		normal = rayHit.transform.TransformDirection (normal);
		if (normal == rayHit.transform.up) {
			currentState = playerState.STATE_GROUND;
		}
		if (normal == rayHit.transform.right) {
			//currentState = playerState.STATE_WALL_UP
		}
		if (normal == -rayHit.transform.up) {
			//hit ceiling
		}
		if (normal == -rayHit.transform.right) {
			//currentState = playerState.STATE_WALL_DOWN
		}



	}


}
