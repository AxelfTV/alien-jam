using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public CombatManager combatManager;
    int health = 10;
    
    public void TakeDamage(int damage)
    {
        Debug.Log("Taking Damage: " + damage.ToString());
        health -= damage;
        if(health <= 0)
        {
            health = 0;
            Die();
        }
    }
    void Die()
    {
        combatManager.OnEnemyDeath(this);
        Destroy(gameObject);
    }
}
