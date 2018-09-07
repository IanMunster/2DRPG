using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Spell.
/// Has properties: Name, Damage, Icon, Speed, CastTime, SpellPrefab and CastbarColor
/// Get with My+NameOfProperty
/// </summary>

[Serializable]
public class Spell {
	// Name of the Spell
	[SerializeField] private string name;
	// Damage of the Spell
	[SerializeField] private int damage;
	// Icon Sprite of the Spell
	[SerializeField] private Sprite icon;
	// Movement Speed of the Spell
	[SerializeField] private float speed;
	// CastTime of the Spell
	[SerializeField] private float castTime;
	// Spell prefab Gameobject of the Spell
	[SerializeField] private GameObject spellPrefab;
	// Color of the Castbar when casting Spell
	[SerializeField] private Color castbarColor;

	// Get Public Properties of Spell
	public string MyName { get { return name; } }
	public int MyDamage { get { return damage; } }
	public Sprite MyIcon { get { return icon; } }
	public float MySpeed { get { return speed; } }
	public float MyCastTime { get { return castTime; } }
	public GameObject MySpellPrefab { get { return spellPrefab; } }
	public Color MyCastbarColor { get { return castbarColor; } }
}
