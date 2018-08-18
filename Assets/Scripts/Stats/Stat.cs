using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat {

	[SerializeField]
	private int baseValue;

	private List<int> modifiers = new List<int>(); // Creates a private list of modifiers to add to base value of stat

	public int GetValue() // Calculates the final value of the current stat with the modifiers list
	{
		int finalValue = baseValue; // sets final values to orignal base value to start
		modifiers.ForEach( x => finalValue += x); // for each value in modifier list as x, add to final value
		return finalValue; // return modified final value
	}

	public void AddModifier (int modifier) // Adds modifier from equiped item to modifiers list
	{
		if (modifier != 0)
			modifiers.Add(modifier);
	}

	public void RemoveModifier (int modifier) // Removes modifier from unequiped item to modifiers list
	{
		if (modifier != 0)
			modifiers.Remove(modifier);
	}
}
