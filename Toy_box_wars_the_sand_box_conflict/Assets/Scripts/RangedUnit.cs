﻿using UnityEngine;
using System.Collections;

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
        health = 10;
        damage = 10;
        actionPoints = 5;
        attackRange = 15;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
