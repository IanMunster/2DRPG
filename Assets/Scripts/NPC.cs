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
// Delegate CharaterRemoved, when Character is removed from game
public delegate void CharacterRemoved ();

public class NPC : Character {

	// Health Changed Event 
	public event HealthChanged healthChanged;
	// Character Removed Event
	public event CharacterRemoved characterRemoved;

	// Portait of NPC for TargetFrame
	[SerializeField] private Sprite portrait;
	// Portrait Property
	public Sprite MyPortrait { get { return portrait; }}


	// Select NPC as Target
	public virtual Transform Select () {
		// Return Hitbox of Targeted NPC
		return hitBox;
	}
	// Deselect NPC from Target
	public virtual void Deselect () {
		// Remove HealthChanged EventListener
		healthChanged -= new HealthChanged (UIDirector.MyInstance.UpdateTargetFrame);
		// Remove CharacterRemoved EventListener
		characterRemoved -= new CharacterRemoved (UIDirector.MyInstance.HideTargetFrame);
	}

	// Function for when Health of NPC changed
	public void OnHealthChanged (float health) {
		// Check for EventListeners
		if (healthChanged != null) {
			// Changed Health (amount)
			healthChanged (health);
		}
	}


	// Function for when a Character is Removed From Game
	public void OnCharacterRemoved () {
		// Check for EventListeners
		if (characterRemoved != null) {
			// Character is Removed
			characterRemoved ();
		}
		// Destroy Character Gameobject
		Destroy (gameObject);
	}
}
