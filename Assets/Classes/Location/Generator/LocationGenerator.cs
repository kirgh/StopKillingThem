using UnityEngine;
using System.Collections.Generic;

namespace LocationGeneration
{
	public class LocationGenerator : MonoBehaviour
	{
		public void Generate ()
		{
			Main main = GetComponent<Main> ();
			GetComponent<GrassGenerator> ().CreateGrass (main);
			GetComponent<RoadGenerator> ().CreateRoads (main);
			GetComponent<HousesGenerator> ().CreateHouses (main);
			GetComponent<DecorGenerator> ().CreateDecor (main);
		}
	}
}