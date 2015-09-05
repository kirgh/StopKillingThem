using System;
using UnityEngine;
using System.Collections.Generic;

namespace LocationGeneration
{
	public class HousesGenerator : MonoBehaviour
	{

		public TiledSpriteSettings wallTiles;
		public TiledSpriteSettings roofTiles;
		public TiledSpriteSettings shadowTiles;
		public TiledSpriteSettings groundToWallTiles;

		public int minHousesCount;
		public int maxHousesCount;
		public int minHouseWidth;
		public int maxHouseWidth;
		public int minHouseHeight;
		public int maxHouseHeight;
		public int maxInvisibleVertical = 0;
		public int maxInvisibleHorizontal = 0;
		public int emptyBorder = 1;
		

		public GameObject CreateHouses (Main main)
		{
			GameObject houses = new GameObject ("Houses");
			int housesCount = UnityEngine.Random.Range (minHousesCount, maxHousesCount + 1);
			for (int i = 0; i < housesCount; i++) {
				GameObject house = TryGenerateHouse (main);
				if (house != null) {
					house.transform.SetParent (houses.transform, true);
				}
			}
			return houses;
		}

		private GameObject TryGenerateHouse (Main main)
		{
			int width = UnityEngine.Random.Range (minHouseWidth, maxHouseWidth);
			int height = UnityEngine.Random.Range (minHouseHeight, maxHouseHeight) + 1; //+1 for roof
			Point<int> point = main.Tiles.GetFreeRandomPoint (width, height, maxInvisibleHorizontal, maxInvisibleVertical, emptyBorder);
			if (point == null) {
				Debug.Log("Cannot find place for house ("  + width + ", " + height +")");
				return null;
			}

			GameObject house = new GameObject ("House (" + width + ", " + height + ")");
			{
				int sortOrder = LocationSortOrders.GetLocationObjectSortOrder (point.y * main.TileSize);

				GameObject walls = TiledSprite.Create (wallTiles);
				walls.GetComponent<TiledSprite> ()
					.SetTiles (width, height - 1)
					.SetTileSize (main.TileSize, main.TileSize)
					.SetSortOrder (sortOrder);
				walls.transform.SetParent (house.transform, true);

				GameObject roof = TiledSprite.Create (roofTiles);
				roof.GetComponent<TiledSprite> ()
					.SetTiles (width, 1)
					.SetTileSize (main.TileSize, main.TileSize)
					.SetSortOrder (sortOrder);
				roof.transform.SetParent (house.transform, true);
				roof.transform.localPosition = new Vector3 (0, (height - 1) * main.TileSize, 0);

				GameObject shadow = TiledSprite.Create (shadowTiles);
				shadow.GetComponent<TiledSprite> ()
					.SetTiles (width, 1)
					.SetTileSize (main.TileSize, main.TileSize)
					.SetSortOrder (sortOrder + 1);
				shadow.transform.SetParent (house.transform, true);
				shadow.transform.localPosition = new Vector3 (0, (height - 2) * main.TileSize, 0);

				GameObject grass = TiledSprite.Create (groundToWallTiles);
				grass.GetComponent<TiledSprite> ()
					.SetTiles (width, 1)
					.SetTileSize (main.TileSize, main.TileSize)
					.SetSortOrder (sortOrder + 1);
				grass.transform.SetParent (house.transform, true);
			}

			main.Tiles.SetRect(point.x, point.y, width, height, TileType.House, true);
			house.transform.localPosition = new Vector3 (point.x * main.TileSize, point.y * main.TileSize);
			return house;
		}
	}
}