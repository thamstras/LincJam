using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public enum playerState {STATE_GROUND, STATE_JUMP_UP, STATE_JUMP_DOWN, STATE_WALL_UP, STATE_WALL_DOWN, STATE_WALL_TRANSITION};
	private enum wallJump {JUMP_LEFT, JUMP_RIGHT};

	public playerState currentState;
	private wallJump wallJumpDirection;

	public Transform deathSplat;

	public float speedMod = 5.0f;

	public bool onGround = false;
	public bool facingRight = true;
	public bool running = false;

	private Rigidbody2D rb;
	private Animator anim;

	void TimetoDie()
	{
		//NYI
		//Spawn death splat and remove player.
		//Make sure to detach camera.
		//Camera.main.gameObject.GetComponent<camShake> ().enabled = false;
		gameObject.transform.DetachChildren ();
		//Camera.main.transform.position = gameObject.transform.position;
		Instantiate (deathSplat, gameObject.transform.position, Quaternion.identity);
		Destroy (gameObject);
	}

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		float xInput = Input.GetAxis ("Horizontal");
		float jumpAxis = Input.GetAxis ("Vertical");
		bool jumpInput = (jumpAxis >= 0.5f);
		Vector2 vec;

		if (rb.velocity.x < 0.0f && facingRight) {
			anim.SetBool ("Right", false);
			facingRight = false;
			Vector3 scale = gameObject.transform.localScale;
			scale.x *= -1.0f;
			gameObject.transform.localScale = scale;
		} else if (rb.velocity.x > 0 && !facingRight){
			anim.SetBool ("Right", true);
			facingRight = true;
			Vector3 scale = gameObject.transform.localScale;
			scale.x *= -1.0f;
			gameObject.transform.localScale = scale;
		}

		switch (currentState) {
		case playerState.STATE_GROUND:
			if (Mathf.Abs(rb.velocity.y) > 5.0f)
			{
				currentState = playerState.STATE_JUMP_UP;
				anim.SetTrigger ("Jump");
			}
			vec = new Vector2 (xInput * speedMod, 0);
			if (vec.x < 0 && rb.velocity.x > 0)
			{
				vec = new Vector2(2*xInput*speedMod, 0);
			}
			if (vec.x > 0 && rb.velocity.x < 0)
			{
				vec = new Vector2(2*xInput*speedMod, 0);
			}
			if (Mathf.Abs(xInput) > 0.5f){
				if (!running){
					running = true;
					anim.SetTrigger("StartRun");
				}
			} else {
				if (Mathf.Abs (rb.velocity.x) < 0.5f && running)
				{
					running = false;
					anim.SetTrigger("StopRun");
				}
			}
			rb.AddForce (vec);
			if (jumpInput)
			{
				rb.velocity += new Vector2(0, 20);
				currentState = playerState.STATE_JUMP_UP;
				anim.SetTrigger("Jump");
				running = false;
			}
			break;
		case playerState.STATE_JUMP_UP:
			vec = new Vector2 (xInput * speedMod * 0.2f, 0);
			rb.AddForce (vec);
			if (rb.velocity.y <= 0.0f){
				currentState = playerState.STATE_JUMP_DOWN;
				anim.SetTrigger("Fall");
				running = false;
			}
			break;
		case playerState.STATE_JUMP_DOWN:
			vec = new Vector2 (xInput * speedMod * 0.2f, 0);
			rb.AddForce (vec);
			break;
		case playerState.STATE_WALL_UP:
			if (rb.velocity.y <= 0.0f){
				currentState = playerState.STATE_WALL_DOWN;
				anim.SetTrigger("WallSlide");
				running = false;
			}
			if (wallJumpDirection == wallJump.JUMP_LEFT)
			{
				if (xInput > 0)
				{
					rb.velocity += new Vector2 (7, 7);
					currentState = playerState.STATE_JUMP_UP;
					anim.SetTrigger("Jump");
				}
			} else {
				if (xInput < 0)
				{
					rb.velocity += new Vector2 (-7, 7);
					currentState = playerState.STATE_JUMP_UP;
					anim.SetTrigger("Jump");
					running = false;
				}
			}
			break;
		case playerState.STATE_WALL_DOWN:
			if (rb.velocity.y < -15.0f){
				rb.velocity += new Vector2 ((wallJumpDirection==wallJump.JUMP_LEFT)?0.5f:-0.5f, 0);
				currentState = playerState.STATE_JUMP_DOWN;
				anim.SetTrigger("Jump");
				anim.SetTrigger ("Fall");
				running = false;
			}
			if (wallJumpDirection == wallJump.JUMP_LEFT)
			{
				if (xInput > 0)
				{
					rb.velocity += new Vector2 (7, 7);
					currentState = playerState.STATE_JUMP_UP;
					anim.SetTrigger("Jump");
					running = false;
				}
			} else {
				if (xInput < 0)
				{
					rb.velocity += new Vector2 (-7, 7);
					currentState = playerState.STATE_JUMP_UP;
					anim.SetTrigger ("Jump");
					running = false;
				}
			}
			break;
		case playerState.STATE_WALL_TRANSITION:
			if (rb.velocity.x > 0)
			{
				wallJumpDirection = wallJump.JUMP_LEFT;
			}
			else {
				wallJumpDirection = wallJump.JUMP_RIGHT;
			}
			rb.velocity = new Vector2(0, (0.2f * Mathf.Abs (rb.velocity.x)));
			if (rb.velocity.y > 0){
				currentState = playerState.STATE_WALL_UP;
				anim.SetTrigger("HitWall");
				running = false;
			}
			else {
				currentState = playerState.STATE_WALL_DOWN;
				anim.SetTrigger("HitWall");
				anim.SetTrigger ("WallSlide");
				running = false;
			}
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
		//if (rb.velocity.magnitude > 25.0f)
		//	TimetoDie ();

		var normal = collision.contacts [0].normal;
		normal = collision.gameObject.transform.InverseTransformDirection (normal);
		//Debug.Log (normal.ToString ());
		if (normal == new Vector2(0, 1)) {
			//Debug.Log ("Hit top");
			currentState = playerState.STATE_GROUND;
			anim.SetTrigger ("Landed");
			running = false;
		}
		if (normal == new Vector2(1, 0)) {
			//Debug.Log ("Hit right");
			//currentState = playerState.STATE_WALL_UP
			if (rb.velocity.x > 0){
				wallJumpDirection = wallJump.JUMP_LEFT;
			}
			else {
				wallJumpDirection = wallJump.JUMP_RIGHT;
			}
			rb.velocity = new Vector2(0.0f, rb.velocity.y + (0.2f * Mathf.Abs (rb.velocity.x)));
			currentState = playerState.STATE_WALL_UP;
			anim.SetTrigger("HitWall");
			running = false;
		}
		if (normal == new Vector2(0, -1)) {
			//Debug.Log("Hit bottom");
			//hit ceiling
		}
		if (normal == new Vector2(-1, 0)) {
			//Debug.Log("Hit left");
			//currentState = playerState.STATE_WALL_DOWN
			if (rb.velocity.x > 0) {
				wallJumpDirection = wallJump.JUMP_LEFT;
			}
			else {
				wallJumpDirection = wallJump.JUMP_RIGHT;
			}
			rb.velocity = new Vector2(0, rb.velocity.y + (0.2f * Mathf.Abs (rb.velocity.x)));
			currentState = playerState.STATE_WALL_UP;
			anim.SetTrigger("HitWall");
			running = false;
		}

	}

	void OnCollisionExit2D()
	{
		if (currentState == playerState.STATE_WALL_DOWN || currentState == playerState.STATE_WALL_UP) {
			//Debug.Log ("Slid off wall?");
			//currentState = (rb.velocity.y > 0) ? playerState.STATE_JUMP_UP : playerState.STATE_JUMP_DOWN;
			//anim.SetTrigger((rb.velocity.y > 0) ? "Jump" : "Fall");
			if (rb.velocity.y > 0) {
				currentState = playerState.STATE_JUMP_UP;
				anim.SetTrigger ("Jump");
				running = false;
			} else {
				currentState = playerState.STATE_JUMP_DOWN;
				anim.SetTrigger ("Jump");
				anim.SetTrigger ("Fall");
				running = false;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Doom") {
			Debug.Log ("DOOMED");
			TimetoDie();
			//DIE!
		}
	}

}
