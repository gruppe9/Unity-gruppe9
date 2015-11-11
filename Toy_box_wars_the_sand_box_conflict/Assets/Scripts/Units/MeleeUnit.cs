using UnityEngine;
using System.Collections;

public class MeleeUnit : UnitProperties
{
    RaycastHit hit;
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

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
        hit = new RaycastHit();
    }
	
    public override void Attack(UnitProperties target)
    {
        Vector3 direction = target.transform.position - transform.position;

        if (Physics.Raycast(transform.position, direction, out hit))
        {
            Debug.Log(hit.collider.gameObject.ToString());
            Debug.DrawRay(transform.position, direction, Color.red);

            if (hit.collider.tag == target.tag)
            {
                if (actionPoints >= attackCost && attackCost != 0 && damage >= 0)
                {
                    target.Health -= damage;
                    actionPoints -= attackCost;
                    Debug.Log("Enemy hit");

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
                else
                {
                    Debug.Log("Error 520: Ranged unit not enough action points - Or damage is less than zero");
                }
            }
            else
            {
                Debug.Log("Hit something else");
            }
        }
    }

    public override void Move(Vector3 movePoint)
    {
        GetComponent<NavMeshAgent>().SetDestination(movePoint);
    }
}
