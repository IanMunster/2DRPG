//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

/// <summary>
/// Point. Structure
/// Position point with X & Y properties
/// </summary>

public struct Point {
	// X value of Point
	public int MyX {get; set;}
	// Y value of Point
	public int MyY {get; set;}

	// Point Constructer
	public Point (int x, int y) {
		// Set Point X and Y Value
		this.MyX = x;
		this.MyY = y;
	}
}