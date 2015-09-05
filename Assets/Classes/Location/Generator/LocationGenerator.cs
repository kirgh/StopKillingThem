using UnityEngine;
using System.Collections.Generic;

namespace LocationGeneration
{
	public class LocationGenerator : MonoBehaviour
	{

		public GameObject Rain;

		public void Generate ()
		{
			Main main = GetComponent<Main> ();
			GameObject location = new GameObject ("Location");
			GetComponent<GrassGenerator> ().CreateGrass (main).transform.SetParent(location.transform, true);
			GetComponent<RoadGenerator> ().CreateRoads (main).transform.SetParent(location.transform, true);
			GetComponent<HousesGenerator> ().CreateHouses (main).transform.SetParent(location.transform, true);
			GetComponent<DecorGenerator> ().CreateDecor (main).transform.SetParent(location.transform, true);
			GetComponent<CharacterGenerator> ().CreateCharacters (main).transform.SetParent(location.transform, true);

			GameObject rain = Instantiate (Rain);
			rain.GetComponent<Rain> ().Init (main);
			rain.transform.SetParent (location.transform, true);
		}
	}
}