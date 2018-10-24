using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    // enums
    protected enum Firing { NoFiring, FiringLeft, FiringRight, FiringUp, FiringDown }

    // stats
    protected float totalLifes;
    protected float lifes;

    protected float damage;
    protected float range;
    protected float fireRate;
    protected float shotSpeed;
    protected float speed;

    protected bool canFire;

    protected Firing firingState;

    public abstract void MovementControl();
    public abstract void FiringControl();
    public abstract void TakeDamage();

    public float Lifes
    {
        get
        {
            return lifes;
        }
        set
        {
            lifes = value;
        }
    }

    public float Damage
    {
        get
        {
            return damage;
        }

        set {
            damage = value;
        }
    }
}
