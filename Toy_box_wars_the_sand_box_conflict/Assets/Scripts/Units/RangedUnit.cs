using UnityEngine;
using System.Collections;
using System;

public class RangedUnit : UnitProperties
{
    private Animator anim;

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
        anim = GetComponent<Animator>();
        _audio.clip = attackSFX;
        //health = 100;
        //damage = 10;
        //actionPoints = 5;
        //attackRange = 15;
    }

    public override void Attack(UnitProperties target)
    {
        target.Health -= damage;

        _audio.clip = attackSFX;
        _audio.Play();

        actionPoints -= attackCost;
    }
    public override void Move(Vector3 movePoint)
    {


    }
}
