using UnityEngine;
using System.Collections.Generic;

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
		new RoadGenerator(1,5, 1, 3).CreateRoads(main);

		//Houses
		//Trees, statues, etc
	}
}
