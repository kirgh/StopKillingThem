using System;
using UnityEngine;

public class GrassGenerator
{
	public GrassGenerator ()
	{
	}

	public GameObject CreateGrass(Main main)
	{
		GameObject grass = TiledSprite.Create (
			TiledSpriteSettings.CreateWithTile("Sprites/Textures/grass_01", true)
			);

		grass.name = "Grass";

		grass.GetComponent<TiledSprite> ()
			.SetTiles (main.Tiles.Width, main.Tiles.Height)
			.SetTileSize (main.TileSize, main.TileSize)
			.SetSortOrder(LocationSortOrders.Grass)
			.ForceUpdate ();

		return grass;
	}
}