using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Character.
/// Moves Characters.
/// Animates Characters; Idle State & Attack
/// Handle Animation Layers
/// Take Damages
/// </summary>

// required components of Character
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
// Abstract Class (Can't be added to a Object on its own)
public abstract class Character : MonoBehaviour {

	//Movement Vars
	// Private Move Speed of Character (Test Default: 1.5f)
	[SerializeField] private float speed;
	// Rigidbody Component for Movement
	private Rigidbody2D rigidBody;
	// Protected Move Direction of Character (still accessable to Player e.d)
	protected Vector2 direction;

	//Animator Vars
	// Private myAnimator Controller Object (needed on the Character to Animate)
	protected Animator myAnimator;

	//Attack Vars
	// Bool to Check If the Character Is Attacking
	protected bool isAttacking = false;
	// Protected Attack Coroutine (needed to Stop Coroutine of Attacking when interupted)
	protected Coroutine attackRoutine;
	// Hitbox for Selecting (with mouse Click) of Character
	[SerializeField] protected Transform hitBox;

	//Health Vars
	// Initial max ammount of Health
	[SerializeField] private float initHealth;
	// Health stat of Character
	[SerializeField] protected Stat health;
	// Health Property of Character
	public Stat MyHealth { get {return health;} }

	/// <summary> Property to Check if charater is moving in any Direction </summary>
	public bool IsMoving { get { return direction.x != 0 || direction.y != 0; } }


	/// <summary> Protected Virtual Update can be called from inheriting Classes (Use this for initialization)</summary>
	protected virtual void Start () {
		// Initialize Characters Health Stat 
		health.Initialize (initHealth, initHealth);

		// Get the Components for the Character
		myAnimator = GetComponent<Animator> ();
		rigidBody = GetComponent<Rigidbody2D> ();
	}


	/// <summary> Protected Virtual Update can be called from inheriting Classes (Update is called once per frame) </summary>
	protected virtual void Update () {
		// Update the myAnimator Layers
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


	/// <summary> Function to Handle correct myAnimator Layers (Walk and Idle) </summary>
	public void HandleLayers () {
		// Check if the Character is moving
		if (IsMoving) {
			// Set Walk-AnimationLayer to front
			ActivateLayer ("WalkLayer");
			// Give Animators X & Y values for Movement & Idle Animations
			myAnimator.SetFloat ("X", direction.x);
			myAnimator.SetFloat ("Y", direction.y);
			// When Moving, Interupt Attack
			StopAttack ();

		// Check if the Character is Attacking
		} else if (isAttacking) {
			// Set the Attack-AnimationLayer to front
			ActivateLayer ("AttackLayer");
		} else {
			//Set Idle-AnimationLayer to front
			ActivateLayer ("IdleLayer");
		}
	}


	/// <summary> Function to Activate the correct myAnimator Layers (String: Layer to Activate) </summary>
	public void ActivateLayer (string layerName) {
		// Go through all possible Layers
		for (int i = 0; i < myAnimator.layerCount; i++) {
			// Set all Layers in myAnimator to Disabled (0)
			myAnimator.SetLayerWeight (i, 0);
		}
		//Enable Layer on Given Index by Name
		myAnimator.SetLayerWeight (myAnimator.GetLayerIndex(layerName), 1);
	}


	/// <summary> Function to Stop Attacking  virtual to override in inheriting class</summary>
	public virtual void StopAttack () {
		// Set character to not Attacking
		isAttacking = false;
		// Set the Animators Paramaters of Attack to false
		myAnimator.SetBool ("isAttacking", isAttacking);
		// Check if the player should be interupted / is Casting
		if (attackRoutine != null) {
			// Stop the Attack Coroutine for Casting
			StopCoroutine (attackRoutine);
		}
	}


	/// <summary> Function to Take Damage (float amount DMG) </summary>
	public virtual void TakeDamage (float damage) {
		// Reduce Health
		health.MyCurrentValue -= damage;
		// Check if the Character has No Health left
		if (health.MyCurrentValue <= 0) {
			// Character is Dead
			myAnimator.SetTrigger ("Die");
		}
	}
}