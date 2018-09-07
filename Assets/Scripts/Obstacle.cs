using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Obstacle.
/// Has SpriteRenderer of Obstacle for SortingOrder
/// Is IComparable in List based on SortingOrder
/// Changes Obstacle's Alpha based on Player
/// </summary>

public class Obstacle : MonoBehaviour, IComparable <Obstacle> {

	// Sprite Renderer needed for sorting LayerOrder
	public SpriteRenderer MySpriteRenderer { get; set; }

	// Default color of Obstacle
	private Color defaultColor;
	// Color of Faded Obstacle
	private Color fadedColor;


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
		// Get SpriteRenderer of Obstacle
		MySpriteRenderer = GetComponent<SpriteRenderer> ();
		// Set Default Color of Obstacle
		defaultColor = MySpriteRenderer.color;
		// Set FadedColor based on DefaultColor
		fadedColor = defaultColor;
		// Change Alpha value = procent
		fadedColor.a = 0.7f;
	}
	
	// Update is called once per frame
	//void Update () {}


	// Function to Fade IN
	public void FadeIn () {
		// Change Color (alpha) to Default
		MySpriteRenderer.color = defaultColor;
	}


	// Function to Fade OUT
	public void FadeOut () {
		// Change Color (alpha) to Faded value
		MySpriteRenderer.color = fadedColor;
	}
}
