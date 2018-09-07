using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Layer sorter.
/// Sorts Layers of World Objects and Characters (in Front & Behind trees e.d.)
/// </summary>

public class LayerSorter : MonoBehaviour {

	// Sprite renderer to Sort
	private SpriteRenderer parentRenderer;

	// List of Obstacles to Check for
	List<Obstacle> obstacles = new List<Obstacle>();

	// Use this for initialization
	void Start () {
		parentRenderer = transform.parent.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	// Function to Check collision with Objects
	private void OnTriggerEnter2D (Collider2D collision) {
		// Check if Collides with "Obstacle" (gameObjects with Obstacle Tag) 
		if (collision.tag == "Obstacle") {
			// Get the Obstacle Script for the SortingOrder
			Obstacle o = collision.GetComponent<Obstacle> ();
			// Obstacle Fade Out
			o.FadeOut ();
			// Check if no Other Obstacles Hit & SortingOrder of Obstacle is Less than Character
			if (obstacles.Count == 0 || o.MySpriteRenderer.sortingOrder-1 < parentRenderer.sortingOrder) {
				// Change sorting order Parent (according to obstacles Sorting Order)
				parentRenderer.sortingOrder = o.MySpriteRenderer.sortingOrder - 1;
			}
			// Add the Obstacle to the List of Colliding Obstacles
			obstacles.Add (o);
		}
	}


	// Funtion after Collision
	private void OnTriggerExit2D (Collider2D collision) {
		// Check if Collides with "Obstacle" (gameObjects with Obstacle Tag) 
		if (collision.tag == "Obstacle") {
			// Get the Obstacle Script for the SortingOrder
			Obstacle o = collision.GetComponent<Obstacle> ();
			// Obstacle Fade IN
			o.FadeIn ();
			// Remove Obstacle from Colliding Obstacles
			obstacles.Remove(o);
			// If No More Colliding Obstacles left
			if (obstacles.Count == 0) {
				// Set to Above All
				parentRenderer.sortingOrder = 200;
			} else {
				// Otherwise, sort the Obstacle List (lowest Sorting Order)
				obstacles.Sort ();
				// Set Character behind Obstacle
				parentRenderer.sortingOrder = obstacles[0].MySpriteRenderer.sortingOrder - 1;
			}
		}
	}
}
