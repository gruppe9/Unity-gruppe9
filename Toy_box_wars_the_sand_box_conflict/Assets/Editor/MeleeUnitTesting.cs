using UnityEngine;
using System.Collections;
using NUnit.Framework;

[TestFixture]
public class MeleeUnitTesting 
{
    [Test]
    public void AttackTest()
    {
        MeleeUnit testAttacker = new MeleeUnit(100, 10, 10, float.MaxValue);
        MeleeUnit testDefender = new MeleeUnit(100, 10, 10, float.MaxValue);
        testAttacker.AttackCost = 1;

        testAttacker.Attack(testDefender);

        int expected = 90;
        int actual = testDefender.Health;


        Assert.AreEqual(expected, actual);
    }
    [Test]
    public void AgainstRangedUnitAttackTest()
    {
        MeleeUnit testAttacker = new MeleeUnit(100, 10, 10, float.MaxValue);
        RangedUnit testDefender = new RangedUnit(100, 10, 10, float.MaxValue);
        testAttacker.AttackCost = 1;
        testAttacker.Attack(testDefender);

        int expected = 90;
        int actual = testDefender.Health;


        Assert.AreEqual(expected, actual);
    }
    [Test]
    public void AgainstVehicleUnitAttackTest()
    {
        MeleeUnit testAttacker = new MeleeUnit(100, 10, 10, float.MaxValue);
        VehicleUnit testDefender = new VehicleUnit(100, 10, 10, float.MaxValue);
        testAttacker.AttackCost = 1;

        testAttacker.Attack(testDefender);

        int expected = 90;
        int actual = testDefender.Health;


        Assert.AreEqual(expected, actual);
    }
    [Test]
    public void AttackOverkillDeath()
    {
        MeleeUnit testAttacker = new MeleeUnit(100, 101, 10, float.MaxValue);
        MeleeUnit testDefender = new MeleeUnit(100, 10, 10, float.MaxValue);
        testAttacker.AttackCost = 1;

        testAttacker.Attack(testDefender);

        int expected = 0;
        int actual = testDefender.Health;


        Assert.AreEqual(expected, actual);
    }
    [Test]
    public void AttackIfAPIsGreaterThanAC()
    {
        MeleeUnit testAttacker = new MeleeUnit(100, 10, 10, float.MaxValue);
        MeleeUnit testDefender = new MeleeUnit(100, 10, 10, float.MaxValue);
        testAttacker.AttackCost = 3;

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
        MeleeUnit testAttacker = new MeleeUnit(100, 100, 10, float.MaxValue);
        MeleeUnit testDefender = new MeleeUnit(100, 10, 10, float.MaxValue);
        testAttacker.AttackCost = 11;

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
        MeleeUnit testAttacker = new MeleeUnit(100, 100, 0, float.MaxValue);
        MeleeUnit testDefender = new MeleeUnit(100, 10, 10, float.MaxValue);
        testAttacker.AttackCost = 1;

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
        MeleeUnit testAttacker = new MeleeUnit(100, -100, 10, float.MaxValue);
        MeleeUnit testDefender = new MeleeUnit(100, 10, 10, float.MaxValue);
        testAttacker.AttackCost = 1;
        testAttacker.Attack(testDefender);

        int expected = 100;
        int actual = testDefender.Health;
        Assert.AreEqual(expected, actual);

    }
    [Test]
    public void NoAttackIfACIsZero()
    {
        MeleeUnit testAttacker = new MeleeUnit(100, -100, 10, float.MaxValue);
        MeleeUnit testDefender = new MeleeUnit(100, 10, 10, float.MaxValue);
        testAttacker.AttackCost = 0;
        testAttacker.Attack(testDefender);

        int expectedAP = 10;
        int actualAP = testAttacker.ActionPoints;
        int expected = 100;
        int actual = testDefender.Health;

        Assert.AreEqual(expectedAP, actualAP);
        Assert.AreEqual(expected, actual);

    }
}
