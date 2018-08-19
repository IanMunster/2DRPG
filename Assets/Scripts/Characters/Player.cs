using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player. Inherits behaviour from Character Class
/// Get Movement Input from User
/// </summary>

// Inherits from Character Class
public class Player : Character {

	//Health Stat of player
	[SerializeField]
	private Stat health;
	// Initial max ammount of Health
	private float initHealth = 100f;
	// Current ammount of Health
	[SerializeField]
	private float healthValue;

	// Mana Stat of player
	[SerializeField]
	private Stat mana;
	// Initial ammount of Mana (Max&Current)
	private float initMana = 50f;


	/// <summary> Overrides Update behaviour of Inherited class (Use this for initialization) </summary>
	protected override void Start () {
		// Initialize Players Health Stat 
		health.Initialize (healthValue, initHealth);
		// Initialize Players Mana Stat
		mana.Initialize (initMana, initMana);
		// Call the Inherited overriden StartFunction
		base.Start ();
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
//DEBUG
		// Get Input When "I" is pressed Decrease Health & When "O" is pressed Increase Health
		if (Input.GetKeyDown (KeyCode.I)) { health.MyCurrentValue -= 10f; mana.MyCurrentValue -= 10f; }
		if (Input.GetKeyDown (KeyCode.O)) { health.MyCurrentValue += 10f; mana.MyCurrentValue += 10f; }
//END DEBUG

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

		// Get Input when Space is Pressed
		if (Input.GetKeyDown (KeyCode.Space)) {
			// Call the Attack IEnumerator
			StartCoroutine ( Attack() );
		}
	}


	/// <summary> Function to Attack (IEnumerator to WaitForSecond Cast Time) </summary>
	private IEnumerator Attack () {
		// Set Character Is Attacking Bool to True to Activate the Correct Layer
		isAttacking = true;
		// Set the Animator to Attack Animation
		myAnimator.SetBool ("Attack", isAttacking);
		// Cast Cooldown Time; Wait for Amount of Seconds
		yield return new WaitForSeconds (3f); // Debugging Purpose
	}
}
