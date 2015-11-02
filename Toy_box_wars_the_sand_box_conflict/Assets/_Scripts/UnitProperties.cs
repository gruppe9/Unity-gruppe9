using UnityEngine;
using System.Collections;

public class UnitProperties : MonoBehaviour {

    [SerializeField]
    protected int health;
    [SerializeField]
    protected int damage;
    [SerializeField]
    protected int actionPoints;
    [SerializeField]
    protected float attackRange;

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
