using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class UnitProperties : MonoBehaviour
{

    #region Fields
    [SerializeField]
    protected AudioClip attackSFX;
    [SerializeField]
    protected AudioClip attackBuildUpSFX;
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

    public List<Node> currentPath = null;
    public int tileX;
    public int tileZ;

    private int timer = 0; // pathfinding timer
    public int timerSet = 50; // what pathfinding-timer should be reset to
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
        PathFinding();
    }

    void PathFinding()
    {
        if (currentPath != null) // check if we have a path
        {
            if (currentPath.Count > 0) // check if our path more than 0 points
            {
                timer--; // decress timer by one (should probably add something with delta time)
                if (timer <= 0)
                {
                    // set the current point to move to
                    GetComponent<NavMeshAgent>().SetDestination(new Vector3(currentPath[0].x + MapStuff.Instance.tileSize / 1.75f - 0.25f, 0, currentPath[0].z + MapStuff.Instance.tileSize / 1.75f - 0.25f));
                    currentPath.RemoveAt(0); // remove the point we're moveing to
                    timer = timerSet; // reset timer
                }
            }
        }
    }

    public abstract void Attack(UnitProperties target);
    public abstract void Move(Vector3 movePoint);
    public abstract IEnumerator PlaySoundTest();
}
