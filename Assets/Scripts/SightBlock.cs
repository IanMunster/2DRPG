using System;
using UnityEngine;

/// <summary>
/// Sight block. (Serializable)
/// Block the Field of View based on the SightBlocker Objects (BoxCollider 2D)
/// </summary>

[Serializable]
public class SightBlock {

	// Gameobject Array of all Sight Blockers
	[SerializeField] private GameObject firstBlock, secondBlock;


	// Function to Deactivate the SightBlocks
	public void Deactivate () {
		firstBlock.SetActive (false);
		secondBlock.SetActive (false);
	}

	// Function to Activate the SightBlocks
	public void Activate () {
		firstBlock.SetActive (true);
		secondBlock.SetActive (true);
	}
}