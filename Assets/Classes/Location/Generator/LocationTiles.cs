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
				tiles[i][j] = TileType.Empty;
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

	public void SetRect(int minX, int minY, int width, int height, TileType type, bool outsizeIsValid)
	{
		for(int x = minX; x < minX + width  ; x++){
			for(int y = minY; y < minY + height  ; y++){
				if(outsizeIsValid && !Contains(x, y)){
					continue;
				}
				Set (x, y, type);
			}
		}
	}

	public TileType Get(int x, int y){
		if (!Contains (x, y)) {
			Debug.LogError("out of range");
			return TileType.Invalid;
		}
		return tiles [x] [y];
	}

	public bool Contains(int x, int y)
	{
		return x >= 0 && y >= 0 && x < Width && y < Height;
	}

	public bool IsEmptyRect(int x, int y, int width, int height, bool outsideIsValid){
		for (int i = x; i < x + width; i++) {
			for (int j = y; j < y + height; j++) {
				if(!Contains(i,j)){
					if(outsideIsValid){
						continue;
					}
					return false;
				}
				if(Get (i,j) != TileType.Empty){
					return false;
				}
			}
		}
		return true;
	}

}