using UnityEngine;
using System.Collections;

public class LocationGenerator : MonoBehaviour {

	void Start () {
		CreateLocation ();
	}
	
	void Update () {
	
	}

	private void CreateLocation()
	{
		Main main = GetComponent<Main> ();

		{
			//Grass
			float grassTileSize = 2;

			GameObject grass = TiledSprite.Create (
				TiledSpriteSettings.CreateWithTile("Sprites/Textures/grass_01", true)
			);

			grass.GetComponent<TiledSprite> ()
				.SetTiles (Mathf.CeilToInt (main.LocationWidth / grassTileSize), Mathf.CeilToInt (main.LocationHeight / grassTileSize))
				.SetTileSize (grassTileSize, grassTileSize)
				.ForceUpdate ();
		}

	}
}
