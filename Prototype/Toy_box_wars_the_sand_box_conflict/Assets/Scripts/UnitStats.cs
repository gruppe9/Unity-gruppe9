using UnityEngine;
using System.Collections;

public class UnitStats : MonoBehaviour {

    [SerializeField]
    private int damage;
    [SerializeField]
    private int health;
    [SerializeField]
    private float attackRange; 
    private int id;
    private static UnitStats instance;
    private bool isSeleccted;


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
    public static UnitStats Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<UnitStats>();
            }
            return instance;
        }
    }
    public bool IsSeleccted
    {
        get
        {
            return isSeleccted;
        }

        set
        {
            isSeleccted = value;
        }
    }

    // Use this for initialization
    void Start ()
    {
        //damage = 10;
        //health = 100;
        //attackRange = 5f;
        isSeleccted = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
}
