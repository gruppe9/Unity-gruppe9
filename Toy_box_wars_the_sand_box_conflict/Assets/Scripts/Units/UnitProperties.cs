using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class UnitProperties : MonoBehaviour
{

    #region Fields
    [SerializeField]
    protected AudioClip attackSoundSFX;
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

    private int timer = 0;
    public int timerSet = 50;
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
        if (currentPath != null)
        {
            if (currentPath.Count > 0)
            {

                //if (Vector3.Distance(transform.position, new Vector3(currentPath[0].x, 0, currentPath[0].z)) < 1)
                //{
                //    currentPath.RemoveAt(0);
                //}
                //else if (GetComponent<NavMeshAgent>().destination != new Vector3(currentPath[0].x, 0, currentPath[0].z))
                //{
                //    GetComponent<NavMeshAgent>().SetDestination(new Vector3(currentPath[0].x, 0, currentPath[0].z));
                //}


                Debug.Log("I have a path with: " + currentPath.Count + " points, ending at:" + currentPath[0].x + ", " + currentPath[0].z);
                timer--;
                if (timer <= 0)
                {
                    Debug.Log("I'll try to move");
                    foreach (Node item in currentPath)
                    {
                        Debug.Log(item.x + ", " + item.z);
                    }
                    GetComponent<NavMeshAgent>().SetDestination(new Vector3(currentPath[0].x + MapStuff.Instance.tileSize / 1.75f - 0.25f, 0, currentPath[0].z + MapStuff.Instance.tileSize / 1.75f - 0.25f));
                    currentPath.RemoveAt(0);
                    timer = timerSet;
                }

            }
        }
    }

    public abstract void Attack(UnitProperties target);
    public abstract void Move(Vector3 movePoint);
    public abstract IEnumerator PlaySoundTest();
}
