using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Level director.
/// 
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


	// Use this for initialization
	void Start () {
		// Generate the Map
		GenerateMap ();
	}
	// Update is called once per frame
//	void Update () {}


	// Function to Generate the Map
	private void GenerateMap () {
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
						GameObject go = Instantiate (newElement.MyElementPrefab, map);
						// Set Tile to correct Position
						go.transform.position = new Vector2 (xPos, yPos);
					}
				}
			}
		}
	}
}