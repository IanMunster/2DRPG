using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NPC. Inherits from Character
/// </summary>

public class NPC : Character {


	// Select NPC as Target
	public virtual Transform Select () {
		// Return Hitbox of Targeted NPC
		return hitBox;
	}
	// Deselect NPC from Target
	public virtual void Deselect () {
		// 

	}
}
