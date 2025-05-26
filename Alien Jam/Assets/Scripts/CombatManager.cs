using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField] GameObject bigBoss;
    List<Enemy> enemies;
    [SerializeField] List<GameObject> enemyPrefabs;
    int points;
    public static int wave = 0;
    public static int fWave;
    [SerializeField] int finalWave = 20;
    [SerializeField] AudioSource bugDie;
    // Start is called before the first frame update
    void Awake()
    {
        wave = 0;
        fWave = finalWave;
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
        bugDie.time = 0;
        bugDie.Play();
        enemies.Remove(enemy);
        if(enemies.Count <= 0)
        {
            EndWave();
        }
    }
    void SpawnWave()
    {
        wave++;
        
        points = 2*wave;
        enemies = new List<Enemy>();
        if (wave >= finalWave)
        {
            for(int i = 0; i< (wave - finalWave +1); i++)
            {
                SpawnBigBoss();
            }
            
            return;
        }
        while (points > 0)
        {
            Enemy enemy = SpawnEnemy().GetComponent<Enemy>();
            enemy.combatManager = this;
            enemies.Add(enemy);
        }
    }
    GameObject SpawnEnemy()
    {
        List<GameObject> randomisedList = enemyPrefabs.OrderBy(x => Random.value).ToList();
        float angle = Random.Range(0, Mathf.PI * 2);
        Vector3 spawnDir = new Vector3(1.6f*Mathf.Sin(angle), Mathf.Cos(angle));
        float radius = Random.Range(30, 40);
        GameObject enemy = null;
        for(int i = 0; i< randomisedList.Count; i++)
        {
            Enemy e = randomisedList[i].GetComponent<Enemy>();
            if(e.cost <= points)
            {
                enemy = randomisedList[i];
                points -= e.cost;
                break;
            }
        }
        return Instantiate(enemy, spawnDir*radius, Quaternion.identity);
    }
    void SpawnBigBoss()
    {
        float angle = Random.Range(0, Mathf.PI * 2);
        Vector3 spawnDir = new Vector3(1.6f * Mathf.Sin(angle), Mathf.Cos(angle));
        float radius = Random.Range(30, 40);
        
        Enemy enemy = Instantiate(bigBoss, spawnDir * radius, Quaternion.identity).GetComponent<Enemy>();
        enemy.combatManager = this;
        enemies.Add(enemy);
    }
    void EndWave()
    {
        GetComponent<GameManager>().CombatOver();
    }
}
