using System.Collections;
using UnityEngine;

public interface IPlayer
{
    bool Die { get; set; }
    bool Jump { get; set; }
    Rigidbody2D MyRigidbody { get; set; }
    bool OnGround { get; set; }
    bool Slide { get; set; }
    void ShootBullet(int value);
    void Start();
    IEnumerator TakeDamage();
}