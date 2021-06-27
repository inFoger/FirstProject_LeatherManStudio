using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Speeds")]
    public float WalkSpeed = 3f;
    public float JumpForce = 10f;

    private MoveState _moveState = MoveState.Idle;
    private DirectionState _directionState = DirectionState.Right;
    private Transform _transform;
    private Rigidbody2D _rigidbody2D;
    private Animator _animatorController;
    private float _walkTime = 0, _walkCooldown = 0.1f;
    private Collider2D _collider2DFirst;
    private Collider2D _collider2DSecond;
    private PlayerAttackCheck _playerAttackCheck;
    private PlayerBattleStats _playerBattleStats;

    void Start()
    {
        _transform = GetComponent<Transform>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animatorController = GetComponent<Animator>();
        _directionState = base.transform.localScale.x > 0 ? DirectionState.Right : DirectionState.Left;
        _collider2DFirst = (Collider2D)GetComponents<Collider2D>().GetValue(0);
        _collider2DSecond = (Collider2D)GetComponents<Collider2D>().GetValue(1);
        _playerAttackCheck = GetComponent<PlayerAttackCheck>();
        _playerBattleStats = GetComponent<PlayerBattleStats>();
    }

    
    void Update()
    {
        if (!_playerBattleStats.isDead)
        {
            if (Input.GetKey(KeyCode.D))
            {
                MoveRight();
            }
            if (Input.GetKey(KeyCode.A))
            {
                MoveLeft();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (_playerAttackCheck.isAttack1)
                {
                    Attack2();
                }
                else if (_playerAttackCheck.isAttack2)
                {
                    Attack3();
                }
                else if (!_playerAttackCheck.isAttack3 && !_playerAttackCheck.isAttack2)
                {
                    Attack1();
                }

            }
        }
    }

    private void FixedUpdate()
    {
        //Вся работа с физикой прописывается здесь. Время между этими обновлениями фиксировано
        if (_moveState == MoveState.Jump)
        {
            if (_rigidbody2D.velocity == Vector2.zero)
            {
                Idle();
            }
        }
        else if (_moveState == MoveState.Run && _playerBattleStats.thrownAwayTime <= 0)
        {
            _collider2DFirst.enabled = false;
            _collider2DSecond.enabled = true;
            //Разобраться с физикой ненужного "планирования"
            _rigidbody2D.velocity = ((_directionState == DirectionState.Right ? Vector2.right : -Vector2.right) * WalkSpeed);
            //_rigidbody2D.MovePosition(_rigidbody2D.position + (_directionState == DirectionState.Right ? Vector2.right : -Vector2.right) * WalkSpeed);
            //_rigidbody2D.AddForce((_directionState == DirectionState.Right ? Vector2.right : -Vector2.right) * WalkSpeed);
            _walkTime -= Time.deltaTime;
            if (_walkTime <= 0)
            {
                Idle();
            }
        } else if (_moveState != MoveState.Run)
        {
            _collider2DFirst.enabled = true;
            _collider2DSecond.enabled = false;
        }
    }

    public void MoveRight()
    {
        if(_moveState != MoveState.Jump)
        {
            _moveState = MoveState.Run;
            if(_directionState == DirectionState.Left)
            {
                _transform.localScale = new Vector3(-_transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
                _directionState = DirectionState.Right;
            }
            _walkTime = _walkCooldown;
            _animatorController.SetBool("Run", true);
        }
    }

    public void MoveLeft()
    {
        if(_moveState != MoveState.Jump)
        {
            _moveState = MoveState.Run;
            if(_directionState == DirectionState.Right)
            {
                _transform.localScale = new Vector3(-_transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
                _directionState = DirectionState.Left;
            }
            _walkTime = _walkCooldown;
            _animatorController.SetBool("Run", true);
        }
    }

    public void Jump()
    {
        if(_moveState != MoveState.Jump)
        {
            //_rigidbody2D.velocity = (Vector3.up * JumpForce * Time.deltaTime);
            _rigidbody2D.AddForce(Vector2.up * JumpForce);
            _moveState = MoveState.Jump;
            _animatorController.SetTrigger("Jump");
            _animatorController.ResetTrigger("Landing");
        }
    }

    public void Idle()
    {
        _moveState = MoveState.Idle;
        _animatorController.SetBool("Run", false);
    }

    public void Attack1()
    {
        _animatorController.SetBool("Attack_1", true);
    }

    public void Attack2()
    {
        _animatorController.SetBool("Attack_2", true);
    }

    public void Attack3() {
        _animatorController.SetBool("Attack_3", true);
    }

    public void Death()
    {
        _animatorController.SetTrigger("Death");

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Enemy"))
        {
            _animatorController.SetTrigger("Landing");
        }
    }

    
    public Vector2 GetVectorDirection()
    {
        if(_directionState == DirectionState.Left)
        {
            return Vector2.left;
        } else
        {
            return Vector2.right;
        }
    }
    
}

enum DirectionState
{
    Right,
    Left
}

enum MoveState
{
    Idle,
    Run,
    Jump
}