using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Entity
{
    // enums
    public enum Behaviour { FollowPlayer, FleeFromPlayer, FleeAndRandom }

    // stats
    public float followDistance = 5;
    public Behaviour behaviour;

    // components
    public Rigidbody2D rb;

    // gameobjects
    public GameObject[] dropList;
    public GameObject player;

    // extra stuff
    public Image ImageVida;

    private float seed1;
    private float seed2;

    // Use this for initialization
    void Start()
    {
        // set default values
        totalLifes = 6;
        lifes = totalLifes;
        damage = 3.5f;
        range = 3f;
        fireRate = 0.3f;
        shotSpeed = 5.5f;
        speed = 3f;

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
            Morrer();
        }
    }

    public void Morrer()
    {
        foreach (var drop in dropList)
        {
           
        }
        Destroy(this.gameObject);
    }
}
