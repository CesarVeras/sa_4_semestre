using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PowerUp", menuName = "PowerUp")]
public class PowerUp : ScriptableObject
{
    public string objectName;
    public float extraLife;
    public float lifeRegen;
    public float damage;
    public float fireRate;
    public float shotSpeed;
    public float speed;
    public float luck;
    public float range;
    public int bombs;
    public int money;
}
