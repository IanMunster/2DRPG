using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spell book.
/// Contains all the Spells the player knows
/// </summary>

public class SpellBook : MonoBehaviour {

	// All known Spells to player
	[SerializeField] private Spell[] spells;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Function to Return a Spell (from Array on Index)
	public Spell CastSpell (int index) {
		// Return the spell on the Index
		return spells [index];
	}
}
