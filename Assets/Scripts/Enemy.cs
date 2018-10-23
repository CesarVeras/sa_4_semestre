using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Entity
{
    // enums
    public enum Behaviour { FollowPlayer, FleeFromPlayer, FleeAndRandom, Revive }

    // stats
    public float followDistance = 5;
    public Behaviour behaviour;

    // components
    public Rigidbody2D rb;

    // gameobjects
    public GameObject[] dropList;
    public GameObject player;
    public GameObject coin;
    public GameObject newEnemy;

    // extra stuff
    public Image ImageVida;

    private float seed1;
    private float seed2;

    // Use this for initialization
    void Start()
    {
        // set default values
        ResetEnemy();
        if (player == null)
        {
            player = FindObjectOfType<Player>().gameObject;
        }

        seed1 = Random.value * 100;
        seed2 = Random.value * 100;

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var diff = player.transform.position - transform.position;
        switch (behaviour)
        {
            case Behaviour.FollowPlayer:
                rb.velocity = (player.transform.position - transform.position).normalized * speed;
                break;
            case Behaviour.FleeFromPlayer:
                rb.velocity = (player.transform.position - transform.position).normalized * -speed;
                break;
            case Behaviour.FleeAndRandom:
                if (diff.magnitude > followDistance)
                {
                    float x = Mathf.PerlinNoise(Time.time, seed1) * 2 - 1;
                    float y = Mathf.PerlinNoise(Time.time, seed2) * 2 - 1;
                    rb.velocity = new Vector2(x, y).normalized * speed;
                }
                else
                {
                    rb.velocity = (player.transform.position - transform.position).normalized * -speed;
                }
                break;
        }

        ImageVida.fillAmount = (float)lifes / totalLifes;
    }

    public void ResetEnemy()
    {
        totalLifes = 20;
        lifes = totalLifes;
        damage = 3.5f;
        range = 3f;
        fireRate = 0.3f;
        shotSpeed = 5.5f;
        speed = 3f;
    }

    public override void FiringControl()
    {

    }

    public override void MovementControl()
    {

    }

    public override void TakeDamage()
    {
        if (lifes > 0)
        {
            lifes--;
        }
        if (lifes == 0)
        {
            Die();
        }
    }

    public void Die()
    {
        foreach (var drop in dropList)
        {

        }
        Instantiate(coin, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        if (behaviour == Behaviour.Revive)
        {
            var respawnChance = Random.value * 100;
            if (respawnChance <= 20)
            {
                GameObject newEnemy = this.gameObject;
                newEnemy.GetComponent<Enemy>().Revive(newEnemy);
            }
        }
    }

    public void Revive(GameObject enemy)
    {
        newEnemy = Instantiate(newEnemy, transform.position, Quaternion.identity);
        newEnemy.GetComponent<Enemy>().enabled = true;
        newEnemy.GetComponent<BoxCollider2D>().enabled = true;
        newEnemy.GetComponentInChildren<Canvas>().enabled = true;
        newEnemy.GetComponent<Enemy>().ResetEnemy();
        Color verm = new Color32(228, 58, 58, 255);
        GetComponent<SpriteRenderer>().color = verm;
        transform.localScale = new Vector2(transform.localScale.x - transform.localScale.x * .3f, transform.localScale.y - transform.localScale.y * .3f);
        speed *= 4;
        damage *= 1.5f;
        lifes = ((totalLifes / 5) <= 0) ? 1 : totalLifes / 5;
    }
}
