using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class TestGun : ShipPart
{
    GameObject target;

    [SerializeField] GameObject projectile;
    protected override void Tick()
    {
        //fire projectile;
        Projectile proj = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
        proj.dir = (target.transform.position - transform.position).normalized;
    }
    protected override void OnCooldownEnd()
    {
        
    }
    public override void Attack(List<GameObject> enemiesInRange)
    {
        if (!Activate()) return;
        SetTarget(enemiesInRange);
        Tick();
    }
    void SetTarget(List<GameObject> enemiesInRange)
    {
        GameObject closest = enemiesInRange[0];
        float dist = (closest.transform.position - transform.position).magnitude;
        for(int i = 1; i < enemiesInRange.Count; i++)
        {
            float newDist = (enemiesInRange[i].transform.position - transform.position).magnitude;
            if(newDist < dist)
            {
                dist = newDist;
                closest = enemiesInRange[i];
            }
        }
        target = closest;
        
    }
}
