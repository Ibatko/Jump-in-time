using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
    private Enemy enemy;
    private float patrolTimer;
    private float patrolDuration = 10;

    // назначение состояние нынежнего персонажа к скрипту
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    // выполнение патрулирования и проверка на нахождение цели
    public void Execute()
    {
        Patrol();
        enemy.Move();

        if (enemy.Target != null && enemy.InMeleeRange)
        {
            enemy.ChangeState(new RangedState());
        }
    }

    public void Exit()
    {
    }
    public void OnTriggerEnter(Collider2D other)
    {
    }

    // выполнение патрулирование
    private void Patrol()
    {
        patrolTimer += Time.deltaTime;
        if (patrolTimer >= patrolDuration)
        {
            enemy.ChangeState(new IdleState());
        }
    }
}