using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private enum playerState {STATE_GROUND, STATE_JUMP_UP, STATE_JUMP_DOWN, STATE_WALL_UP, STATE_WALL_DOWN, STATE_WALL_TRANSITION};
	private enum wallJump {JUMP_LEFT, JUMP_RIGHT};

	private playerState currentState;
	private wallJump wallJumpDirection;

	public Transform deathSplat;

	public float speedMod = 5.0f;

	public bool onGround = false;

	private Rigidbody2D rb;

	void TimetoDie()
	{
		//NYI
		//Spawn death splat and remove player.
		//Make sure to detach camera.
		/*var velAngle = Mathf.Atan (rb.velocity.y / rb.velocity.x);
		Quaternion q = Quaternion.Euler (0, 0, velAngle);
		var ds = (Transform)Instantiate (deathSplat, gameObject.transform.position, q);

		var cam = Camera.main;
		gameObject.transform.DetachChildren ();
		cam.transform.position = gameObject.transform.position;
		Destroy (gameObject);*/
	}

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		float xInput = Input.GetAxis ("Horizontal");
		float jumpAxis = Input.GetAxis ("Vertical");
		bool jumpInput = (jumpAxis >= 0.5f);
		Vector2 vec;

		switch (currentState) {
		case playerState.STATE_GROUND:
			vec = new Vector2 (xInput * speedMod, 0);
			rb.AddForce (vec);
			if (jumpInput)
			{
				rb.velocity += new Vector2(0, 15);
				currentState = playerState.STATE_JUMP_UP;
			}
			break;
		case playerState.STATE_JUMP_UP:
			vec = new Vector2 (xInput * speedMod * 0.2f, 0);
			rb.AddForce (vec);
			if (rb.velocity.y <= 0.0f)
				currentState = playerState.STATE_JUMP_DOWN;
			break;
		case playerState.STATE_JUMP_DOWN:
			vec = new Vector2 (xInput * speedMod * 0.2f, 0);
			rb.AddForce (vec);
			break;
		case playerState.STATE_WALL_UP:
			if (rb.velocity.y <= 0.0f)
				currentState = playerState.STATE_WALL_DOWN;
			if (wallJumpDirection == wallJump.JUMP_LEFT)
			{
				if (xInput > 0)
				{
					rb.velocity += new Vector2 (7, 7);
					currentState = playerState.STATE_JUMP_UP;
				}
			} else {
				if (xInput < 0)
				{
					rb.velocity += new Vector2 (-7, 7);
					currentState = playerState.STATE_JUMP_UP;
				}
			}
			break;
		case playerState.STATE_WALL_DOWN:
			if (rb.velocity.y < -15.0f){
				rb.velocity += new Vector2 ((wallJumpDirection==wallJump.JUMP_LEFT)?0.5f:-0.5f, 0);
				currentState = playerState.STATE_JUMP_DOWN;
			}
			if (wallJumpDirection == wallJump.JUMP_LEFT)
			{
				if (xInput > 0)
				{
					rb.velocity += new Vector2 (7, 7);
					currentState = playerState.STATE_JUMP_UP;
				}
			} else {
				if (xInput < 0)
				{
					rb.velocity += new Vector2 (-7, 7);
					currentState = playerState.STATE_JUMP_UP;
				}
			}
			break;
		case playerState.STATE_WALL_TRANSITION:
			if (rb.velocity.x > 0)
				wallJumpDirection = wallJump.JUMP_LEFT;
			else 
				wallJumpDirection = wallJump.JUMP_RIGHT;
			rb.velocity = new Vector2(0, (0.2f * Mathf.Abs (rb.velocity.x)));
			if (rb.velocity.y > 0)
				currentState = playerState.STATE_WALL_UP;
			else
				currentState = playerState.STATE_WALL_DOWN;
			break;
		default:
			StaticUtils.PANIC("Illegal State in playerController.currentState!");
			break;
		}
		if (gameObject.transform.position.y < -400) {
			TimetoDie ();
		}

	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (rb.velocity.magnitude > 25.0f)
			TimetoDie ();

		var normal = collision.contacts [0].normal;
		if (normal == new Vector2(0, 1)) {
			Debug.Log ("Hit top");
			currentState = playerState.STATE_GROUND;
		}
		if (normal == new Vector2(1, 0)) {
			Debug.Log ("Hit right");
			//currentState = playerState.STATE_WALL_UP
			if (rb.velocity.x > 0)
				wallJumpDirection = wallJump.JUMP_LEFT;
			else 
				wallJumpDirection = wallJump.JUMP_RIGHT;
			rb.velocity = new Vector2(0.0f, rb.velocity.y + (0.2f * Mathf.Abs (rb.velocity.x)));
			currentState = playerState.STATE_WALL_UP;
		}
		if (normal == new Vector2(0, -1)) {
			Debug.Log("Hit bottom");
			//hit ceiling
		}
		if (normal == new Vector2(-1, 0)) {
			Debug.Log("Hit left");
			//currentState = playerState.STATE_WALL_DOWN
			if (rb.velocity.x > 0)
				wallJumpDirection = wallJump.JUMP_LEFT;
			else 
				wallJumpDirection = wallJump.JUMP_RIGHT;
			rb.velocity = new Vector2(0, rb.velocity.y + (0.2f * Mathf.Abs (rb.velocity.x)));
			currentState = playerState.STATE_WALL_UP;
		}

	}

	void OnCollisionExit2D()
	{
		if (currentState == playerState.STATE_WALL_DOWN || currentState == playerState.STATE_WALL_UP) {
			Debug.Log ("Slid off wall?");
			currentState = (rb.velocity.y > 0) ? playerState.STATE_JUMP_UP : playerState.STATE_JUMP_DOWN;
		}
	}


}
