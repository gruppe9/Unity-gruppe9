using UnityEngine;
using System.Collections;

public class MeleeUnit : UnitProperties
{


    public MeleeUnit(int health, int damage, int actionPoints, float attackRange) : base(health, damage, actionPoints, attackRange)
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
        //health = 100;
        //damage = 10;
        //actionPoints = 5;
        //attackRange = 7;
    }

    public override void Attack(UnitProperties target)
    {
        if (actionPoints >= attackCost && damage > 0 && attackCost != 0)
        {
            target.Health -= damage;

            if (_audio && _audio.clip != null)
            {
                _audio.clip = attackSFX;
                _audio.Play();
            }
            else
            {
                Debug.Log("Error: Melee Unit - Check sound clip or sound source");
            }

            actionPoints -= attackCost;
        }
        else
        {
            Debug.Log("Error: Melee Unit - AP too low or damage less than zero");
        }


    }

    public override void Move(Vector3 movePoint)
    {
        GetComponent<NavMeshAgent>().SetDestination(movePoint);
    }
}
