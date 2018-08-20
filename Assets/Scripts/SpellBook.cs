using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Spell book.
/// Contains all the Spells the player knows
/// </summary>

public class SpellBook : MonoBehaviour {

	// Speed of Fade In/Out of Castingbar
	[SerializeField] private float fadeSpeed;
	// Casting bar on UI to Change Castbar Color
	[SerializeField] private Image castingBar;
	// UI to show Name of the Spell being Cast
	[SerializeField] private Text spellName;
	// UI to show Icon of Spell being Cast
	[SerializeField] private Image icon;
	// Ui to show Spell Cast time
	[SerializeField] private Text castTime;
	// CanvasGroup Component on CastBar to Fade Castbar
	[SerializeField] private CanvasGroup canvasGroup;
	// Fade Routine of CastBar
	private Coroutine fadeRoutine;

	// Progress Routine of Spell
	private Coroutine spellRoutine;
	// All known Spells to player
	[SerializeField] private Spell[] spells;


	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}

	// Function to Return a Spell (from Array on Index)
	public Spell CastSpell (int index) {
		// reset Castingbar Progress Fill
		castingBar.fillAmount = 0;
		// Set the Correct Spell Color to Casting Bar
		castingBar.color = spells[index].MyCastbarColor;
		// Set correct Spell Name
		spellName.text = spells[index].MyName;
		// Set correct Icon of Spell
		icon.sprite = spells[index].MyIcon;
		// Start the Progress Routine to show Progress on UI
		spellRoutine = StartCoroutine (Progress (index));
		// Start the Fade Routine to show CastBar
		fadeRoutine = StartCoroutine ( FadeCastBar ()); 
		// Return the spell on the Index
		return spells[index];
	}


	// Function to Fade progressBar in and Out
	private IEnumerator FadeCastBar () {
		// Rate of Fade (based on Cast Time of Spell)
		float rate = (1.0f / fadeSpeed);
		// Current Fade value
		float value = 0.0f;
		// While progress of Fade is below 1 (100 procent)
		while (value <= 1.0f) {
			// Fade Alpha In and Out with speed of Rate
			canvasGroup.alpha = Mathf.Lerp (0, 1, rate);
			// Increase Fade value with rate and time past
			value += rate * Time.deltaTime;
			// return nothing
			yield return null;
		}
	}


	// Function to show Progress of Spell Casting
	private IEnumerator Progress (int index) {
		// Start time of Progress
		float timePassed = Time.deltaTime;
		// Rate of Progress (based on Cast Time of Spell)
		float rate = (1.0f / spells[index].MyCastTime);
		// Current Progress
		float progress = 0.0f;
		// While progress is below 1 (100 procent)
		while (progress <= 1.0f) {
			// Lerp the Fill Amount of Casting bar from Current to New progress
			castingBar.fillAmount = Mathf.Lerp (0, 1, progress);
			// Increase Progress with rate and time past
			progress += rate * Time.deltaTime;
			// Increase the time that has passed
			timePassed += Time.deltaTime;
			// Update UI of Cast Time (Time needed - time Passed) as a String (2after colon)
			castTime.text = (spells[index].MyCastTime - timePassed).ToString("F2");
			// Dont display negative number
			if (spells[index].MyCastTime - timePassed < 0) {
				castTime.text = "0.00";
			}
			// return nothing
			yield return null;
		}
		// After done with Progress stop Casting
		StopCasting ();
	}


	// Function to Stop Casting
	public void StopCasting () {
		// If Fade Routine is Running
		if (fadeRoutine != null) {
			// Stop the Fade In Routine
			StopCoroutine (fadeRoutine);
			// Fade CastBar
			canvasGroup.alpha = 0;
			// Reset the FadeIn Routine
			fadeRoutine = null;
		}

		// There currently is a SpellCast-Progress running
		if (spellRoutine != null) {
			// Stop the progress routine
			StopCoroutine (spellRoutine);
			// Reset progress routine
			spellRoutine = null;
		}
	}
}