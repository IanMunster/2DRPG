using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // needed for UI Components

/// <summary>
/// Stat.
/// Get UI Component for Stat
/// Initializes Health & Keeps CurrentValue
/// </summary>

public class Stat : MonoBehaviour {

	// Content of the Stat UI to display Amount in Fill Amount
	private Image content;
	// UI Stat Text Component to display current Amount in Text
	[SerializeField] private Text textValue;
	// The current amount of StatFill 
	private float currentFill;
	// The current value of the Stat
	private float currentValue;
	// Speed of UI Component value changes transition (Default Test 5.5f)
	//[SerializeField]
	private float lerpSpeed = 5.5f;

	// Maximal Value Property of Stat
	public float MyMaxValue { get; set; }

	///<summary> Current Value Property of Stat {Get currentValue, Set currentValue} </summary>
	public float MyCurrentValue {
		get { return currentValue; }
		set {
			// If received Value is Greater then Maximum of Value
			if (value > MyMaxValue) {
				// Revert Value to Maximum
				currentValue = MyMaxValue;
				// If received Value is Less than 0 (negative) 
			} else if (value < 0) {
				// Revert to 0
				currentValue = 0;
			} else {
				// Set current value
				currentValue = value;
			}
			// Set the Fill of UI; Current Fill / Max Value (between 0&1)
			currentFill = currentValue / MyMaxValue;
			// If stat has a UI Text Component
			if (textValue != null) {
				// Set the Text UI to Current Amount
				textValue.text = name + " :  " + currentValue + " / " + MyMaxValue;
			}
		}
	}


	// Use this for initialization
	void Start () {
		// Get the reference to the Image in UI for Content of Stat
		content = GetComponent<Image> ();
	}
		

	///<summary> Initialize the Stat Values (current, max) </summary>
	public void Initialize (float currentValue, float maxValue) {
		// Set the Max & Current Values of this Stat
		MyMaxValue = maxValue;
		MyCurrentValue = currentValue;
	}


	// Update is called once per frame
	void Update () {
		// If Current fill Value and UI fill are not the same
		if (currentFill != content.fillAmount) {
			// Make smooth Transition of Fill by Lerping Value
			content.fillAmount = Mathf.Lerp (content.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
		}
	}
}
