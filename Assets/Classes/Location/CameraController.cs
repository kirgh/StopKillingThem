using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private static readonly float CameraZ = -10;

	private Camera mainCamera;
	private Main main;
	private bool dirty;

	void Start () {
		main = GetComponent<Main> ();
		mainCamera = Camera.main;
		gameObject.AddComponent<ScreenResizeCapturer> ().ScreenResizeEvent += OnScreenResized;
		Clamp ();
	}

	private void OnScreenResized (object sender, int screenWidth, int screenHeight)
	{
		dirty = true;
	}
	
	void Update () {
		if (!dirty) {
			return;
		}
		Clamp ();
		dirty = false;
	}

   	private void Clamp (){
		mainCamera.transform.position = new Vector3 (main.LocationWidth / 2, main.LocationHeight / 2, CameraZ);
		float screenAspect = (float)Screen.width / Screen.height;
		mainCamera.orthographicSize = 0.5f * Mathf.Max (main.LocationWidth / screenAspect, main.LocationHeight);
	}

}
