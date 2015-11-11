using UnityEngine;
using System.Collections;
using NUnit.Framework;

[TestFixture]
public class RangedUnitTesting
{

    [Test]
    public void AgainstMeleeUnitAttackTest()
    {
        RangedUnit testAttacker = new RangedUnit(100, 10, 10, float.MaxValue);
        MeleeUnit testDefender = new MeleeUnit(100, 10, 10, float.MaxValue);


        testAttacker.AttackCost = 1;
        testAttacker.IsNotTesting = false;

        testAttacker.Attack(testDefender);

        int expected = 90;
        int actual = testDefender.Health;


        Assert.AreEqual(expected, actual);
    }
    [Test]
    public void AttackTest()
    {
        RangedUnit testAttacker = new RangedUnit(100, 10, 10, float.MaxValue);
        RangedUnit testDefender = new RangedUnit(100, 10, 10, float.MaxValue);
        testAttacker.AttackCost = 1;
        testAttacker.IsNotTesting = false;
        testAttacker.Attack(testDefender);

        int expected = 90;
        int actual = testDefender.Health;


        Assert.AreEqual(expected, actual);
    }
    [Test]
    public void AgainstVehicleUnitAttackTest()
    {
        RangedUnit testAttacker = new RangedUnit(100, 10, 10, float.MaxValue);
        VehicleUnit testDefender = new VehicleUnit(100, 10, 10, float.MaxValue);
        testAttacker.AttackCost = 1;
        testAttacker.IsNotTesting = false;
        testAttacker.Attack(testDefender);

        int expected = 90;
        int actual = testDefender.Health;


        Assert.AreEqual(expected, actual);
    }
    [Test]
    public void AttackOverkillDeath()
    {
        RangedUnit testAttacker = new RangedUnit(100, 101, 10, float.MaxValue);
        RangedUnit testDefender = new RangedUnit(100, 10, 10, float.MaxValue);
        testAttacker.AttackCost = 1;
        testAttacker.IsNotTesting = false;
        testAttacker.Attack(testDefender);

        int expected = 0;
        int actual = testDefender.Health;


        Assert.AreEqual(expected, actual);
    }
    [Test]
    public void AttackIfAPIsGreaterThanAC()
    {
        RangedUnit testAttacker = new RangedUnit(100, 10, 10, float.MaxValue);
        RangedUnit testDefender = new RangedUnit(100, 10, 10, float.MaxValue);
        testAttacker.AttackCost = 3;
        testAttacker.IsNotTesting = false;
        testAttacker.Attack(testDefender);

        int expectedAC = 7;
        int actualAC = testAttacker.ActionPoints;
        int expctedHP = 90;
        int actualHP = testDefender.Health;


        Assert.AreEqual(expectedAC, actualAC);
        Assert.AreEqual(expctedHP, actualHP);
    }
    [Test]
    public void NoAttackIfAPIsLessThanAC()
    {
        RangedUnit testAttacker = new RangedUnit(100, 100, 10, float.MaxValue);
        RangedUnit testDefender = new RangedUnit(100, 10, 10, float.MaxValue);
        testAttacker.AttackCost = 11;
        testAttacker.IsNotTesting = false;
        testAttacker.Attack(testDefender);

        int expectedAP = 10;
        int actualAP = testAttacker.ActionPoints;
        int expectedHealth = 100;
        int actualHealth = testDefender.Health;


        Assert.AreEqual(expectedAP, actualAP);
        Assert.AreEqual(expectedHealth, actualHealth);

    }
    [Test]
    public void NoAttackIfAPIsZero()
    {
        RangedUnit testAttacker = new RangedUnit(100, 100, 0, float.MaxValue);
        RangedUnit testDefender = new RangedUnit(100, 10, 10, float.MaxValue);
        testAttacker.AttackCost = 1;
        testAttacker.IsNotTesting = false;
        testAttacker.Attack(testDefender);

        int expectedAP = 0;
        int actualAP = testAttacker.ActionPoints;

        int expectedHP = 100;
        int actualHP = testDefender.Health;

        Assert.AreEqual(expectedAP, actualAP);
        Assert.AreEqual(expectedHP, actualHP);

    }
    [Test]
    public void NoDamageWhenLessThanZero()
    {
        RangedUnit testAttacker = new RangedUnit(100, -100, 10, float.MaxValue);
        RangedUnit testDefender = new RangedUnit(100, 10, 10, float.MaxValue);
        testAttacker.AttackCost = 1;
        testAttacker.IsNotTesting = false;
        testAttacker.Attack(testDefender);

        int expected = 100;
        int actual = testDefender.Health;
        Assert.AreEqual(expected, actual);

    }
    [Test]
    public void NoAttackIfACIsZero()
    {
        RangedUnit testAttacker = new RangedUnit(100, -100, 10, float.MaxValue);
        RangedUnit testDefender = new RangedUnit(100, 10, 10, float.MaxValue);
        testAttacker.AttackCost = 0;
        testAttacker.IsNotTesting = false;
        testAttacker.Attack(testDefender);

        int expectedAP = 10;
        int actualAP = testAttacker.ActionPoints;
        int expected = 100;
        int actual = testDefender.Health;

        Assert.AreEqual(expectedAP, actualAP);
        Assert.AreEqual(expected, actual);

    }
}
