using System.Collections.Generic;

public class LocationTiles
{

	private Dictionary<int, Dictionary<int, TileType>> availableTiles = new Dictionary<int, Dictionary<int, TileType>> ();
	public int Width{ private set; get; }
	public int Height{ private set; get; }


	public LocationTiles (int width, int height)
	{
		this.Width = width;
		this.Height = height;
		for (int i = 0; i < width; i++) {
			availableTiles[i] = new Dictionary<int, TileType>();
			for (int j = 0; j < height; j++) {
				availableTiles[i][j] = TileType.EMPTY;
			}
		}
	}

}