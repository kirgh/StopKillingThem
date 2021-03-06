﻿using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	public float LocationWidthInMeters{ private set; get; }
	public float LocationHeightInMeters{ private set; get; }
	public float TileSize{private set; get;}
	public float SourceGraphicsScale{ private set; get; }
	public LocationTiles Tiles{ private set; get; }
	
	void Awake ()
	{
		TileSize = 2;
		SourceGraphicsScale = TileSize / 0.4f;
		Tiles = new LocationTiles (UnityEngine.Random.Range(7, 12), UnityEngine.Random.Range(5, 10));
		LocationWidthInMeters = Tiles.Width * TileSize;
		LocationHeightInMeters = Tiles.Height * TileSize;
	}

	void Start()
	{
		GetComponent<LocationGeneration.LocationGenerator> ().Generate ();
	}

	public void Restart()
	{
		Application.LoadLevel (Application.loadedLevel);
	}
	
}
