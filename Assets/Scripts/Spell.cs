using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spell.
/// 
/// </summary>

public class Spell : MonoBehaviour {

	// Rigidbody Component needed for Movement 
	private Rigidbody2D rigidBody;
	// Speed value of Spell Movement (serialized for testing)
	[SerializeField] private float speed;

	// Target of the Spell
	public Transform MyTarget {
		get;
		set;
	}


	// Use this for initialization
	void Start () {
		// Get the rigidbody Component
		rigidBody = GetComponent <Rigidbody2D> ();
	}

	
	// Update is called once per frame
	void Update () {
		
	}


	// Update every Physics update
	private void FixedUpdate () {
		// If there is A Target
		if (MyTarget != null) {
			// Get Direction from the Spell position towards Targets position
			Vector2 direction = MyTarget.position - transform.position;
			// Move the spell in direction of target by speed
			rigidBody.velocity = direction.normalized * speed;
			// Get angle in degrees towards Target
			float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
			// Rotate the Spell towards Target
			transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
		}
	}


	// Check for Impact by Colliders
	private void OnTriggerEnter2D (Collider2D collision) {
		// If Spell Collides with an Enemy Hitbox and Position is Correct
		if (collision.tag == "HitBox" && collision.transform == MyTarget) {
			// Set the Animator to Impact (to show Smoke)
			GetComponent<Animator>().SetTrigger("Impact");
			// Reset the Velocity (movement) of Spell
			rigidBody.velocity = Vector2.zero;
			// Reset the Target of Spell
			MyTarget = null;
		}
	}
}
