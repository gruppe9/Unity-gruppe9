﻿using UnityEngine;
using System.Collections;
using System;

public class RangedUnit : UnitProperties
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="health"></param>
    /// <param name="damage"></param>
    /// <param name="actionPoints"></param>
    /// <param name="attackRange"></param>
    public RangedUnit(int health, int damage, int actionPoints, float attackRange) : base(health, damage, actionPoints, attackRange)
    {
        this.health = health;
        this.damage = damage;
        this.actionPoints = actionPoints;
        this.attackRange = attackRange;
    }
    
    // Use this for initialization
    void Start()
    {
        _audio = GetComponent<AudioSource>();
        health = 100;
        damage = 10;
        actionPoints = 5;
        attackRange = 15;
    }

    public override void Attack(UnitProperties target)
    {
        target.Health -= damage;

        StartCoroutine(PlaySoundTest());

        actionPoints -= attackCost;
    }
    public override void Move(Vector3 movePoint)
    {


    }
    public override IEnumerator PlaySoundTest()
    {
        _audio.clip = attackBuildUpSFX;
        _audio.Play();
        yield return new WaitForSeconds(_audio.clip.length);
        _audio.clip = attackSoundSFX;
        _audio.Play();
    }
}