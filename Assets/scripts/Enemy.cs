using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

    int valueded;
    float xDir;
    private IEnemyState currentState;
    public GameObject Target { get; set; }
    [SerializeField]
    private float meleeRange;
    [SerializeField]
    private Transform leftEdge;
    [SerializeField]
    private Transform rightEdge;
   
    // определения растояние между персонажем для атаки 
    public bool InMeleeRange
    {
        get
        {
            if (Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= meleeRange;
            }
            return false;
        }
    }

    // проверка здоровья персонажа (смерть)
    public override bool IsDead
    {
        get
        {
            return health <= 0;
        }
    }

    // Загрузка скрипта Character, определение скорости героя, запуск нового  нейтрального поведения 
    public override void Start () {
        base.Start();
        MovementSpeedNotAttack = MovementSpeed;
        MovementSpeedAttack = MovementSpeed * 2;
        ChangeState(new IdleState()); 
	}
	
	// проверка на смерть, если что проверить на получение урона и искать героя
	void Update () {
        if (!IsDead)
        {
            if (!TakingDamage)
            {
                currentState.Execute();
            }
            LookAtTarget();
        }
	}

    // поиск цели, поведение при нахождение цели и потери цели
    private void LookAtTarget()
    {
        if (Target != null)
        {
            xDir = Target.transform.position.x - transform.position.x;
            MovementSpeed = MovementSpeedAttack;
        }
        else
        {
            MovementSpeed = MovementSpeedNotAttack;
        }
        if (Target != null && (xDir < 0 && facingRigth || xDir > 0 && !facingRigth))
        {
            ChangeDirection();
        }
    }

    // Выход из нынешнего поведения, и запуск нового поведния
    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter(this); 
    }

    // Движение персонажа и запуск функции поворота персонажа
    public void Move()
    {
        if (!Attack)
        {
            if ((GetDirection().x > 0 && transform.position.x < rightEdge.position.x) || (GetDirection().x < 0 && transform.position.x > leftEdge.position.x))
            {
                MyAnimator.SetFloat("speed", 1);

                transform.Translate(GetDirection() * (MovementSpeed * Time.deltaTime));
            }
            else if (currentState is PatrolState)
            {
                ChangeDirection();
            }
        }
    }

    // проверка в какую сторону повернут персонаж
    public Vector2 GetDirection()
    {
        return facingRigth ? Vector2.right : Vector2.left;
    }

    // при нахождение объекта проверка на нужны и выполнить поведение с ним
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        currentState.OnTriggerEnter(other);
    }

    // получение урона, убийство героя при выполнении условия и выподение монеты и персонажа
    public override IEnumerator TakeDamage()
    {      
        if (Target != null)
        {
            health -= 10;
        }
        else
        {
            health -= 20;
            ChangeDirection();
        }

        if (IsDead && valueded == 0)
        {
            valueded++;
            GameObject coin = (GameObject)Instantiate(GameManager.Instance.CoinPrefab, new Vector3(transform.position.x, transform.position.y + 2), Quaternion.identity);
            Physics2D.IgnoreCollision(coin.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            MyAnimator.SetTrigger("die");
            yield return null;
        }
    }
}
