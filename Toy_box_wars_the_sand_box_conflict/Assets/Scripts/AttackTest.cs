using UnityEngine;
using System.Collections;

public class AttackTest : MonoBehaviour {


    
	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
    public void DealDamage()
    {
        if (SelectTest.Instance.SelectedUnit != null && SelectTest.Instance.OtherSelectedUnit != null)
        {
            
            if(SelectTest.Instance.TargetDistance <= SelectTest.Instance.OtherSelectedUnit.GetComponent<Stats>().AttackRange)
            {
                SelectTest.Instance.OtherSelectedUnit.GetComponent<Stats>().Health -= SelectTest.Instance.SelectedUnit.GetComponent<Stats>().Damage;
                if (SelectTest.Instance.OtherSelectedUnit.GetComponent<Stats>().Health <= 0)
                {
                    Destroy(SelectTest.Instance.OtherSelectedUnit);
                }
                Debug.Log("Attack done!");
            }
           
            
        }
    }
}
