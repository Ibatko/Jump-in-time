using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {
    [SerializeField]
    protected Transform bulletPos;
    [SerializeField]
    protected float MovementSpeed;
    protected float MovementSpeedNotAttack;
    protected float MovementSpeedAttack;
    protected bool facingRigth;
    protected bool die;
    protected bool death;
    [SerializeField]
    private GameObject BulletPrefab;
    [SerializeField]
    protected int health;
    [SerializeField]
    private EdgeCollider2D SwordColider;
    [SerializeField]
    private List<string> damageSources;
    public abstract bool IsDead { get; }
    public bool Attack { get; set; }
    public bool TakingDamage { get; set; }
    public Animator MyAnimator { get; private set; }

    // Назначение изначальных переменных
    public virtual void Start () {
        facingRigth = true;
        MyAnimator = GetComponent<Animator>();
        death = false;
    }

    public abstract IEnumerator TakeDamage();

    // поворот персона
    public void ChangeDirection()
    {
        facingRigth = !facingRigth;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }

    // назначение позиции персонажа от стороны поворота
    public virtual void ShootBullet(int value)
    {
        if (facingRigth)
        {
            GameObject tmp = (GameObject)Instantiate(BulletPrefab, bulletPos.position, Quaternion.identity);
            tmp.GetComponent<Bullets>().Initialize(Vector2.right);
            tmp.GetComponent<Bullets>().Initialize2(bulletPos.transform);
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(BulletPrefab, bulletPos.position, Quaternion.Euler(new Vector3(0, 0, 180)));
            tmp.GetComponent<Bullets>().Initialize(Vector2.left);
            tmp.GetComponent<Bullets>().Initialize2(bulletPos.transform);
        }
    }

    // появление колайдера атаки или убрать колайдер атаки
    public void MeleeAtack()
    {
        SwordColider.enabled = !SwordColider.enabled;
    }

    // при вхождение колайдера с определенным тегом вызвать функцию получения урона
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (damageSources.Contains(other.tag))
        {
            Debug.Log(other.tag);
            StartCoroutine(TakeDamage());
        }
    }
}
