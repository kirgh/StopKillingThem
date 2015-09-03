using UnityEngine;
using System.Collections;

public class SpriteNamePrinter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (GetComponent<SpriteRenderer> ().sprite.name);
	}
}
