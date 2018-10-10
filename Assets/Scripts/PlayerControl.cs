using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Exception = System.Exception;

public class PlayerControl : MonoBehaviour
{
    // enums
    public enum Firing { NoFiring, FiringLeft, FiringRight, FiringUp, FiringDown }

    // stats
    public int totalLifes = 6;
    public int money = 0;
    public int bombs = 1;
    public int lifes;

    public float damage = 3.5f;
    public float range = 3f;
    public float fireRate = 0.05f;
    public float shotSpeed = 5.5f;
    public float speed = 5f;
    public float luck = 0f;

    public Firing firingState;

    // components
    private Rigidbody2D rb;

    // extra things
    public float timeHelper;

    public Transform ammoDispenser;
    public GameObject Ammo;
    private GameObject newAmmo;
    private AmmoControl scriptAmmo;

    public Image lifeImage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ammoDispenser = ammoDispenser.transform;
        firingState = Firing.NoFiring;
        timeHelper = Time.time;
        lifes = totalLifes;
        if (lifeImage == null)
        {
            throw new Exception("É preciso definir uma imagem para a barra de vida");
        }
    }

    // Update is called once per frame
    void Update()
    {

        // moviment control
        MovimentControl();

        // firing control
        FiringControl();
    }

    private void CreateAmmo()
    {
        if (Time.time - timeHelper >= fireRate)
        {
            newAmmo = Instantiate(Ammo, ammoDispenser.position, Quaternion.identity);
            scriptAmmo = newAmmo.GetComponent<AmmoControl>();
            scriptAmmo.firingRange = range;
            scriptAmmo.firingSpeed = shotSpeed;
            scriptAmmo.initialPosition = ammoDispenser.position;
            switch (firingState)
            {
                case Firing.FiringLeft:
                    scriptAmmo.firingDirection = AmmoControl.Direction.GoingLeft;
                    break;
                case Firing.FiringRight:
                    scriptAmmo.firingDirection = AmmoControl.Direction.GoingRight;
                    break;
                case Firing.FiringUp:
                    scriptAmmo.firingDirection = AmmoControl.Direction.GoingUp;
                    break;
                case Firing.FiringDown:
                    scriptAmmo.firingDirection = AmmoControl.Direction.GoingDown;
                    break;
            }
            timeHelper = Time.time;
        }
    }

    private void MovimentControl()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector2(rb.velocity.x, -speed);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void FiringControl()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            firingState = Firing.FiringLeft;
            CreateAmmo();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            firingState = Firing.FiringRight;
            CreateAmmo();
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            firingState = Firing.FiringUp;
            CreateAmmo();
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            firingState = Firing.FiringDown;
            CreateAmmo();
        }
    }

    public void TakeDamage()
    {
        if (lifes > 0)
        {
            lifeImage.fillAmount = (float)--lifes / totalLifes;
        }
    }

    // Collision control
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }
}
