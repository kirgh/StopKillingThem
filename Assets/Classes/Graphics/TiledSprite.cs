﻿using UnityEngine;
using System.Collections.Generic;

public class TiledSprite : MonoBehaviour {

	private int width;
	private int height;
	private float tileWidth;
	private float tileHeight;
	private int sortOrder;
	private bool dirty;
	private TiledSpriteSettings tileSet;

	public static GameObject Create(TiledSpriteSettings settings)
	{
		GameObject result = new GameObject ();
		result.AddComponent<TiledSprite> ().tileSet = settings;
		return result;
	}

	public TiledSprite SetTileSize(float tileWidth, float tileHeight)
	{
		if (this.tileWidth == tileWidth && this.tileHeight == tileHeight) {
			return this;
		}
		this.tileWidth = tileWidth;
		this.tileHeight = tileHeight;
		dirty = true;
		return this;
	}

	public TiledSprite SetTiles(int width, int height)
	{
		if (this.width == width && this.height == height) {
			return this;
		}
		this.width = width;
		this.height = height;
		dirty = true;
		return this;
	}

	public TiledSprite SetSortOrder(int order)
	{
		sortOrder = order;
		dirty = true;
		return this;
	}
	
	void Update()
	{
		if (!dirty) {
			return;
		}
		ForceUpdate ();
		dirty = false;
	}

	public void ForceUpdate ()
	{
		Transform thisTransform = transform;
		for (var i = thisTransform.childCount - 1; i >= 0; i--){
			Destroy(thisTransform.GetChild(i).gameObject);
		}

		float spriteWidth = tileSet.Sprites[0].bounds.size.x;
		float spriteHeight = tileSet.Sprites[0].bounds.size.y;
		float tileWidth = this.tileWidth == 0 ? spriteWidth : this.tileWidth;
		float tileHeight = this.tileHeight == 0 ? spriteHeight : this.tileHeight;
		Vector3 spriteScale = new Vector3 (tileWidth / spriteWidth, tileHeight / spriteHeight, 1);

		for (int x = 0; x < width; x++) {
			for(int y = 0; y < height; y++){
				GameObject child = new GameObject();
				child.AddComponent<SpriteRenderer>().sprite = tileSet.Sprites[UnityEngine.Random.Range(0, tileSet.Sprites.Length)];
				child.transform.SetParent(thisTransform, false);
				child.transform.localScale = spriteScale;
				child.GetComponent<SpriteRenderer>().sortingOrder = sortOrder;
				if(tileSet.AllowRandomRotation){
					child.transform.Rotate(0, 0, 90 * UnityEngine.Random.Range(0, 4));
				}
				child.transform.localPosition = new Vector3(tileWidth / 2 + x * tileWidth, tileHeight / 2 + y * tileHeight, 0);
			}
		}
	}

}
