using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreColision : MonoBehaviour {
    [SerializeField]
    private Collider2D other;

    // Игнорирование физического тела объекта к персонажу
    private void Awake()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other, true);
    }
}
