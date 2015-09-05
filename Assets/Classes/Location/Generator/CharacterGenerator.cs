using UnityEngine;
using System.Collections;

namespace LocationGeneration
{
	public class CharacterGenerator : MonoBehaviour
	{

		public GameObject Player;

		public GameObject[] NPCs;
		public int minimum = 2;
		public int maximum = 10;

		public GameObject CreateCharacters(Main main)
		{
			GameObject parent = new GameObject ("Npcs");
			int count = UnityEngine.Random.Range(minimum, maximum + 1);

			for (int i = 0; i < count + 1; i++) {
				Point<int> point = main.Tiles.GetRandomTile(TileIsAvailable);
				if (point == null) {
					Debug.Log("no more place for character");
					break;
				}

				bool isPlayer = i == 0;
				GameObject toInstaniate = isPlayer ? Player : NPCs[UnityEngine.Random.Range(0, NPCs.Length)];
				Vector3 position =new Vector3(point.x * main.TileSize + main.TileSize * UnityEngine.Random.value, point.y * main.TileSize + main.TileSize * UnityEngine.Random.value, 0);
				GameObject instance = Instantiate(toInstaniate, position, Quaternion.identity) as GameObject;
				instance.GetComponentInChildren<SpriteRenderer>().sortingOrder = LocationSortOrders.GetLocationObjectSortOrder(instance.transform.localPosition.y);
				instance.transform.SetParent(parent.transform, true);
			}

			return parent;
		}

		private static bool TileIsAvailable(TileType type)
		{
			return type == TileType.Empty || type == TileType.Road;
		}


	}
}