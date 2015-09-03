using System;
using UnityEngine;
using System.Collections.Generic;

namespace LocationGeneration{
public class RoadGenerator
{

	private int minRoadsCount;
	private int maxRoadsCount;
	private int minRoadWidth;
	private int maxRoadWidth;

	public RoadGenerator(int minRoadsCount, int maxRoadsCount, int minRoadWidth, int maxRoadWidth)
	{
		this.minRoadsCount = minRoadsCount;
		this.maxRoadsCount = maxRoadsCount;
		this.minRoadWidth = minRoadWidth;
		this.maxRoadWidth = maxRoadWidth;
	}

	public void CreateRoads(Main main)
	{
		GameObject roads = new GameObject ("Roads");
		int roadsCount = UnityEngine.Random.Range(minRoadsCount, maxRoadsCount);
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
			road.transform.SetParent(roads.transform, true);

			int minX = vertical ? startX : 0;
			int maxX = vertical ? (startX + roadWidth) : main.Tiles.Width;
			int minY = vertical ? 0 : startY;
			int maxY = vertical ? main.Tiles.Height : (startY + roadWidth);
			
			main.Tiles.SetRect(minX, minY, maxX - minX, maxY - minY, TileType.Road, false);
		}

		CreateGrassToRoadBorders (main);
	}

	private GameObject CreateRoad(int startX, int startY, int roadWidth, bool vertical, Main main)
	{
		GameObject result = TiledSprite.Create (
			TiledSpriteSettings.CreateWithTile("Sprites/Textures/road_01", true)
			);

		result.name = "Road (" + (vertical ? "vertical" : "horizontal") + " width " + roadWidth + ")";

		result.GetComponent<TiledSprite> ()
			.SetTiles (vertical ? roadWidth : main.Tiles.Width, vertical ? main.Tiles.Height : roadWidth)
			.SetTileSize (main.TileSize, main.TileSize)
			.SetSortOrder(LocationSortOrders.Roads)
			.ForceUpdate ();

		result.transform.localPosition = new Vector3 (startX * main.TileSize, startY * main.TileSize, 0);
		return result;
	}


	private class Border
	{

		static Border(){
			AllBorders = new List<Border>();

			AllBorders.Add(
				new Border{
					conditions = new List<BorderCondition>(){new BorderCondition(-1, 0, false)},
					spriteName = "Sprites/Textures/grass_01_to_roads_01",
					rotation = -90
				}
			);

			AllBorders.Add(
				new Border{
				conditions = new List<BorderCondition>(){new BorderCondition(1, 0, false)},
				spriteName = "Sprites/Textures/grass_01_to_roads_01",
				rotation = 90
			}
			);

			AllBorders.Add(
				new Border{
				conditions = new List<BorderCondition>(){new BorderCondition(0, 1, false)},
				spriteName = "Sprites/Textures/grass_01_to_roads_01",
				rotation = 180
			}
			);

			AllBorders.Add(
				new Border{
				conditions = new List<BorderCondition>(){new BorderCondition(0, -1, false)},
				spriteName = "Sprites/Textures/grass_01_to_roads_01",
				rotation = 0
			}
			);

			AllBorders.Add(
				new Border{
				conditions = new List<BorderCondition>(){new BorderCondition(-1, 0, true), new BorderCondition(0, -1, true), new BorderCondition(-1, -1, false)},
				spriteName = "Sprites/Textures/grass_01_to_roads_corner_01",
				rotation = 0
			}
			);

			AllBorders.Add(
				new Border{
				conditions = new List<BorderCondition>(){new BorderCondition(-1, 0, true), new BorderCondition(0, 1, true), new BorderCondition(-1, 1, false)},
				spriteName = "Sprites/Textures/grass_01_to_roads_corner_01",
				rotation = -90
			}
			);

			AllBorders.Add(
				new Border{
				conditions = new List<BorderCondition>(){new BorderCondition(1, 0, true), new BorderCondition(0, 1, true), new BorderCondition(1, 1, false)},
				spriteName = "Sprites/Textures/grass_01_to_roads_corner_01",
				rotation = 180
			}
			);

			AllBorders.Add(
				new Border{
				conditions = new List<BorderCondition>(){new BorderCondition(1, 0, true), new BorderCondition(0, -1, true), new BorderCondition(1, -1, false)},
				spriteName = "Sprites/Textures/grass_01_to_roads_corner_01",
				rotation = 90
			}
			);
		}

		public void LoadSprites()
		{
			if (sprite != null) {
				return;
			}
			sprite = Resources.Load<Sprite>(spriteName);
		}

		public struct BorderCondition{
			public BorderCondition(int xDelta, int yDelta, bool shouldBeRoad)
			{
				this.xDelta = xDelta;
				this.yDelta = yDelta;
				this.shouldBeRoad = shouldBeRoad;
			}
			public int xDelta;
			public int yDelta;
			public bool shouldBeRoad;
		}

		public static List<Border> AllBorders;
		public List<BorderCondition> conditions;
		private string spriteName;
		public Sprite sprite;
		public float rotation;
	}

	private void CreateGrassToRoadBorders(Main main)
	{
		GameObject grassToRoadsBorders = new GameObject ("GrassToRoadsBorders");
		Transform parentTransform = grassToRoadsBorders.transform;

		for(int x= 0; x < main.Tiles.Width; x++){
			for(int y= 0; y < main.Tiles.Height; y++){
				if(main.Tiles.Get(x, y) != TileType.Road){
					continue;
				}
				foreach(Border border in Border.AllBorders){
					bool needBorder = true;
					for(int i = 0; i < border.conditions.Count; i++){
						Border.BorderCondition condition = border.conditions[i];
						if(!main.Tiles.Contains(x + condition.xDelta, y + condition.yDelta)){
							needBorder = false;
							break;
						}
						if((main.Tiles.Get(x + condition.xDelta, y + condition.yDelta) == TileType.Road) != condition.shouldBeRoad){
							needBorder = false;
							break;
						}
					}
					if(!needBorder){
						continue;
					}

					border.LoadSprites();

					GameObject borderObject = new GameObject();
					borderObject.AddComponent<SpriteRenderer>().sprite = border.sprite;
					borderObject.GetComponent<SpriteRenderer>().sortingOrder = LocationSortOrders.GrassToRoadsBorders;
					borderObject.transform.localScale = new Vector3(main.TileSize / border.sprite.bounds.size.x, main.TileSize / border.sprite.bounds.size.y, 0);
					borderObject.transform.Rotate(0, 0, border.rotation);
					borderObject.transform.SetParent(parentTransform, true);
					borderObject.transform.localPosition = new Vector3(x * main.TileSize + main.TileSize / 2, y * main.TileSize + main.TileSize / 2, 0);
				}
			}
		}
	}

}
}