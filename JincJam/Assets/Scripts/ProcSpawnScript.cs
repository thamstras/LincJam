using UnityEngine;
using System.Collections;

public class ProcSpawnScript : MonoBehaviour {

	private bool spawned = false;

	bool onScreen()
	{
		var bounds = gameObject.GetComponent<SpriteRenderer> ().bounds;
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes (Camera.main);
		if (GeometryUtility.TestPlanesAABB (planes, bounds))
			return true;
		else
			return false;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (!spawned) {
			if (onScreen())
			{
				Debug.Log("Flag on!");
				var procMan = GlobalProcBlockList.instance;
				Transform nextBlock = procMan.getNextBlock();
				nextBlock.position = gameObject.transform.position;
				Instantiate(nextBlock, gameObject.transform.position, gameObject.transform.rotation);
				spawned = true;
			}
		}
	}
}
