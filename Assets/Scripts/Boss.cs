using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Enemy
{
    private GameObject player;
    private float initialTime;
    private float currentTime;
    public GameObject bossAmmo;
    public Transform firingPoint;
    public GameObject WinExit;
    public Image bossLife;

    // Use this for initialization
    void Start()
    {
        totalLifes = 100;
        lifes = totalLifes;
        damage = 3f;
        range = 5f;
        fireRate = 0.7f;
        shotSpeed = 5.5f;
        speed = 0;

        player = GameObject.Find("Player");
        initialTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - initialTime >= fireRate)
        {
            Fire();
            initialTime = Time.time;
        }
        bossLife.fillAmount = lifes / totalLifes;
    }

    void Fire()
    {
        GameObject ammo = Instantiate(bossAmmo, firingPoint.position, Quaternion.identity);
        BossAmmo ammoScript = ammo.GetComponent<BossAmmo>();
        ammoScript.damage = damage;
        ammoScript.speed = shotSpeed;
        ammoScript.pointToGo = player.transform.position;
    }


    public override void TakeDamage()
    {
        if (lifes > 0)
        {
            lifes -= damageTaken;
        }
        if (lifes <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        WinExit.SetActive(true);
        Destroy(this.gameObject);
        FindObjectOfType<MapManager>().DecreaseEnemyCount();
    }
}
