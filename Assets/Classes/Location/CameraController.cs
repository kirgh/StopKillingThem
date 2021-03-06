﻿using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private static readonly float CameraZ = -10;

	public Camera mainCamera;
	public Main main;
	private bool dirty;

	void Start () {
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
		mainCamera.transform.position = new Vector3 (main.LocationWidthInMeters / 2, main.LocationHeightInMeters / 2, CameraZ);
		float screenAspect = (float)Screen.width / Screen.height;
		mainCamera.orthographicSize = 0.5f * Mathf.Max (main.LocationWidthInMeters / screenAspect, main.LocationHeightInMeters);
	}

}
