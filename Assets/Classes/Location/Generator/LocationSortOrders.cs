public static class LocationSortOrders
{

	public static int Grass = 10;
	public static int Roads = 20;
	public static int GrassToRoadsBorders = 30;
	public static int MainObjectsBase = 10000;

	public static int GetLocationObjectSortOrder(float y)
	{
		return MainObjectsBase - (int)(y * 100);
	}

}