using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour {

    [SerializeField]
    private Enemy enemy;

    // приписывание цели если в области игрок (поиск цели)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            enemy.Target = other.gameObject;
        }
    }

    // потеря цели
    private void OnTriggerExit2D(Collider2D other)
    {
        enemy.Target = null;
    }
}
