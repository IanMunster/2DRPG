using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// User interface director.
/// 
/// </summary>

public class UIDirector : MonoBehaviour {

	// All Action Buttons
	[SerializeField] private Button[] actionButtons;

	// Keycodes for Quick Action based on Keybinds
	private KeyCode action1, action2, action3;

	// Use this for initialization
	void Start () {
		// Setup Default for Keybinds
		action1 = KeyCode.Alpha1;
		action2 = KeyCode.Alpha2;
		action3 = KeyCode.Alpha3;
	}
	
	// Update is called once per frame
	void Update () {
		// Check for Key Presses
		if (Input.GetKeyDown(action1)) {
			// "Click" the Correct Button
			ActionButtonOnClick (0);
		}
		if (Input.GetKeyDown(action2)) {
			// "Click" the Correct Button
			ActionButtonOnClick (1);
		}
		if (Input.GetKeyDown(action3)) {
			// "Click" the Correct Button
			ActionButtonOnClick (2);
		}
	}


	// Function to Click Action button Based on button Index
	private void ActionButtonOnClick (int buttonIndex) {
		// Invoke Function on Buttons OnClick Function
		actionButtons[buttonIndex].onClick.Invoke();
	}
}
