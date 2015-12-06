using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnQuitClicked() {
		Application.Quit ();
	}

	public void onCreditsClicked() {
		Application.LoadLevel ("CreditsScene");
	}

	public void OnStartClicked() {
		Application.LoadLevel ("MainScene");
	}
}
