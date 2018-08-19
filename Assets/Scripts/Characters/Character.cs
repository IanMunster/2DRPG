using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Character.
/// Moves Characters.
/// Animates Characters; Idle State
/// </summary>

// Abstract Class (Can't be added to a Object on its own)
public abstract class Character : MonoBehaviour {

	// Private Move Speed of Character (Test Default: 1.5f)
	[SerializeField]
	private float speed;
	// Private Animator Controller Object (needed on the Character to Animate)
	private Animator animator;
	// Protected Move Direction of Character (still accessable to Player e.d)
	protected Vector2 direction;
	// Rigidbody Component for Movement
	private Rigidbody2D rigidBody;

	///<summary> Property to Check if charater is moving in any Direction </summary>
	public bool IsMoving { get{ return direction.x != 0 || direction.y != 0; }}

	///<summary> Protected Virtual Update can be called from inheriting Classes (Use this for initialization)</summary>
	protected virtual void Start () {
		// Get the Components for the Character
		animator = GetComponent<Animator> ();
		rigidBody = GetComponent<Rigidbody2D> ();
	}


	///<summary> Protected Virtual Update can be called from inheriting Classes (Update is called once per frame) </summary>
	protected virtual void Update () {
		// Update the Animator Layers
		HandleLayers ();
	}


	// Update every Physics update
	private void FixedUpdate () {
		// Update the Move Function
		Move ();
	}


	/// <summary> Function to Move the Character </summary>
	public void Move () {
		// Move the Character, in given Direction (normalized) times PlayerSpeed
		rigidBody.velocity = direction.normalized * speed;
	}


	/// <summary> Function to Handle correct Animator Layers (Walk and Idle) </summary>
	public void HandleLayers () {
		// Check if the player is moving
		if (IsMoving) {
			// Set Walk-AnimationLayer to front
			ActivateLayer ("WalkLayer");
			// Give Animators X & Y values for Movement & Idle Animations
			animator.SetFloat ("X", direction.x);
			animator.SetFloat ("Y", direction.y);
		} else {
			//Set Idle-AnimationLayer to front
			ActivateLayer ("IdleLayer");
		}
	}


	/// <summary> Function to Activate the correct Animator Layers (String: Layer to Activate) </summary>
	public void ActivateLayer (string layerName) {
		// Go through all possible Layers
		for (int i = 0; i < animator.layerCount; i++) {
			// Set all Layers in Animator to Disabled (0)
			animator.SetLayerWeight (i, 0);
		}
		//Enable Layer on Given Index by Name
		animator.SetLayerWeight (animator.GetLayerIndex(layerName), 1);
	}
}
