using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleStats : MonoBehaviour
{
    [Header("Player Stats")]
    public float Health = 5f, Damage = 1f, HitForce = 50f;
    public Transform attackPoint;
    public float attackSizeX = 2, attackSizeY = 1;
    private Animator _animator;
    private Rigidbody2D _rb;
    private PlayerMovement _playerMovement;
    public bool isDead = false;
    public float thrownAwayTime = 0f;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Health <= 0 && !isDead)
        {
            isDead = true;
            _playerMovement.Death();
        }
        if(thrownAwayTime > 0)
        {
            thrownAwayTime -= Time.deltaTime;
        }
    }

    private void MeleeAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapBoxAll(attackPoint.position, new Vector2(attackSizeX, attackSizeY), 0f); //потом перенести создание вектора в Start
        foreach (Collider2D enemy in enemies)
        {
            if (enemy.tag == "Enemy")
            {
                enemy.GetComponent<EnemyBattleLogic>().TakeDamage(Damage, _playerMovement.GetVectorDirection(), HitForce);
            }
        }
    }

    public void TakeDamage(float damage, Vector2 direction, float hit_force, float thrownAwayTime)
    {
        _animator.SetTrigger("Hurt");
        _rb.AddForce(direction * hit_force, ForceMode2D.Impulse);
        this.thrownAwayTime = thrownAwayTime;
        Health -= damage;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPoint.position , new Vector3(attackSizeX, attackSizeY, 1));
    }

}
