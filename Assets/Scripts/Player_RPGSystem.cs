using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_RPGSystem : MonoBehaviour
{
    private int _level;
    private float _levelBorder;
    private float _expAmount;
    private float _damageMultiplier;
    private float _defenceMultiplier;
    private float _healthMultiplier;
    private float _criticalHitChance;
    private PlayerBattleStats _playerStats;
     

    void Start()
    {
        _level = 1;
        _levelBorder = 20;
        _expAmount = 0;
        _damageMultiplier = 1;
        _defenceMultiplier = 0;
        _healthMultiplier = 1;
        _criticalHitChance = 0.01f;
        _playerStats = GetComponent<PlayerBattleStats>();
    }

    void Update()
    {
        if(_expAmount >= _levelBorder)
        {
            LevelUp();
        }
    }

    public void AddExp(float exp)
    {
        _expAmount += exp;
    }

    void LevelUp()
    {
        _level += 1;
        _expAmount -= _levelBorder;
        _levelBorder += 5;
        _damageMultiplier += 0.25f;
        _defenceMultiplier += 0.02f;
        _healthMultiplier += 0.15f;
        _criticalHitChance += 0.02f;
        _playerStats.StatsRedisribute(1.15f, 1.15f);
        Debug.Log("Level Up");
    }

    public float GetDamageMultiplier()
    {
        return _damageMultiplier;
    }

    public float GetDefenceMultiplier()
    {
        return _defenceMultiplier;
    }

    public float GetLevel()
    {
        return _level;
    }

    public float GetExpAmount()
    {
        return _expAmount;
    }

    public float GetLevelBorder()
    {
        return _levelBorder;
    }

    public float GetHealthMultiplier()
    {
        return _healthMultiplier;
    }

    public float GetCriticalHitChance()
    {
        return _criticalHitChance;
    }
}
