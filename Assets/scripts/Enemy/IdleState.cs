using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{
    private Enemy enemy;
    private float idleTimer;
    private float idleDuration = 5;

    // назначение состояние нынежнего персонажа к скрипту
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    // выполнение нейтрального поведения
    public void Execute()
    {
        Idle();
        if (enemy.Target != null)
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    public void Exit()
    {
        
    }
    public void OnTriggerEnter(Collider2D other)
    {
        
    }

    // поведение персонажа при нахождении на месте
    private void Idle()
    {
        enemy.MyAnimator.SetFloat("speed", 0);
        idleTimer += Time.deltaTime;
        if (idleTimer >= idleDuration)
        {
            enemy.ChangeState(new PatrolState()); 
        }
    }
}
