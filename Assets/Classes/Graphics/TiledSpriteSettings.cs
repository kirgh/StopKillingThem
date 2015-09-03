using System.Collections.Generic;

public class TiledSpriteSettings
{

	public List<string> TileNames{ private set; get; }
	public bool AllowRandomRotation{ private set; get; }

	private TiledSpriteSettings ()
	{
	}

	public static TiledSpriteSettings CreateWithTiles(List<string> tileNames, bool allowRandomRotation)
	{
		TiledSpriteSettings result = new TiledSpriteSettings ();
		result.TileNames = new List<string> (tileNames);
		result.AllowRandomRotation = allowRandomRotation;
		return result;
	}

	public static TiledSpriteSettings CreateWithTile(string tileName, bool allowRandomRotation)
	{
		TiledSpriteSettings result = new TiledSpriteSettings ();
		result.TileNames = new List<string>(){tileName};
		result.AllowRandomRotation = allowRandomRotation;
		return result;
	}

}