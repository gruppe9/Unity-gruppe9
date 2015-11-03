using UnityEngine;
using System.Collections;

public class UnitProperties : MonoBehaviour {

    #region Fields
    [SerializeField]
    protected int health;
    [SerializeField]
    protected int damage;
    [SerializeField]
    protected int actionPoints;
    [SerializeField]
    protected float attackRange;
    #endregion

    #region Properties
    public int Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }

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

    public int ActionPoints
    {
        get
        {
            return actionPoints;
        }

        set
        {
            actionPoints = value;
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
    #endregion

    public UnitProperties(int health, int damage, int actionPoints, float attackRange)
    {

    }
    
    // Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
