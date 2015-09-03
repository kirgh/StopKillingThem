using System.Collections.Generic;
using UnityEngine;

public class LocationTiles
{

	private Dictionary<int, Dictionary<int, TileType>> tiles = new Dictionary<int, Dictionary<int, TileType>> ();
	public int Width{ private set; get; }
	public int Height{ private set; get; }


	public LocationTiles (int width, int height)
	{
		this.Width = width;
		this.Height = height;
		for (int i = 0; i < width; i++) {
			tiles[i] = new Dictionary<int, TileType>();
			for (int j = 0; j < height; j++) {
				tiles[i][j] = TileType.EMPTY;
			}
		}
	}

	public void Set(int x, int y, TileType tile)
	{
		if (!Contains (x, y)) {
			Debug.LogError("out of range");
			return;
		}
		tiles [x] [y] = tile;
	}

	public TileType Get(int x, int y){
		if (!Contains (x, y)) {
			Debug.LogError("out of range");
			return TileType.INVALID;
		}
		return tiles [x] [y];
	}

	public bool Contains(int x, int y)
	{
		return x >= 0 && y >= 0 && x < Width && y < Height;
	}

}