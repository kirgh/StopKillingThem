using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	public float LocationWidthInMeters{ private set; get; }
	public float LocationHeightInMeters{ private set; get; }
	public float TileSize{private set; get;}
	public LocationTiles Tiles{ private set; get; }
	


	void Start () {
		TileSize = 2;
		Tiles = new LocationTiles (12, 10);
		LocationWidthInMeters = Tiles.Width * TileSize;
		LocationHeightInMeters = Tiles.Height * TileSize;
		gameObject.AddComponent<LocationGeneration.LocationGenerator> ();
		gameObject.AddComponent<CameraController> ();
	}
	
}
