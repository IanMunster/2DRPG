using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Obstacle.
/// </summary>

public class Obstacle : MonoBehaviour, IComparable <Obstacle> {

	// Sprite Renderer needed for sorting LayerOrder
	public SpriteRenderer MySpriteRenderer { get; set; }

	// Sort Interface (What to compare when calling Sort() Function)
	public int CompareTo (Obstacle other) {
		// If This is Larger than Other
		if (MySpriteRenderer.sortingOrder > other.MySpriteRenderer.sortingOrder) {
			return 1;
		} else if (MySpriteRenderer.sortingOrder < other.MySpriteRenderer.sortingOrder) {
			// Otherwise if This is Smaller than Other
			return -1;
		}
		// Otherwise both Equal (no sort needed)
		return 0;
	}

	// Use this for initialization
	void Start () {
		MySpriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
