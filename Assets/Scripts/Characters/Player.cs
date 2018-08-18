using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player. Inherits behaviour from Character Class
/// Get Movement Input from User
/// </summary>

// Inherits from Character Class
public class Player : Character {



	// Use this for initialization
	void Start () {
		
	}


	/// <summary> Overrides Update behaviour of Inherited class (Update is called once per frame) </summary>
	protected override void Update () {
		// Update the GetInput Function
		GetInput ();
		// Call the Inherited overridden Update Function
		base.Update ();
	}


	/// <summary> Function to GetInput from User (WASD)</summary>
	private void GetInput () {
		// No Input; No Movement, sets Direction to Zero
		direction = Vector2.zero;

		// Get Input When "W" is hold.
		if (Input.GetKey (KeyCode.W)) {
			// Change Direction to Upwards Direction 
			direction += Vector2.up;
		}
		// Get Input When "A" is hold.
		if (Input.GetKey (KeyCode.A)) {
			// Change Direction to Left Direction 
			direction += Vector2.left;
		}
		// Get Input When "S" is hold.
		if (Input.GetKey (KeyCode.S)) {
			// Change Direction to Downwards Direction 
			direction += Vector2.down;
		}
		// Get Input When "D" is hold.
		if (Input.GetKey (KeyCode.D)) {
			// Change Direction to Right Direction 
			direction += Vector2.right;
		}
	}
}
