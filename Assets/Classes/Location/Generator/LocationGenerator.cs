using UnityEngine;
using System.Collections.Generic;

namespace LocationGeneration{
public class LocationGenerator : MonoBehaviour {

	void Start () {
		CreateLocation ();
	}
	
	void Update () {
	
	}

	private void CreateLocation()
	{
		Main main = GetComponent<Main> ();

		new GrassGenerator ().CreateGrass (main);

		new RoadGenerator(1, 3, 1, 3).CreateRoads(main);

		new HousesGenerator {
			minHousesCount = 1,
			maxHousesCount = 5,
			minHouseWidth = 2,
			maxHouseWidth = 5,
			minHouseHeight = 1,
			maxHouseHeight = 3
		}.CreateHouses (main);

		//Trees, statues, etc
	}
}
}