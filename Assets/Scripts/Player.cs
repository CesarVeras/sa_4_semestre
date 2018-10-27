using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Exception = System.Exception;

public class Player : Entity
{

    // stats
    [HideInInspector] public int money;
    [HideInInspector] public int bombs;
    [HideInInspector] public float luck;
    private bool colidingWithEnemy;
    private float iframe = .5f; // invencibility frame

    // components
    private Rigidbody2D rb;

    // helpers
    private float timeHelper; // keeps track of the time between shots.
    private float iframeTimeHelper; // keeps track of the time between iframes. 
    private SceneControl sceneControl;

    // gameobject
    public Transform ammoDispenser;
    public GameObject AmmoPrefab;
    public Image lifeImage;
    public Text coinText;

    private GameObject newAmmo;
    private Ammo scriptAmmo;


    // Use this for initialization
    void Start()
    {
        // set default values
        totalLifes = 12;
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

        sceneControl = FindObjectOfType<SceneControl>();

        rb = GetComponent<Rigidbody2D>();
        ammoDispenser = ammoDispenser.transform;
        firingState = Firing.NoFiring;
        timeHelper = Time.time;
        iframeTimeHelper = Time.time;

        lifeImage.fillAmount = lifes / totalLifes;
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
            newAmmo = Instantiate(AmmoPrefab, ammoDispenser.position, Quaternion.identity);
            scriptAmmo = newAmmo.GetComponent<Ammo>();
            scriptAmmo.damage = this.damage;
            scriptAmmo.firingRange = range;
            scriptAmmo.firingSpeed = shotSpeed;
            scriptAmmo.initialPosition = ammoDispenser.position;
            switch (firingState)
            {
                case Firing.FiringLeft:
                    scriptAmmo.firingDirection = Ammo.Direction.GoingLeft;
                    break;
                case Firing.FiringRight:
                    scriptAmmo.firingDirection = Ammo.Direction.GoingRight;
                    break;
                case Firing.FiringUp:
                    scriptAmmo.firingDirection = Ammo.Direction.GoingUp;
                    break;
                case Firing.FiringDown:
                    scriptAmmo.firingDirection = Ammo.Direction.GoingDown;
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
                lifes -= damageTaken;
                iframeTimeHelper = Time.time;
                lifeImage.fillAmount = lifes / totalLifes;
            }
            if (lifes <= 0)
            {
                sceneControl.OpenLoseScreen();
                // Application.Quit();
                // UnityEditor.EditorApplication.isPlaying = false;
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            colidingWithEnemy = true;
            damageTaken = (int)collision.gameObject.GetComponent<Enemy>().damage;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            colidingWithEnemy = false;
            damageTaken = 0;
        }
    }

    public void AddMoney(int amountMoney)
    {
        money += amountMoney;
        coinText.text = money.ToString();
    }

    public void AddBombs(int amountBombs)
    {
        bombs += amountBombs;
    }

    public void CheckEverything()
    {
        /*
            public Transform ammoDispenser;
            public GameObject AmmoPrefab;
            public Image lifeImage;
            public Text coinText;
        */
        if (ammoDispenser == null)
        {
            UnityEditor.EditorApplication.isPlaying = false;
            throw new Exception("É preciso informar um local para instanciar as balas (ammoDispenser)");
        }
        if (AmmoPrefab == null)
        {
            UnityEditor.EditorApplication.isPlaying = false;
            throw new Exception("É preciso informar um prefab de bala para instanciar as balas (Ammo)");
        }
        if (lifeImage == null)
        {
            UnityEditor.EditorApplication.isPlaying = false;
            throw new Exception("É preciso informar um imagem para mostrar a vida (lifeImage)");
        }
        if (coinText == null)
        {
            UnityEditor.EditorApplication.isPlaying = false;
            throw new Exception("É preciso informar um texto para mostrar as moedas (coinText)");
        }
    }
}
