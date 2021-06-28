using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SoundManager : MonoBehaviour
{
    public AudioClip steps1;
    public AudioClip steps2;
    public AudioClip landing;
    public AudioClip attack1;
    public AudioClip attack2;
    public AudioClip attack3;
    public AudioClip hurt;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Steps1()
    {
        _audioSource.PlayOneShot(steps1);
    }

    void Steps2()
    {
        _audioSource.PlayOneShot(steps2);
    }

    void Landing()
    {
        _audioSource.PlayOneShot(landing);
    }

    void Attack1()
    {
        _audioSource.PlayOneShot(attack1);
    }

    void Attack2()
    {
        _audioSource.PlayOneShot(attack2);
    }

    void Attack3()
    {
        _audioSource.PlayOneShot(attack3);
    }

    void Hurt()
    {
        _audioSource.PlayOneShot(hurt);
    }

}
