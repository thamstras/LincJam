using UnityEngine;
using System.Collections;

public class GlobalProcBlockList : MonoBehaviour {

	public static GlobalProcBlockList instance;
	public Transform[] prefabList;

	// Use this for initialization
	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		Random.seed = 4579;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Transform getNextBlock()
	{
		return prefabList [Random.Range(0, prefabList.Length-1)];
	}
}
