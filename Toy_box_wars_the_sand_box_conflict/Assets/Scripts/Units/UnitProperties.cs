using UnityEngine;
using System.Collections;

public abstract class UnitProperties : MonoBehaviour
{

    #region Fields
    [SerializeField]
    protected AudioClip attackSFX;
    [SerializeField]
    protected AudioClip moveSFX;
    [SerializeField]
    protected AudioClip deathSFX;
    [SerializeField]
    protected int health;
    [SerializeField]
    protected int damage;
    [SerializeField]
    protected int actionPoints;
    [SerializeField]
    protected float attackRange;
    [SerializeField]
    protected int initialAP;
    [SerializeField]
    protected int attackCost;
    [SerializeField]
    protected int movementCost;
    protected AudioSource _audio;
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
            if (value < 0)
            {
                health = 0;
            }
            else
            {
                health = value;
            }
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

    public int InitialAP
    {
        get
        {
            return initialAP;
        }

        set
        {
            initialAP = value;
        }
    }

    public int AttackCost
    {
        get
        {
            return attackCost;
        }

        set
        {
            attackCost = value;
        }
    }

    public int MovementCost
    {
        get
        {
            return movementCost;
        }

        set
        {
            movementCost = value;
        }
    }
    #endregion

    public UnitProperties(int health, int damage, int actionPoints, float attackRange)
    {

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public abstract void Attack(UnitProperties target);
    public abstract void Move(Vector3 movePoint);
}
