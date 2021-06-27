using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementLogic : MonoBehaviour
{
    public float WalkSpeed = 10f;
    public float MinWalkSpeed = 5f, MaxWalkSpeed = 15f;
    public float LeftBorderDistance = 15f, RightBorderDistance = 15f;
    private Rigidbody2D _rigidbody2D;
    private DirectionState _directionState;
    private Transform _transform;
    private Collider2D _playerCollider;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        _directionState = DirectionState.Left;
        LeftBorderDistance = _rigidbody2D.position.x - LeftBorderDistance;
        RightBorderDistance = _rigidbody2D.position.x + RightBorderDistance;;
        _rigidbody2D.velocity = Vector2.left * WalkSpeed;
        _rigidbody2D.sharedMaterial.friction = 0.4f;
    }

    void Update()
    {
        
    }

    public Vector2 GetVectorDirection()
    {
        if (_directionState == DirectionState.Left)
        {
            return Vector2.left;
        }
        else
        {
            return Vector2.right;
        }
    }

    private void Follow(Collider2D _followObj)
    {
        
    }

    private void FixedUpdate()
    {
        //отображение спрайта в соответсвующем направлении
        if(_rigidbody2D.velocity.x < 0 && _rigidbody2D.transform.localScale.x < 0)
        {
            _directionState = DirectionState.Left;
            _rigidbody2D.transform.localScale = new Vector3(-_transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
        } else if (_rigidbody2D.velocity.x > 0 && _rigidbody2D.transform.localScale.x > 0)
        {
            _directionState = DirectionState.Right;
            _rigidbody2D.transform.localScale = new Vector3(-_transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
        }
        

        if(_playerCollider == null)
        {
            foreach(Collider2D collider2d in Physics2D.OverlapAreaAll(new Vector2(LeftBorderDistance, _rigidbody2D.position.y + 100), new Vector2(RightBorderDistance, _rigidbody2D.position.y - 100)))
            {
                if(collider2d.tag == "Player")
                {
                    Debug.Log("Player found");
                    _playerCollider = collider2d;
                }
            }
        }
        

        
        if (_rigidbody2D.position.x <= LeftBorderDistance)
        {
            _rigidbody2D.position = new Vector2(LeftBorderDistance, _rigidbody2D.position.y);
            _rigidbody2D.velocity *= -1;
        }
        else if (_rigidbody2D.position.x >= RightBorderDistance)
        {
            _rigidbody2D.position = new Vector2(RightBorderDistance, _rigidbody2D.position.y);
            _rigidbody2D.velocity *= -1;
        }



        if (_playerCollider != null && _playerCollider.transform.position.x >= LeftBorderDistance && _playerCollider.transform.position.x <= RightBorderDistance)
        {
            if (_playerCollider.transform.position.x < _rigidbody2D.position.x)
            {
                //движение
                if (Mathf.Abs(_rigidbody2D.velocity.x) < 1)
                {
                    Debug.Log("left");
                    _rigidbody2D.velocity = Vector2.left * WalkSpeed;
                }
                //
            }
            else
            {
                //движение
                if (Mathf.Abs(_rigidbody2D.velocity.x) < 1)
                {
                    Debug.Log("right");
                    _rigidbody2D.velocity = Vector2.right * WalkSpeed;
                }
                //
            }

        }
        else
        {
            if (Mathf.Abs(_rigidbody2D.velocity.x) < 1)
            {
                _rigidbody2D.velocity = GetVectorDirection() * WalkSpeed;
            }
        }
        
    }
}