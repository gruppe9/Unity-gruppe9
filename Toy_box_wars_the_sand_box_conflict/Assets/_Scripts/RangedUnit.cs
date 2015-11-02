using UnityEngine;
using System.Collections;

public class RangedUnit : UnitProperties {


    public RangedUnit(int health, int damage, int actionPoints, float attackRange) : base (health, damage, actionPoints, attackRange)
    {
        this.health = health;
        this.damage = damage;
        this.actionPoints = actionPoints;
        this.attackRange = attackRange;
    }

    // Use this for initialization
    void Start ()
    {
        health = 100;
        damage = 100;
        actionPoints = 5;
        attackRange = 25;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
