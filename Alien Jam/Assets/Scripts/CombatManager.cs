using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    List<Enemy> enemies;
    [SerializeField] List<GameObject> enemyPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnShop()
    {
        
    }
    public void OffShop()
    {
        SpawnWave();
    }
    public void OnEnemyDeath(Enemy enemy)
    {
        enemies.Remove(enemy);
        if(enemies.Count <= 0)
        {
            EndWave();
        }
    }
    void SpawnWave()
    {
        enemies = new List<Enemy>();

        Enemy enemy = SpawnEnemy().GetComponent<Enemy>();
        enemy.combatManager = this;
        enemies.Add(enemy);
    }
    GameObject SpawnEnemy()
    {
        int i = Random.Range(0, enemies.Count);
        return Instantiate(enemyPrefabs[i], new Vector3(20,20,0), Quaternion.identity);
    }
    void EndWave()
    {
        GetComponent<GameManager>().CombatOver();
    }
}
