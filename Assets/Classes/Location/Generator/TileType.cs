using System;
public class TileType
{

	public static TileType Invalid = new TileType ();
	public static TileType Empty = new TileType ();
	public static TileType Road = new TileType ();
	public static TileType House = new TileType ();
	public static TileType Forbidden = new TileType ();

	private TileType ()
	{
	}
}