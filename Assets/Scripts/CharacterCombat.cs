﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour {

	public float attackSpeed = 1f;
	private float attackCooldown = 0f;
	const float comabatCooldown = 5;
	float lastAttackTime;

	public float attackDelay = .6f;

	public bool InCombat { get; private  set; }
	public event System.Action OnAttack;

	CharacterStats myStats;
	CharacterStats opponentStats;

	// Use this for initialization
	void Start ()
	{
		myStats = GetComponent<CharacterStats>();
	}

	void Update ()
	{
		attackCooldown -= Time.deltaTime;

		if(Time.time - lastAttackTime > comabatCooldown)
		{
			InCombat = false;
		}
	}

	public void Attack (CharacterStats targetStats)
	{
		if (attackCooldown <= 0f)
		{

			opponentStats = targetStats;
			if (OnAttack != null)
				OnAttack();

			attackCooldown = 1f / attackSpeed;
			InCombat = true;
			lastAttackTime = Time.time;
		}

	}
	IEnumerator DoDamage (CharacterStats stats, float delay)
	{
		yield return new WaitForSeconds(delay);
		stats.TakeDamage(myStats.damage.GetValue());
		if (stats.currentHealth <= 0)
		{
			InCombat = false;
		}
	}

	public void AttackHit_AnimationEvent()
	{
		opponentStats.TakeDamage(myStats.damage.GetValue());
		if (opponentStats.currentHealth <= 0)
		{
			InCombat = false;
		}
	}
}
