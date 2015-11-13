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
    protected bool isNotTesting = true;
    protected Animator anim;
    protected UnitProperties lastTarget;

    public List<Node> currentPath = null;
    public int tileX;
    public int tileZ;

    private float timer = 0; // pathfinding timer
    public float timerSet = 50; // what pathfinding-timer should be reset to

    protected Animator myAnimator;

    private Vector3 lastFramePosition;
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

    public bool IsNotTesting
    {
        get
        {
            return isNotTesting;
        }

        set
        {
            isNotTesting = value;
        }
    }
    #endregion

    public UnitProperties(int health, int damage, int actionPoints, float attackRange)
    {

    }

    // Use this for initialization
    void Start()
    {
        lastFramePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        PathFinding();
        RunAnimation();
    }

    void PathFinding()
    {

        if (currentPath != null) // check if we have a path
        {
            if (currentPath.Count > 0) // check if our path more than 0 points
            {
                timer -= (1 * Time.deltaTime); // decress timer by one (should probably add something with delta time)
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

    void RunAnimation()
    {
        // we nicked this code from http://answers.unity3d.com/questions/285682/add-animation-to-a-navmeshagent-character.html
        float distance = Vector3.Distance(lastFramePosition, transform.position);
        lastFramePosition = transform.position;

        float currentSpeed = Mathf.Abs(distance) / Time.deltaTime;

        if (currentSpeed > 0.01f)
        {
            if (myAnimator == null)
            {
                myAnimator = GetComponent<Animator>();
            }
            myAnimator.SetBool("isRunning", true);
        }
        else
        {
            if (myAnimator == null)
            {
                myAnimator = GetComponent<Animator>();
            }
            myAnimator.SetBool("isRunning", false);
        }
    }

    public void TakeDmg()
    {
        lastTarget.anim.SetTrigger("TakeDmg");
    }

    public void EnterArmy()
    {
        if (gameObject.tag == "team1")
        {
            ArmySaves.Armies[0].Add(gameObject);
        }
        if (gameObject.tag == "team2")
        {
            ArmySaves.Armies[1].Add(gameObject);
        }
    }

    public abstract void Attack(UnitProperties target);
    public abstract void Move(Vector3 movePoint);
}
