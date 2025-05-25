using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armour : ShipPart
{
    [SerializeField] int health;
    protected override void OnCooldownEnd()
    {
        
    }
    protected override void Tick()
    {
        
    }
    public override void OnAdd()
    {
        ShipController.stats.maxHealth += health;
        ShipController.stats.health += health;
    }
    public override void OnRemove()
    {
        ShipController.stats.maxHealth -= health;
        if(ShipController.stats.health > ShipController.stats.maxHealth)
        {
            ShipController.stats.health = ShipController.stats.maxHealth;
        }
    }
}
