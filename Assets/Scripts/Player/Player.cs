using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player.
/// Moves Player
/// </summary>

public class Player : MonoBehaviour {

	// Private Move Speed of Player (Test Default: 1.5f)
	[SerializeField]
	private float speed;

	// Private Move Direction of Player
	private Vector2 direction;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Update the GetInput Function
		GetInput ();
		// Update the Move Function
		Move ();

	}


	// Function to Move the Player
	public void Move () {
		// Move (translate) the Transform position, in given Direction times PlayerSpeed times FrameUpdate
		transform.Translate (direction * speed * Time.deltaTime);
	}


	// Function to GetInput of User
	private void GetInput () {
		// No Input; No Movement, sets Direction to Ze	ro
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
