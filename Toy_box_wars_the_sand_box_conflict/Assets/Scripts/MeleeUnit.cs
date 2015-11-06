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
        _audio = GetComponent<AudioSource>();
        health = 100;
        damage = 10;
        actionPoints = 5;
        attackRange = 7;
    }
	
    public override void Attack(UnitProperties target)
    {
        target.Health -= damage;

        StartCoroutine(PlaySoundTest());

        actionPoints -= attackCost;
    }

    public override void Move()
    {
        
    }

    public override IEnumerator PlaySoundTest()
    {
        _audio.clip = moveSFX;
        _audio.Play();
        yield return new WaitForSeconds(_audio.clip.length);
        _audio.clip = attackSFX;
        _audio.Play();
    }


}
