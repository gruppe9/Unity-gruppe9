using UnityEngine;
using System.Collections;
using NUnit.Framework;

[TestFixture]
public class PlayerTest
{
  
    [Test]
    public void MeleeUnitAttackTest()
    {
        MeleeUnit testMeleeAttacker = new MeleeUnit(100, 10, 10, float.MaxValue);
        MeleeUnit testDefender = new MeleeUnit(100, 10, 10, float.MaxValue);

        testMeleeAttacker.Attack(testDefender);
        
        int expected = 90;
        int actual = testDefender.Health;


        Assert.AreEqual(expected, actual);
    }
}
