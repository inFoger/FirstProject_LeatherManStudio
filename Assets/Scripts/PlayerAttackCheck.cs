using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCheck : MonoBehaviour
{
    public bool isAttack1 = false, isAttack2 = false, isAttack3 = false;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void StartAttack1()
    {
        isAttack1 = true;
    }

    private void EndAttack1()
    {
        isAttack1 = false;
        _animator.SetBool("Attack_1", false);
    }

    private void StartAttack2()
    {
        isAttack2 = true;
    }

    private void EndAttack2()
    {
        isAttack2 = false;
        _animator.SetBool("Attack_2", false);
    }

    private void StartAttck3()
    {
        isAttack3 = true;
    }

    private void EndAttack3()
    {
        isAttack3 = false;
        _animator.SetBool("Attack_3", false);
    }
    
}
