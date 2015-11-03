using UnityEngine;
using System.Collections;

public class MeleeUnit : UnitProperties {

    public MeleeUnit(int health, int damage, int actionPoints, float attackRange) : base (health, damage, actionPoints, attackRange)
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
        damage = 10;
        actionPoints = 5;
        attackRange = 7;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
