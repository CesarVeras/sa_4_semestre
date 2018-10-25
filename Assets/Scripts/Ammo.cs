using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{

    // enums
    public enum Direction { GoingLeft, GoingRight, StandingStill, GoingUp, GoingDown };

    // stats
    public float damage;
    public Direction firingDirection = Direction.StandingStill;
    public float firingSpeed;
    public float firingRange;
    public Vector2 initialPosition;
    public int yInitial;

    // components
    private Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (firingDirection == Direction.GoingLeft)
        {
            rb.velocity = new Vector2(-firingSpeed, 0);
            if (initialPosition.x - firingRange >= transform.position.x)
            {
                Destroy(this.gameObject);
            }
        }
        else if (firingDirection == Direction.GoingRight)
        {
            rb.velocity = new Vector2(firingSpeed, 0);
            if (initialPosition.x + firingRange <= transform.position.x)
            {
                Destroy(this.gameObject);
            }
        }
        else if (firingDirection == Direction.GoingUp)
        {
            rb.velocity = new Vector2(0, firingSpeed);
            if (initialPosition.y + firingRange <= transform.position.y)
            {
                Destroy(this.gameObject);
            }
        }
        else if (firingDirection == Direction.GoingDown)
        {
            rb.velocity = new Vector2(0, -firingSpeed);
            if (initialPosition.y - firingRange >= transform.position.y)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.damageTaken = damage;
            enemy.TakeDamage();
        }
        Destroy(this.gameObject);
    }
}