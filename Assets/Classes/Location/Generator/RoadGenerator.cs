using System;
using UnityEngine;
using System.Collections.Generic;

namespace LocationGeneration
{
	public class RoadGenerator : MonoBehaviour
	{

		public TiledSpriteSettings tiles;
		public Direction8ToSpritePair[] GrassToRoadCorners;

		public int minRoadsCount;
		public int maxRoadsCount;
		public int minRoadWidth;
		public int maxRoadWidth;

		public GameObject CreateRoads (Main main)
		{
			GameObject roads = new GameObject ("Roads");
			int roadsCount = UnityEngine.Random.Range (minRoadsCount, maxRoadsCount);
			for (int i = 0; i < roadsCount; i++) {
				int roadWidth = UnityEngine.Random.Range (minRoadWidth, maxRoadWidth);
				bool vertical = UnityEngine.Random.value > 0.5f;
				int startX = 0;
				int startY = 0;
				if (vertical) {
					startX = UnityEngine.Random.Range (0, main.Tiles.Width - roadWidth);
					startY = 0;
				} else {
					startX = 0;
					startY = UnityEngine.Random.Range (0, main.Tiles.Height - roadWidth);
				}

				GameObject road = CreateRoad (startX, startY, roadWidth, vertical, main);
				road.transform.SetParent (roads.transform, true);

				int minX = vertical ? startX : 0;
				int maxX = vertical ? (startX + roadWidth) : main.Tiles.Width;
				int minY = vertical ? 0 : startY;
				int maxY = vertical ? main.Tiles.Height : (startY + roadWidth);
			
				main.Tiles.SetRect (minX, minY, maxX - minX, maxY - minY, TileType.Road, false);
			}

			GameObject grassToRoads = CreateGrassToRoadBorders (main);
			grassToRoads.transform.SetParent (roads.transform, true);
			return roads;
		}

		private GameObject CreateRoad (int startX, int startY, int roadWidth, bool vertical, Main main)
		{
			GameObject result = TiledSprite.Create (tiles);

			result.name = "Road (" + (vertical ? "vertical" : "horizontal") + " width " + roadWidth + ")";

			result.GetComponent<TiledSprite> ()
			.SetTiles (vertical ? roadWidth : main.Tiles.Width, vertical ? main.Tiles.Height : roadWidth)
			.SetTileSize (main.TileSize, main.TileSize)
			.SetSortOrder (LocationSortOrders.Roads)
			.ForceUpdate ();

			result.transform.localPosition = new Vector3 (startX * main.TileSize, startY * main.TileSize, 0);
			return result;
		}

		private struct BorderCondition
		{

			private static Dictionary<Direction8, List<BorderCondition>> cachedConditions = new Dictionary<Direction8, List<BorderCondition>> ();

			public static List<BorderCondition> GetBorderConditions(Direction8 direction)
			{
				if (cachedConditions.ContainsKey (direction)) {
					return cachedConditions[direction];
				}
				cachedConditions [direction] = CreateBorderConditions (direction);
				return cachedConditions [direction];
			}

			private static List<BorderCondition> CreateBorderConditions(Direction8 direction)
			{
				switch (direction) {
				case Direction8.Down: return new List<BorderCondition>(){new BorderCondition(0, -1, false)};
				case Direction8.DownLeft: return new List<BorderCondition>(){new BorderCondition(-1, 0, true), new BorderCondition(0, -1, true), new BorderCondition(-1, -1, false)};
				case Direction8.DownRight: return new List<BorderCondition>(){new BorderCondition(1, 0, true), new BorderCondition(0, -1, true), new BorderCondition(1, -1, false)};
				case Direction8.Left: return new List<BorderCondition>(){new BorderCondition(-1, 0, false)};
				case Direction8.Right: return new List<BorderCondition>(){new BorderCondition(1, 0, false)};
				case Direction8.Up: return new List<BorderCondition>(){new BorderCondition(0, 1, false)};
				case Direction8.UpLeft: return new List<BorderCondition>(){new BorderCondition(-1, 0, true), new BorderCondition(0, 1, true), new BorderCondition(-1, 1, false)};
				case Direction8.UpRight: return new List<BorderCondition>(){new BorderCondition(1, 0, true), new BorderCondition(0, 1, true), new BorderCondition(1, 1, false)};
				}
				Debug.LogError ("unknown direction");
				return new List<BorderCondition>();
			}

			private BorderCondition (int xDelta, int yDelta, bool shouldBeRoad)
			{
				this.xDelta = xDelta;
				this.yDelta = yDelta;
				this.shouldBeRoad = shouldBeRoad;
			}

			public int xDelta;
			public int yDelta;
			public bool shouldBeRoad;
		}

		private GameObject CreateGrassToRoadBorders (Main main)
		{
			GameObject grassToRoadsBorders = new GameObject ("GrassToRoadsBorders");
			Transform parentTransform = grassToRoadsBorders.transform;

			for (int x= 0; x < main.Tiles.Width; x++) {
				for (int y= 0; y < main.Tiles.Height; y++) {
					if (main.Tiles.Get (x, y) != TileType.Road) {
						continue;
					}
					foreach (Direction8ToSpritePair border in GrassToRoadCorners) {
						bool needBorder = true;
						List<BorderCondition> conditions = BorderCondition.GetBorderConditions(border.direction);
						for (int i = 0; i < conditions.Count; i++) {
							BorderCondition condition = conditions [i];
							if (!main.Tiles.Contains (x + condition.xDelta, y + condition.yDelta)) {
								needBorder = false;
								break;
							}
							if ((main.Tiles.Get (x + condition.xDelta, y + condition.yDelta) == TileType.Road) != condition.shouldBeRoad) {
								needBorder = false;
								break;
							}
						}
						if (!needBorder) {
							continue;
						}

						GameObject borderObject = new GameObject ();
						borderObject.AddComponent<SpriteRenderer> ().sprite = border.sprite;
						borderObject.GetComponent<SpriteRenderer> ().sortingOrder = LocationSortOrders.GrassToRoadsBorders;
						borderObject.transform.localScale = new Vector3 (main.TileSize / border.sprite.bounds.size.x, main.TileSize / border.sprite.bounds.size.y, 0);
						borderObject.transform.Rotate (0, 0, border.rotation);
						borderObject.transform.SetParent (parentTransform, true);
						borderObject.transform.localPosition = new Vector3 (x * main.TileSize + main.TileSize / 2, y * main.TileSize + main.TileSize / 2, 0);
					}
				}
			}
			return grassToRoadsBorders;
		}
	}
}