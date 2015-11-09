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
        //health = 100;
        //damage = 10;
        //actionPoints = 5;
        //attackRange = 7;
    }
	
    public override void Attack(UnitProperties target)
    {
        if (actionPoints >= attackCost && attackCost != 0 && damage >= 0)
        {
            target.Health -= damage;
            actionPoints -= attackCost;

            //Sound stuff
            if (_audio != null && _audio.clip != null)
            {
                _audio.clip = attackSFX;
                _audio.Play();
            }
            else
            {
                Debug.Log("Error 31: Check Sound Source/Clip on melee unit");
            }
        }
        else
        {
            Debug.Log("Error 520: Melee unit not enough action points - Or damage is less than zero");
        }
    }

    public override void Move(Vector3 movePoint)
    {
        GetComponent<NavMeshAgent>().SetDestination(movePoint);
    }
}
