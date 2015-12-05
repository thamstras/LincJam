using UnityEngine;
using System.Collections;

public class StaticUtils : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void PANIC(string msg)
	{
		Debug.LogError(string.Concat("HELP! It's all gone wrong! ", msg));
	}
}
