using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Exception = System.Exception;

public class Player : Entity
{

    // stats
    protected int money;
    protected int bombs;
    public float luck;
    public bool colidingWithEnemy;

    // components
    private Rigidbody2D rb;

    // extra things
    public float iframeTimeHelper;
    public float iframe = .5f; // invencibility frame
    public float timeHelper;

    // gameobject
    public Transform ammoDispenser;
    public GameObject Ammo;
    public Image lifeImage;
    public Text coinText;

    private GameObject newAmmo;
    private AmmoControl scriptAmmo;


    // Use this for initialization
    void Start()
    {
        // set default values
        totalLifes = 6;
        money = 0;
        bombs = 1;
        lifes = totalLifes;
        damage = 3.5f;
        range = 3f;
        fireRate = 0.5f;
        shotSpeed = 5.5f;
        speed = 5f;
        luck = 0;
        colidingWithEnemy = false;
        CheckEverything();

        rb = GetComponent<Rigidbody2D>();
        ammoDispenser = ammoDispenser.transform;
        firingState = Firing.NoFiring;
        timeHelper = Time.time;
        iframeTimeHelper = Time.time;

        lifeImage.fillAmount = (float)lifes / totalLifes;
        coinText.text = money.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // moviment control
        MovementControl();

        // firing control
        FiringControl();

        // enemy coliding management
        if (colidingWithEnemy)
        {
            TakeDamage();
        }
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

    public override void FiringControl()
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

    public override void MovementControl()
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

    public override void TakeDamage()
    {
        if (Time.time - iframeTimeHelper >= iframe)
        {
            if (lifes > 0)
            {
                lifes--;
                iframeTimeHelper = Time.time;
                lifeImage.fillAmount = (float)lifes / totalLifes;
            }
            else if (lifes <= 0)
            {
                Application.Quit();
                UnityEditor.EditorApplication.isPlaying = false;    
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            colidingWithEnemy = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            colidingWithEnemy = false;
        }
    }

    public void CheckEverything()
    {
        /*
            public Transform ammoDispenser;
            public GameObject Ammo;
            public Image lifeImage;
            public Text coinText;
        */
        if (ammoDispenser == null)
        {
            throw new Exception("É preciso informar um local para instanciar as balas (ammoDispenser)");
        }
        if (Ammo == null)
        {
            throw new Exception("É preciso informar um prefab de bala para instanciar as balas (Ammo)");
        }
        if (lifeImage == null)
        {
            throw new Exception("É preciso informar um imagem para mostrar a vida (lifeImage)");
        }
        if (coinText == null)
        {
            throw new Exception("É preciso informar um texto para mostrar as moedas (coinText)");
        }
    }
    public void AddMoney(int amountMoney)
    {
        SetMoney(amountMoney + money);
    }

    public void SetMoney(int amountMoney)
    {
        money = amountMoney;
        coinText.text = money.ToString();
    }

    public int GetMoney()
    {
        return money;
    }

    public void SetBombs(int amountBombs)
    {
        bombs = amountBombs;
    }

    public int GetBombs()
    {
        return bombs;
    }
}
