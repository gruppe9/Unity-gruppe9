using UnityEngine;
using System.Collections;
using System;

public class MeleeUnit : UnitProperties
{
    RaycastHit hit;
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

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
        anim = GetComponent<Animator>();
        _audio.clip = attackSFX;
        hit = new RaycastHit();

        EnterArmy();
    }

    public override void Attack(UnitProperties target)
    {

        if (actionPoints >= attackCost && attackCost != 0 && damage >= 0)
        {
            if (isNotTesting)
            {
                lastTarget = target;
                anim.SetTrigger("Attack");
            }
            target.Health -= damage;
            actionPoints -= attackCost;
            Debug.Log("Enemy hit");
        }
        else
        {
            Debug.Log("Error 520: Ranged unit not enough action points - Or damage is less than zero");
        }
    }

    public override void Move(Vector3 movePoint)
    {
        GetComponent<NavMeshAgent>().SetDestination(movePoint);
    }
    public void PlayAttackSound()
    {
        //Sound stuff
        if (_audio != null && _audio.clip != null)
        {
            _audio.clip = attackSFX;
            _audio.Play();
        }
        else
        {
            Debug.Log("Error 31: Check Sound Source/Clip on Ranged unit");
        }
    }
}
