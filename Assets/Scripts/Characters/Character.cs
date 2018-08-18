using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Character.
/// Moves Characters
/// </summary>

// Abstract Class (Can't be added to a Object on its own)
public abstract class Character : MonoBehaviour {

	// Private Move Speed of Character (Test Default: 1.5f)
	[SerializeField]
	private float speed;

	// Protected Move Direction of Character (still accessable to Player e.d)
	protected Vector2 direction;


	// Use this for initialization
	void Start () {
		
	}


	///<summary> Protected Virtual Update can be called from inhereting Classes (Update is called once per frame) </summary>
	protected virtual void Update () {
		// Update the Move Function
		Move ();
	}


	/// <summary> Function to Move the Character </summary>
	public void Move () {
		// Move (translate) the Transform position, in given Direction times PlayerSpeed times FrameUpdate
		transform.Translate (direction * speed * Time.deltaTime);
	}
}
