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
			if (hit != null) {
				// If raycast Hit Collider with tag Enemy (!ADDED: "HIT.COLLIDER != NULL" to prevent Errors)
				if (hit.collider != null && hit.collider.tag == "Enemy") {
					// Set Target of Player to Object Clicked
					player.MyTarget = hit.transform.GetChild(0);
				}
			// Otherwise De-Target from previous target
			} else {
				player.MyTarget = null;
			}
		}
	}
}