using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    // enums
    [HideInInspector] public enum Firing { NoFiring, FiringLeft, FiringRight, FiringUp, FiringDown }

    // stats
    [HideInInspector] public float totalLifes;
    [HideInInspector] public float lifes;
    [HideInInspector] public float damage;
    [HideInInspector] public float range;
    [HideInInspector] public float fireRate;
    [HideInInspector] public float shotSpeed;
    [HideInInspector] public float speed;

    // helper stats
    [HideInInspector] public float damageTaken;
    [HideInInspector] public bool canFire;

    [HideInInspector] public Firing firingState;

    public abstract void MovementControl();
    public abstract void FiringControl();
    public abstract void TakeDamage();
}
