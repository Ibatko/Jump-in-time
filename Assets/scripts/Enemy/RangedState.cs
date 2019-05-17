using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedState : IEnemyState
{
    private float attackTimer;
    private float attackCoolDown = 1;
    private bool canAttack = true;
    private Enemy enemy;

    // назначение состояние нынежнего персонажа к скрипту
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    // проверка растояния до цели и поведение при потери цели
    public void Execute()
    {

        if (enemy.InMeleeRange)
        {
            AttackMelee();
            enemy.MyAnimator.SetFloat("speed", 0);
        }
        else if (enemy.Target != null && !enemy.InMeleeRange)
        {
            enemy.Move();
        }
        else
        {
            enemy.ChangeState(new IdleState());
        }
    }

    public void Exit()
    {
    }
    public void OnTriggerEnter(Collider2D other)
    {
    }

    // поведение при нахождение в радиусе атаки
    private void AttackMelee()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackCoolDown)
        {
            canAttack = true;
            attackTimer = 0;
        }
        if (canAttack)
        {
            canAttack = false;
            enemy.MyAnimator.SetTrigger("attack");
        }
    }
}