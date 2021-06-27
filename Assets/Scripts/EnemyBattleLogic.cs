using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleLogic : MonoBehaviour
{
    public float health = 10f, damage = 1f , hit_force = 200f;
    private Animator _animator;
    private Rigidbody2D _rb;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float damage, Vector2 direction, float hit_force)
    {
        _animator.SetTrigger("Hurt");
        _rb.sharedMaterial.friction = 0.4f;
        _rb.AddForce(direction * hit_force, ForceMode2D.Impulse);
        health -= damage;
    }

    public void Death()
    {
        _animator.SetTrigger("Death");
        Destroy(GetComponent<EnemyMovementLogic>(), 0.1f);
        Destroy(GetComponent<Rigidbody2D>(), 0.1f);
        Destroy(GetComponent<BoxCollider2D>(), 0.1f);
    }

    private void Update()
    {
        if(health <= 0)
        {
            Death();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerBattleStats>().TakeDamage(damage, GetComponent<EnemyMovementLogic>().GetVectorDirection(), hit_force, 0.6f);
        }
    }
}
