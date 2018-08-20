using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player. Inherits behaviour from Character Class
/// Get Input from User for Movement & Attack
/// Initializes players Health & Mana
/// Cast Spells & Position to cast
/// Check's for Line of Sight (Sight-Block)
/// </summary>

// Inherits from Character Class
public class Player : Character {

	//Health Stat of player
	[SerializeField] private Stat health;
	// Initial max ammount of Health
	private float initHealth = 100f;
	// Current ammount of Health
	[SerializeField] private float healthValue;

	// Mana Stat of player
	[SerializeField] private Stat mana;
	// Initial ammount of Mana (Max&Current)
	private float initMana = 50f;

	// Players SpellBook
	private SpellBook spellBook;

	// Array of All the GemPoints (Exit points of spell)
	[SerializeField] private Transform[] gemPoints;
	// Index of the current Gem ExitPoint (2 as Default downwards)
	private int gemIndex = 2;

	// All Sightblock of Player to Check Line Of Sight
	[SerializeField] private SightBlock[] sightBlocks;
	// Property to Set the Target of an Attack
	public Transform MyTarget { get; set; }


	/// <summary> Overrides Update behaviour of Inherited class (Use this for initialization) </summary>
	protected override void Start () {
		// Find the Players SpellBook
		spellBook = GetComponent <SpellBook> ();
		// Initialize Players Health Stat 
		health.Initialize (healthValue, initHealth);
		// Initialize Players Mana Stat
		mana.Initialize (initMana, initMana);
		// Call the Inherited overriden StartFunction
		base.Start ();
	}


	/// <summary> Overrides Update behaviour of Inherited class (Update is called once per frame) </summary>
	protected override void Update () {
		// Update the GetInput Function
		GetInput ();
		// Call the Inherited overridden Update Function
		base.Update ();
	}


	/// <summary> Function to GetInput from User (WASD)</summary>
	private void GetInput () {

//DEBUG
		// Get Input When "I" is pressed Decrease Health & When "O" is pressed Increase Health
		if (Input.GetKeyDown (KeyCode.I)) { health.MyCurrentValue -= 10f; mana.MyCurrentValue -= 10f; }
		if (Input.GetKeyDown (KeyCode.O)) { health.MyCurrentValue += 10f; mana.MyCurrentValue += 10f; }
//END DEBUG

		// No Input; No Movement, sets Direction to Zero
		direction = Vector2.zero;
		// Get Input When "W" is hold.
		if (Input.GetKey (KeyCode.W)) {
			// Change Direction to Upwards Direction 
			direction += Vector2.up;
			// Gem Index to Upwards
			gemIndex = 0;
		}
		// Get Input When "A" is hold.
		if (Input.GetKey (KeyCode.A)) {
			// Change Direction to Left Direction 
			direction += Vector2.left;
			// Gem Index to Left
			gemIndex = 1;
		}
		// Get Input When "S" is hold.
		if (Input.GetKey (KeyCode.S)) {
			// Change Direction to Downwards Direction 
			direction += Vector2.down;
			// Gem Index to Downwards
			gemIndex = 2;
		}
		// Get Input When "D" is hold.
		if (Input.GetKey (KeyCode.D)) {
			// Change Direction to Right Direction 
			direction += Vector2.right;
			// Gem Index to Right
			gemIndex = 3;
		}
	}


	/// <summary> Function to Attack (IEnumerator to WaitForSecond Cast Time) </summary>
	private IEnumerator Attack (int spellIndex) {
		// Current Target is Last Clicked Target
		Transform currentTarget = MyTarget; 
		// Get a new Spell from Spellbook (on Index of Array)
		Spell newSpell = spellBook.CastSpell(spellIndex);
		// Set Character Is Attacking to True to Activate the Attack Layer
		isAttacking = true;
		// Set the Animator to Attack Animation
		myAnimator.SetBool ("isAttacking", isAttacking);
		// Cast Time; Wait for Amount of Seconds
		yield return new WaitForSeconds (newSpell.MyCastTime);
		// Check if Target is Still there & InLine of Sight
		if (currentTarget != null && InLineOfSight() ) {
			// Instantiate a Spell (Get the First Spell, from possible Spell Prefabs, on the Player Pos, with Own Rot)
			SpellScript s = Instantiate(newSpell.MySpellPrefab, gemPoints[gemIndex].position, Quaternion.identity).GetComponent<SpellScript>();
			// Set the new Spells Target
			s.MyTarget = currentTarget;
		}
		// After cast, Stop Attacking
		StopAttack ();
	}


	/// <summary> Function to Cast a Spell (From SpellLibrary) </summary>
	public void CastSpell (int spellIndex) {
		// When Attacking Block the View Behind Player
		BlockView ();
		// Check if the Player has a target, is Not Attacking or Moving & In Line of Sight
		if (MyTarget != null && !isAttacking && !IsMoving && InLineOfSight() ) {
			// Call the Attack IEnumerator
			attackRoutine = StartCoroutine ( Attack(spellIndex) );
		}
	}


	/// <summary> Checks with Raycast if InLineOfSight or Blocked</summary>
	private bool InLineOfSight () {
		// Calculate the Direction of Target
		Vector3 targetDirection = (MyTarget.position - transform.position).normalized;
		// Cast a Ray (from player towards enemy, with distance of same range), on LayerMask 8 (viewBlock Layer)
		RaycastHit2D hit = Physics2D.Raycast (transform.position, targetDirection, Vector2.Distance( transform.position, MyTarget.position ), LayerMask.GetMask("ViewBlock"));
		// If Raycast doesnt Hit anything (no block of view)
		if (hit.collider == null) {
			// In Line of Sight
			return true;
		}
		// Default return false (view Blocked)
		return false;
	}


	/// <summary> Function to Block the View behind player </summary>
	private void BlockView () {
		// Go through each SightBlock
		foreach (SightBlock b in sightBlocks) {
			// Deactivate all SightBlocks
			b.Deactivate ();
		}
		// Activate correct block behind player, based on Gem ExitPoint Position aka Attack Direction
		sightBlocks [gemIndex].Activate ();
	}


	// Override of StopAttack function inherited from Character
	public override void StopAttack () {
		// Stop spellbook from Casting
		spellBook.StopCasting ();
		// Call Base StopAttack from Character
		base.StopAttack ();
	}
}