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


	///<summary> Protected Virtual Update can be called from inheriting Classes (Use this for initialization)</summary>
	protected virtual void Start () {
		// Get the Animator Component from the Character
		animator = GetComponent<Animator> ();
	}


	///<summary> Protected Virtual Update can be called from inheriting Classes (Update is called once per frame) </summary>
	protected virtual void Update () {
		// Update the Move Function
		Move ();
	}


	/// <summary> Function to Move the Character </summary>
	public void Move () {
		// Move (translate) the Transform position, in given Direction times PlayerSpeed times FrameUpdate
		transform.Translate (direction * speed * Time.deltaTime);
		// Check if the player is moving in any Direction
		if (direction.x != 0 || direction.y != 0) {
			// Call Animate Function to Animate Movement
			Animate (direction);
		} else {
			//Set Idle-AnimationLayer to front
			animator.SetLayerWeight (1, 0);
		}
	}


	/// <summary> Function to Animate Character Movement & Idle (in Vector2 Direction) And set the Animation Layer </summary>
	public void Animate (Vector2 direction) {
		//Set Walk-AnimationLayer to front
		animator.SetLayerWeight (1, 1);
		// Give Animators X & Y values for Movement & Idle Animations
		animator.SetFloat ("X", direction.x);
		animator.SetFloat ("Y", direction.y);
	}
}
