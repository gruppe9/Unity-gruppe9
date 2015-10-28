using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

    [SerializeField]
    private int damage;
    [SerializeField]
    private int health;
    [SerializeField]
    private float attackRange; 
    private int id;


    public int Damage
    {
        get
        {
            return damage;
        }

        set
        {
            damage = value;
        }
    }
    public int Health
    {
        get
        {
            if(health < 0)
            {
                health = 0;
            }
            return health;
        }

        set
        {
            health = value;
        }
    }
    public float AttackRange
    {
        get
        {
            return attackRange;
        }

        set
        {
            attackRange = value;
        }
    }

    // Use this for initialization
    void Start ()
    {
        damage = 10;
        health = 100;
        attackRange = 5f;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
}
