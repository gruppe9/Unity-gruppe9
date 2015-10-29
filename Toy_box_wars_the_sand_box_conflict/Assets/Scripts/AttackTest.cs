using UnityEngine;
using System.Collections;

public class AttackTest : MonoBehaviour {


   
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
    public void DealDamage()
    {
        if (SelectTest.Instance.SelectedUnit != null && SelectTest.Instance.OtherSelectedUnit != null)
        {            
            if(SelectTest.Instance.TargetDistance <= SelectTest.Instance.SelectedUnit.GetComponent<UnitStats>().AttackRange)
            {
                SelectTest.Instance.OtherSelectedUnit.GetComponent<UnitStats>().Health -= SelectTest.Instance.SelectedUnit.GetComponent<UnitStats>().Damage;
                Debug.Log("Attack done!");
                if (SelectTest.Instance.OtherSelectedUnit.GetComponent<UnitStats>().Health <= 0)
                {
                    Destroy(SelectTest.Instance.OtherSelectedUnit);
                    Debug.Log("Target destroyed!");
                }                
            }
           
            
        }
    }
}
