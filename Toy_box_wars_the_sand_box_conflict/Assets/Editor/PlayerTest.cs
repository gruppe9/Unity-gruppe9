using UnityEngine;
using System.Collections;
using NUnit.Framework;

public class PlayerTest : MonoBehaviour
{
  
    [Test]
    public void AttackTest()
    {
        GameObject unit1 = new GameObject();
        unit1.AddComponent<UnitProperties>();
        unit1.GetComponent<UnitProperties>().Damage = 10;
        unit1.GetComponent<UnitProperties>().AttackRange = 10;
        unit1 = GetComponent<Player>().SelectedUnit;

        Player unit2 = new Player();
        unit2.SelectedOther.AddComponent<UnitProperties>();
        unit2.SelectedOther.GetComponent<UnitProperties>().Health = 100;

        
        int expected = 90;
        int actual = unit2.GetComponent<UnitProperties>().Health;

        Assert.AreEqual(expected, actual);

    }
}
