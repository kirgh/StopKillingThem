using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	public float LocationWidth{ private set; get; }
	public float LocationHeight{ private set; get; }

	void Start () {
		LocationWidth = 20;
		LocationHeight = 20;
		gameObject.AddComponent<LocationGenerator> ();
		gameObject.AddComponent<CameraController> ();
	}
	
}
