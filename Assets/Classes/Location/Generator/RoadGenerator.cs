using System;
using UnityEngine;

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
			CreateRoad (startX, startY, roadWidth, vertical, main);
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

	private void CreateGrassToRoadBorders(Main main)
	{
		
	}

}
