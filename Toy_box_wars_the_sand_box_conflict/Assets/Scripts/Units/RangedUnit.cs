using UnityEngine;
using System.Collections;
using System;

public class RangedUnit : UnitProperties
{

    private RaycastHit hit;
    private Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
        _audio.clip = attackSFX;
        anim = GetComponent<Animator>();
        //health = 100;
        //damage = 10;
        //actionPoints = 5;
        //attackRange = 15;
        hit = new RaycastHit();
    }
    public override void Attack(UnitProperties target)
    {
        //Vector3 direction = target.transform.position - transform.position;


        if (actionPoints >= attackCost && attackCost != 0 && damage >= 0)
        {
            target.Health -= damage;
            actionPoints -= attackCost;
            Debug.Log("Enemy hit");

            if(isNotTesting)
                anim.SetTrigger("Attack");
        }
        else
        {
            Debug.Log("Error 520: Ranged unit not enough action points - Or damage is less than zero");
        }
        
    }

    public override void Move(Vector3 movePoint)
    {


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
