using System;
using System.Collections.Generic;
using UnityEngine;

namespace LocationGeneration
{
	public class DecorGenerator : MonoBehaviour
	{

		public Sprite[] Sprites;
		public int minCount;
		public int maxCount;

		public void CreateDecor(Main main)
		{
			GameObject parent = new GameObject ("Decor");
			int count = UnityEngine.Random.Range(minCount, maxCount);
			LocationTiles tempTiles = new LocationTiles (main.Tiles);

			for (int i = 0; i < count; i++) {
				Point<int> point = tempTiles.GetFreeRandomPoint (1, 1, 0, 0, 0);
				if (point == null) {
					Debug.Log("no more place for decor");
					break;
				}

				Sprite sprite = Sprites[UnityEngine.Random.Range(0, Sprites.Length)];
				GameObject decor = new GameObject("Decor " + sprite.name);

				float scale = main.SourceGraphicsScale;
				decor.AddComponent<SpriteRenderer>().sprite = sprite;
				decor.transform.SetParent(parent.transform, true);
				decor.transform.localScale = new Vector3(scale* (UnityEngine.Random.value > 0.5f ? 1 : -1), scale, 1);
				decor.transform.localPosition = new Vector3(point.x * main.TileSize + main.TileSize / 2, point.y * main.TileSize + main.TileSize / 2 + scale * sprite.bounds.size.y / 2, 0);
				decor.GetComponent<SpriteRenderer>().sortingOrder = LocationSortOrders.GetLocationObjectSortOrder(point.y * main.TileSize + main.TileSize / 2);
				tempTiles.Set(point.x, point.y, TileType.Forbidden);
			}
		}

	}
}

