using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NPC. Inherits from Character
/// Function to Select and Deselect
/// Delegate: Updates UI TargetFrame Health Stat
/// </summary>

// Delegate Healthchange (amount health)
public delegate void HealthChanged (float health);

public class NPC : Character {

	// Health Changed Event 
	public event HealthChanged healthChanged;

	// Select NPC as Target
	public virtual Transform Select () {
		// Return Hitbox of Targeted NPC
		return hitBox;
	}
	// Deselect NPC from Target
	public virtual void Deselect () {
		// 

	}

	// Function for when Health of NPC changed
	public void OnHealthChanged (float health) {
		// Check for Listeners
		if (healthChanged != null) {
			// Changed Health (amount)
			healthChanged (health);
		}
	}
}
