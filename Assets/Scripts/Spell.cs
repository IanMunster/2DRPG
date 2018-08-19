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
	private Transform target;

	// Use this for initialization
	void Start () {
		// Get the rigidbody Component
		rigidBody = GetComponent <Rigidbody2D> ();

		// DEBUG & TESTING PURPOSE ONLY
		target = GameObject.Find ("NPC_Target").transform;
	}

	
	// Update is called once per frame
	void Update () {
		
	}


	// Update every Physics update
	private void FixedUpdate () {
		// Get Direction from the Spell position towards Targets position
		Vector2 direction = target.position - transform.position;
		// Move the spell in direction of target by speed
		rigidBody.velocity = direction.normalized * speed;
		// Get angle in degrees towards Target
		float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
		// Rotate the Spell towards Target
		transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
	}
}
