using UnityEngine;
using System.Collections;
using NUnit.Framework;

[TestFixture]
public class PlayerTest
{

    [Test]
    public void MeleeUnitAttackTest()
    {
        MeleeUnit testAttacker = new MeleeUnit(100, 10, 10, float.MaxValue);
        MeleeUnit testDefender = new MeleeUnit(100, 10, 10, float.MaxValue);

        testAttacker.Attack(testDefender);

        int expected = 90;
        int actual = testDefender.Health;


        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void MeleeAgainstRangedUnitAttackTest()
    {
        MeleeUnit testAttacker = new MeleeUnit(100, 10, 10, float.MaxValue);
        RangedUnit testDefender = new RangedUnit(100, 10, 10, float.MaxValue);

        testAttacker.Attack(testDefender);

        int expected = 90;
        int actual = testDefender.Health;


        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void MeleeAgainstVehicleUnitAttackTest()
    {
        MeleeUnit testAttacker = new MeleeUnit(100, 10, 10, float.MaxValue);
        VehicleUnit testDefender = new VehicleUnit(100, 10, 10, float.MaxValue);

        testAttacker.Attack(testDefender);

        int expected = 90;
        int actual = testDefender.Health;


        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void RangedUnitAttackTest()
    {
        RangedUnit testAttacker = new RangedUnit(100, 10, 10, float.MaxValue);
        RangedUnit testDefender = new RangedUnit(100, 10, 10, float.MaxValue);

        testAttacker.Attack(testDefender);

        int expected = 90;
        int actual = testDefender.Health;


        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void RangedAgainstMeleeUnitAttackTest()
    {
        RangedUnit testAttacker = new RangedUnit(100, 10, 10, float.MaxValue);
        MeleeUnit testDefender = new MeleeUnit(100, 10, 10, float.MaxValue);

        testAttacker.Attack(testDefender);

        int expected = 90;
        int actual = testDefender.Health;


        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void RangedAgainstVehicleUnitAttackTest()
    {
        RangedUnit testAttacker = new RangedUnit(100, 10, 10, float.MaxValue);
        VehicleUnit testDefender = new VehicleUnit(100, 10, 10, float.MaxValue);

        testAttacker.Attack(testDefender);

        int expected = 90;
        int actual = testDefender.Health;


        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void VehichleUnitAttackTest()
    {
        VehicleUnit testAttacker = new VehicleUnit(100, 10, 10, float.MaxValue);
        VehicleUnit testDefender = new VehicleUnit(100, 10, 10, float.MaxValue);

        testAttacker.Attack(testDefender);

        int expected = 90;
        int actual = testDefender.Health;


        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public void VehicleAgainstMeleeUnitAttackTest()
    {
        VehicleUnit testAttacker = new VehicleUnit(100, 10, 10, float.MaxValue);
        MeleeUnit testDefender = new MeleeUnit(100, 10, 10, float.MaxValue);

        testAttacker.Attack(testDefender);

        int expected = 90;
        int actual = testDefender.Health;


        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void VehicleAgainstRangedUnitAttackTest()
    {
        VehicleUnit testAttacker = new VehicleUnit(100, 10, 10, float.MaxValue);
        RangedUnit testDefender = new RangedUnit(100, 10, 10, float.MaxValue);

        testAttacker.Attack(testDefender);

        int expected = 90;
        int actual = testDefender.Health;


        Assert.AreEqual(expected, actual);
    }   
}
