using System;
using UnityEngine;

namespace LocationGeneration
{
	public class GrassGenerator : MonoBehaviour
	{

		public TiledSpriteSettings tiles;

		public GameObject CreateGrass (Main main)
		{
			GameObject grass = TiledSprite.Create (tiles);
			grass.name = "Grass";

		grass.GetComponent<TiledSprite> ()
			.SetTiles (main.Tiles.Width, main.Tiles.Height)
			.SetTileSize (main.TileSize, main.TileSize)
			.SetSortOrder(LocationSortOrders.Grass)
			.ForceUpdate ();

			return grass;
		}
	}
}