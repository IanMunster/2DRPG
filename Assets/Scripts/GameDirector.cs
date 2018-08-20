using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Game director.
/// Gives Target reference to Player (Clicked On)
/// </summary>

public class GameDirector : MonoBehaviour {

	// Reference to Player
	[SerializeField] private Player player;

	// Reference to Current Target NPC
	private NPC currentTarget;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Update the ClickTarget
		ClickTarget ();
	}


	// Function to Click on a Target with Mouse
	private void ClickTarget () {
		// If Left-Mouse Clicked and the Mouse is hovering over UI buttons
		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() ) {
			// Raycast from MousePosition into GameWorld on Clickable LayerMask
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, LayerMask.GetMask ("Clickable") );
			// If raycast Hit something
			if (hit.collider != null) {
				// Check if there is a Target
				if (currentTarget != null) {
					// Deselect the previous Target
					currentTarget.Deselect ();
				}
				// Set new Hit Target
				currentTarget = hit.collider.GetComponent<NPC> ();
				// Set new Target Selected to Player
				player.MyTarget = currentTarget.Select ();
			} else {
				// If no Target & no Clickable Target hit
				if (currentTarget != null) {
					// Deselect the Target / Previous target
					currentTarget.Deselect ();
				}
				// Empty Target ref
				currentTarget = null;
				player.MyTarget = null;
			}
		}
	}
}