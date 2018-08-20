using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Map element. aka Tile
/// TileTag, color and prefab
/// </summary>

[Serializable] public class MapElement {
	// Tag of the Tile
	[SerializeField] private string tileTag;
	// Color used for Placing Tile
	[SerializeField] private Color color;
	// Tile Prefab
	[SerializeField] private GameObject elementPrefab;

	// Properties of MapElement
	public string MyTileTag {get { return tileTag; }}
	public Color MyColor {get { return color; }}
	public GameObject MyElementPrefab {get { return elementPrefab; }}
}