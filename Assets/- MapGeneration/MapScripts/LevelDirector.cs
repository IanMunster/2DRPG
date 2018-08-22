using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Level director.
/// Generate a Map based on PixelData. Creates Tiles from Sprite, gives Position, Sort Tree Renderer LayerOrder & Creates Edged Water
/// </summary>

public class LevelDirector : MonoBehaviour {

	// Parent of all MapTiles
	[SerializeField] private Transform map;
	// Map Data Layers
	[SerializeField] private Texture2D[] mapData;
	// Map Elements (tile)
	[SerializeField]private MapElement[] mapElements;
	// Default Sprite Size for Tile
	[SerializeField] private Sprite defaultTile;

	// World StartPos based on Camera (Start of Map Generation Pos)
	private Vector3 WorldStartPos { get { return Camera.main.ScreenToWorldPoint (new Vector3(0,0)); } }
	// Dictionary for checking Neighboring WaterTiles <Position, Tile>
	private Dictionary<Point, GameObject> waterTiles = new Dictionary<Point, GameObject>();


	// Use this for initialization
	void Start () {
		// Generate the Map
		GenerateMap ();
	}

	/// <summary> Function to Generate the Map; Create Tile on Pos, Sort Tree LayerOrder & Create WaterTile Dictionary</summary>
	private void GenerateMap () {
		// Set height of Map
		int height = mapData[0].height;
		int width = mapData [0].width;

		// Go through all Map Data
		for (int i = 0; i < mapData.Length; i++) {
			//  Go through all pixels on X width
			for (int x = 0; x < mapData[i].width; x++) {
				// Go through all pixels on Y height
				for (int y = 0; y < mapData[i].height; y++) {
					// Color of current Pixel
					Color c = mapData[i].GetPixel (x, y);
					// New MapElement to place, check for same color of Pixel
					MapElement newElement = Array.Find(mapElements, e => e.MyColor == c);
					// If Tile found with color
					if (newElement != null) {
						// Set Tiles X Position (startpos + bounds of OtherTile)
						float xPos = WorldStartPos.x + (defaultTile.bounds.size.x * x);
						// Set Tiles Y Position (startpos + bounds of OtherTile)
						float yPos = WorldStartPos.y + (defaultTile.bounds.size.y * y);
						// Create Tile GameObject as Child of Map
						GameObject newTile = Instantiate (newElement.MyElementPrefab, map.GetChild(i));
						// Set Tile to correct Position
						newTile.transform.position = new Vector2 (xPos, yPos);
						// Check for Tag Water on Tile
						if (newElement.MyTileTag == "Water") {
							// Add WaterTile Position and GameObject to WaterTiles Dictionary
							waterTiles.Add (new Point (x, y), newTile);
						}
						//Check for Tag Tree or Bush on Tile
						if (newElement.MyTileTag == "Tree" || newElement.MyTileTag == "Bush") {
							// Get the SpriteRenderer and Set Sorting Order, based on Y
							newTile.GetComponent<SpriteRenderer>().sortingOrder = height * 2 - y * 2;
						}
					}
				}
			}
		}
		// Check all WaterTiles after Creation of Map
		CheckWater ();
	}

	public void CheckWater () {
		// Check for Tiles of <position and gameobject>
		foreach (KeyValuePair <Point,GameObject> tile in waterTiles) {
			// Compose the Neighbor-String on Tile Position
			string composition = TileCheck (tile.Key);
		}
	}


	/// <summary> Function to Check Through the WaterTiles Dictionary & .. (Current WaterTile Position) </summary>
	public string TileCheck (Point currentPoint) {
		// Composition of Water ("W" water, "E" earth)
		string composition = string.Empty;
		// Go through all X values of Tile
		for (int x = -1; x <= 1; x++) {
			// Go through all Y values of Tile
			for (int y = -1; y <= 1; y++) {
				// Dont check Own position.
				if (x != 0 || y != 0) {
					// check for waterTiles in Dictionary
					if (waterTiles.ContainsKey (new Point (currentPoint.MyX + x, currentPoint.MyY + y))) {
						// Add W for Water to Composition
						composition += "W";
					} else {
						// Otherwise add E for Earth to Composition
						composition += "E";
					}
				}
			}
		}
		return composition;
	}
}