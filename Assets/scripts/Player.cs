using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, IPlayer
{
    private static Player instance;

    // присвоение к переменной состаяние персонажа
    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }
            return instance;
        }
    }

    private bool sideLeft;
    private bool sideRight;
    [SerializeField]
    private Transform[] groundPoints;
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private bool airControl;
    [SerializeField]
    private float jumpForce;
    private bool run;
    public Rigidbody2D MyRigidbody { get; set; }
    public bool Slide { get; set; }
    public bool Jump { get; set; }
    public bool Die { get; set; }
    public bool OnGround { get; set; }
    private float dieTimer;
    private float dieCoolDown = 1;
    private bool dieMenu = true;

    // проверка на смерть персонажа
    public override bool IsDead
    {
        get
        {
            return health <= 0;
        }
    }

    // приписование физического состояние объекта к переменной и запуск скрипта Character
    public override void Start () {
        MyRigidbody = GetComponent<Rigidbody2D>();
        base.Start();
        health = 50;
        GameManager.Instance.HelthBar = health;
    }

    // Выполнение функции нажатия кнопок (находится ниже
    private void Update()
    {
        HandleInput();
    }

    // приписование к переменной состояние нажатия кнопки (право, лево)
    void FixedUpdate () {
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal != 0)
        {
            run = true;
        }
        OnGround = IsGrounded();
        HandleMovement(horizontal);
        if (!IsDead)
        {
            Flip(horizontal);
        }
        else
        {
            TimeDead();
        }
        HandleLayers();
    }

    // Движение персонажа
    private void HandleMovement(float Horizontal)
    {
       if (MyRigidbody.velocity.y < 0)
        {
            MyAnimator.SetBool("land", true);
        }
       if (!Slide && (OnGround || airControl) && !IsDead)
        {
            MyRigidbody.velocity = new Vector2(Horizontal * MovementSpeed, MyRigidbody.velocity.y);

        }
       if (Jump && MyRigidbody.velocity.y == 0 && !IsDead)
        {
            MyRigidbody.AddForce(new Vector2(0, jumpForce));
        }
        if ((sideLeft == true && Horizontal < 0) || (sideRight == true && Horizontal > 0))
        {
            MyAnimator.SetFloat("speed", 0);
        }
        else
        {
             MyAnimator.SetFloat("speed", Mathf.Abs(Horizontal));
        }
    }

    // проверка на нажатие клавиш и запуск анимации
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MyAnimator.SetTrigger("jump");
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            MyAnimator.SetTrigger("slide");
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            MyAnimator.SetTrigger("attack");
        }
        if (Input.GetKeyDown(KeyCode.Keypad5) && !Die)
        {
            MyAnimator.SetTrigger("shoot");
        }
    }

    // поворот персонажа в зависимости от переменной (нажатие клавиш: право или лево)
    private void Flip(float horizontal)
    {
        if ((horizontal > 0 && !facingRigth || horizontal < 0 && facingRigth) && !Die)
        {
            ChangeDirection();
        }
    }

    // проверка нахождения на земле или в воздхе
    private bool IsGrounded()
    {
        if (MyRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in  groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    // назначение в аниматоре от состояние объекта падения на земле
    private void HandleLayers()
    {
        if (!OnGround)
        {
            MyAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            MyAnimator.SetLayerWeight(1, 0);
        }
    }

    // проверка на нахождение объекта в каком состояние и кол-во ввыстрелов и выполнить функцию выстрела пули
    public override void ShootBullet(int value)
    {
        if (!OnGround && value == 2 || OnGround && run && value == 1 || OnGround && !run && value == 1)
        {
            base.ShootBullet(value);
        }
    }

    public override IEnumerator TakeDamage()
    {
        yield return null;
    }

    // поведение персонажа с встречей тем или иным объетком
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "end")
        {
            Application.LoadLevel("completed");
        }
        if (other.tag == "EnemySword")
        {
            health -= 10;
            GameManager.Instance.HelthBar = health;
            if (IsDead)
            {
                MyAnimator.SetTrigger("die");
            }
        }
        if (other.tag == "left")
        {
            sideLeft = true;
        }
        if (other.tag == "right")
        {
            sideRight = true;
        }
        if (other.tag == "DieDamage")
        {
            health -= health;
            GameManager.Instance.HelthBar = health;
            MyAnimator.SetTrigger("die");
        }
    }

    private void TimeDead()
    {
        dieTimer += Time.deltaTime;
        if (dieTimer >= dieCoolDown)
        {
            Application.LoadLevel("dieMenu");
        }
    }

    // поведение персонажа при выходе из столкновения со стороной
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "left")
        {
            sideLeft = false;
        }
        if (other.tag == "right")
        {
            sideRight = false;
        }
    }

    // столкновение с твердым объектом и поведение (колекционирование)
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Coin")
        {
            GameManager.Instance.CollectedCoin++;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Crystal")
        {
            GameManager.Instance.CollectedCrystal++;
            Destroy(other.gameObject);
        }
    }
}
