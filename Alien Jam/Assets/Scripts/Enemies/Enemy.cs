using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public CombatManager combatManager;
    [SerializeField] int health = 10;
    [SerializeField] public int cost = 1;
    [SerializeField] int money = 0;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            health = 0;
            Die();
        }
    }
    void Die()
    {
        ShipController.stats.money += money;
        combatManager.OnEnemyDeath(this);
        Destroy(gameObject);
    }
}
