using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Entity
{
    public GameObject player;

    public Rigidbody2D rb;

    public Text textoVida;
    public Image ImageVida;

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

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = (player.transform.position - transform.position).normalized * speed;
        textoVida.text = lifes.ToString();
        ImageVida.fillAmount = lifes / totalLifes;
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
    }
}
