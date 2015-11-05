using UnityEngine;
using System.Collections;

public class VehicleUnit : UnitProperties
{
	
    public VehicleUnit(int health, int damage, int actionPoints, float attackRange) : base (health, damage, actionPoints, attackRange)
    {
        this.health = health;
        this.damage = damage;
        this.actionPoints = actionPoints;
        this.attackRange = attackRange;
    }

    // Use this for initialization
	void Start ()
    {
        _audio = GetComponent<AudioSource>();
        health = 200;
        damage = 20;
        actionPoints = 5;
        attackRange = 15;
	}
	
	
    public override void Attack(UnitProperties target)
    {
        
    }

    public override void Move()
    {
       
    }

    public override IEnumerator PlaySoundTest()
    {
        yield return new WaitForSeconds(_audio.clip.length);
    }
}
