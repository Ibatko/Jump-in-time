using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullets : MonoBehaviour {
    [SerializeField]
    private float speed = 0.25f;
    private Rigidbody2D myRigbody;
    private Vector2 direction;
    private Transform Ppos;

	// Назначение состояние объекта пули к переменной, назначение местоположения от переменной "Ppos" - место появления пули
	void Start () {
        myRigbody = GetComponent<Rigidbody2D>();
        myRigbody.velocity = new Vector2(Ppos.transform.position.x, Ppos.transform.position.y);
    }

    // Изменение позиции пули (движение пули)
    void FixedUpdate()
    {
        myRigbody.velocity = myRigbody.velocity + new Vector2(direction.x * speed, 0);
        transform.position = myRigbody.velocity;
    }

    // инцилизац стороны выстрела
    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }

    // инцилизация позиции выстрела
    public void Initialize2(Transform pPos)
    {
        this.Ppos = pPos;
    }

    // уничтожение объекта при выходе из области вида
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
