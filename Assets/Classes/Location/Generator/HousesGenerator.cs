using System;
using UnityEngine;
using System.Collections.Generic;

namespace LocationGeneration
{
	public class HousesGenerator
	{

		private static readonly int MaxInvisibleVertical = 1;
		private static readonly int MaxInvisibleHorizontal = 1;
		private static readonly int EmptyBorder = 1;

		public int minHousesCount;
		public int maxHousesCount;
		public int minHouseWidth;
		public int maxHouseWidth;
		public int minHouseHeight;
		public int maxHouseHeight;

		public HousesGenerator ()
		{
		}

		public void CreateHouses (Main main)
		{
			GameObject houses = new GameObject ("Houses");
			int housesCount = UnityEngine.Random.Range (minHousesCount, maxHousesCount);
			for (int i = 0; i < housesCount; i++) {
				GameObject house = TryGenerateHouse (main);
				if (house != null) {
					house.transform.SetParent (houses.transform, true);
				}
			}
		}

		private GameObject TryGenerateHouse (Main main)
		{
			int width = UnityEngine.Random.Range (minHouseWidth, maxHouseWidth);
			int height = UnityEngine.Random.Range (minHouseHeight, maxHouseHeight) + 1; //+1 for roof
			Point<int> point = GetPointForHouse (main, width, height);
			if (point == null) {
				Debug.Log("Cannot find place for house ("  + width + ", " + height +")");
				return null;
			}

			GameObject house = new GameObject ("House (" + width + ", " + height + ")");
			{
				int sortOrder = LocationSortOrders.GetLocationObjectSortOrder (point.y * main.TileSize);

				GameObject walls = TiledSprite.Create (TiledSpriteSettings.CreateWithTile ("Sprites/Textures/wall_01", false));
				walls.GetComponent<TiledSprite> ()
					.SetTiles (width, height - 1)
					.SetTileSize (main.TileSize, main.TileSize)
					.SetSortOrder (sortOrder);
				walls.transform.SetParent (house.transform, true);

				GameObject roof = TiledSprite.Create (TiledSpriteSettings.CreateWithTile ("Sprites/Textures/roof_01", false));
				roof.GetComponent<TiledSprite> ()
					.SetTiles (width, 1)
					.SetTileSize (main.TileSize, main.TileSize)
					.SetSortOrder (sortOrder);
				roof.transform.SetParent (house.transform, true);
				roof.transform.localPosition = new Vector3 (0, (height - 1) * main.TileSize, 0);

				GameObject shadow = TiledSprite.Create (TiledSpriteSettings.CreateWithTile ("Sprites/Textures/roof_shadow_01", false));
				shadow.GetComponent<TiledSprite> ()
					.SetTiles (width, 1)
					.SetTileSize (main.TileSize, main.TileSize)
					.SetSortOrder (sortOrder);
				shadow.transform.SetParent (house.transform, true);
				shadow.transform.localPosition = new Vector3 (0, (height - 2) * main.TileSize, 0);

				GameObject grass = TiledSprite.Create (TiledSpriteSettings.CreateWithTile ("Sprites/Textures/grass_01_to_buildings_01", false));
				grass.GetComponent<TiledSprite> ()
					.SetTiles (width, 1)
					.SetTileSize (main.TileSize, main.TileSize)
					.SetSortOrder (sortOrder);
				grass.transform.SetParent (house.transform, true);
			}

			main.Tiles.SetRect(point.x, point.y, width, height, TileType.House, true);
			house.transform.localPosition = new Vector3 (point.x * main.TileSize, point.y * main.TileSize);
			return house;
		}

		private Point<int> GetPointForHouse (Main main, int width, int height)
		{
			List<Point<int>> availablePoints = new List<Point<int>> ();
			for (int x = Mathf.Max(-MaxInvisibleHorizontal, 1 - width); x < main.Tiles.Width + 1 - Mathf.Max(1, width - MaxInvisibleHorizontal); x++) {
				for (int y = Mathf.Max(-MaxInvisibleVertical, 1 - height); y < main.Tiles.Height + 1 - Mathf.Max(1, height - MaxInvisibleVertical); y++) {
					if (main.Tiles.IsEmptyRect (x - EmptyBorder, y - EmptyBorder, width + EmptyBorder * 2, height + EmptyBorder * 2, true)) {
						availablePoints.Add (new Point<int> (x, y));
					}
				}
			}
			return availablePoints.Count > 0 ? availablePoints [UnityEngine.Random.Range (0, availablePoints.Count)] : null;
		}

	}
}