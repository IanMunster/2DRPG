using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// User interface director.
/// Stores KeyBinds, looks for Input of Keybinds
/// </summary>

public class UIDirector : MonoBehaviour {

	// Singleton Structure
	private static UIDirector instance;
	public static UIDirector MyInstance {
		get {
			//Check if there is No Instance
			if (instance == null) {
				// Find Object and set as Instance 
				instance = FindObjectOfType<UIDirector>();
			}
			return instance;
		}
	}
	// End of Singleton

	// All Action Buttons
	[SerializeField] private Button[] actionButtons;
	// Keycodes for Quick Action based on Keybinds
	private KeyCode action1, action2, action3;

	// UI frame of Target
	[SerializeField] private GameObject targetFrame;
	// Reference to Targeted Health Stat
	private Stat healthStat;


	// Use this for initialization
	void Start () {

		// Set targetframe's Health Stat
		healthStat = targetFrame.GetComponentInChildren<Stat>();

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


	// Function to Show UI Target Frame (target)
	public void ShowTargetFrame (NPC target) {
		// Show UI
		targetFrame.SetActive (true);
		// Set Health of TargetFrame to targeted Health
		healthStat.Initialize (target.MyHealth.MyCurrentValue, target.MyHealth.MyMaxValue);

		// Listen to HealthChanged Event
		target.healthChanged += new HealthChanged (UpdateTargetFrame);
	}

	// Function to Update UI TargetFrame Health Stat
	public void UpdateTargetFrame (float health) {
		// Update HealthStat
		healthStat.MyCurrentValue = health;
	}


	// Function to Hide UI TargetFrame
	public void HideTargetFrame () {
		// Hide UI
		targetFrame.SetActive (false);
	}
}
