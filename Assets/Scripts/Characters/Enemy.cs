using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy. Inherits from NPC
/// Show Health UI on Select
/// </summary>

public class Enemy : NPC {

	// UI Canvas of Healthbar
	[SerializeField] private CanvasGroup healthGroup;


	// Override of NPC Select Function
	public override Transform Select () {
		// Make healthBar visible when Enemy is Selected
		healthGroup.alpha = 1;
		// Call base of Select from NPC
		return base.Select ();
	}


	// overide of Npc Deselect function
	public override void Deselect () {
		// Remove Healthbar for vision
		healthGroup.alpha = 0;
		// Call base of Deselect
		base.Deselect ();
	}


	// Override of NPC take Damage Function
	public override void TakeDamage (float damage) {
		// Call Base TakeDamage (reduces health)
		base.TakeDamage (damage);

		// Trigger OnHealthChanged Event (sent current health value (after subtrackting))
		OnHealthChanged (health.MyCurrentValue);
	}
}
