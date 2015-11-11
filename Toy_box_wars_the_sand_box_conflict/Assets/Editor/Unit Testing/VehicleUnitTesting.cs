using UnityEngine;
using System.Collections;
using NUnit.Framework;

[TestFixture]
public class VehicleUnitTesting
{

    [Test]
    public void AttackTest()
    {
        VehicleUnit testAttacker = new VehicleUnit(100, 10, 10, float.MaxValue);
        VehicleUnit testDefender = new VehicleUnit(100, 10, 10, float.MaxValue);
        testAttacker.AttackCost = 5;

        testAttacker.Attack(testDefender);

        int expected = 90;
        int actual = testDefender.Health;


        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void AgainstMeleeUnitAttackTest()
    {
        VehicleUnit testAttacker = new VehicleUnit(100, 10, 10, float.MaxValue);
        MeleeUnit testDefender = new MeleeUnit(100, 10, 10, float.MaxValue);
        testAttacker.AttackCost = 5;

        testAttacker.Attack(testDefender);

        int expected = 90;
        int actual = testDefender.Health;


        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void AgainstRangedUnitAttackTest()
    {
        VehicleUnit testAttacker = new VehicleUnit(100, 10, 10, float.MaxValue);
        RangedUnit testDefender = new RangedUnit(100, 10, 10, float.MaxValue);
        testAttacker.AttackCost = 5;

        testAttacker.Attack(testDefender);

        int expected = 90;
        int actual = testDefender.Health;


        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void AttackOverkillDeath()
    {
        VehicleUnit testAttacker = new VehicleUnit(100, 10, 10, float.MaxValue);
        VehicleUnit testDefender = new VehicleUnit(2, 10, 10, float.MaxValue);
        testAttacker.AttackCost = 5;

        testAttacker.Attack(testDefender);

        int expected = 0;
        int actual = testDefender.Health;

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void AttackIfAPIsGreaterThanAC()
    {
        VehicleUnit testAttacker = new VehicleUnit(100, 10, 10, float.MaxValue);
        VehicleUnit testDefender = new VehicleUnit(100, 10, 10, float.MaxValue);
        testAttacker.AttackCost = 5;

        testAttacker.Attack(testDefender);

        int expectedAP = 5;
        int actualAP = testAttacker.ActionPoints;

        int expedtedHP = 90;
        int actualHP = testDefender.Health;

        Assert.AreEqual(expectedAP, actualAP);
        Assert.AreEqual(expedtedHP, actualHP);
    }

    [Test]
    public void NoAttackIfAPIsLessThanAC()
    {
        VehicleUnit testAttacker = new VehicleUnit(100, 10, 10, float.MaxValue);
        VehicleUnit testDefender = new VehicleUnit(100, 10, 10, float.MaxValue);
        testAttacker.AttackCost = 15;

        testAttacker.Attack(testDefender);

        int expectedAP = 10;
        int actualAP = testAttacker.ActionPoints;

        int expedtedHP = 100;
        int actualHP = testDefender.Health;

        Assert.AreEqual(expectedAP, actualAP);
        Assert.AreEqual(expedtedHP, actualHP);
    }

    [Test]
    public void NoAttackIfAPIsZero()
    {
        VehicleUnit testAttacker = new VehicleUnit(100, 10, 0, float.MaxValue);
        VehicleUnit testDefender = new VehicleUnit(100, 10, 10, float.MaxValue);
        testAttacker.AttackCost = 1;

        testAttacker.Attack(testDefender);

        int expectedAP = 0;
        int actualAP = testAttacker.ActionPoints;

        int expedtedHP = 100;
        int actualHP = testDefender.Health;

        Assert.AreEqual(expectedAP, actualAP);
        Assert.AreEqual(expedtedHP, actualHP);
    }

    [Test]
    public void NoDamageWhenLessThanZero()
    {
        VehicleUnit testAttacker = new VehicleUnit(100, -10, 10, float.MaxValue);
        VehicleUnit testDefender = new VehicleUnit(100, 10, 10, float.MaxValue);
        testAttacker.AttackCost = 5;

        testAttacker.Attack(testDefender);

        int expedtedHP = 100;
        int actualHP = testDefender.Health;

        Assert.AreEqual(expedtedHP, actualHP);
    }

    [Test]
    public void NoAttackIfACIsZero()
    {
        VehicleUnit testAttacker = new VehicleUnit(100, 10, 10, float.MaxValue);
        VehicleUnit testDefender = new VehicleUnit(100, 10, 10, float.MaxValue);
        testAttacker.AttackCost = 0;

        testAttacker.Attack(testDefender);

        int expectedAP = 10;
        int actualAP = testAttacker.ActionPoints;

        int expedtedHP = 100;
        int actualHP = testDefender.Health;

        Assert.AreEqual(expectedAP, actualAP);
        Assert.AreEqual(expedtedHP, actualHP);
    }
}
