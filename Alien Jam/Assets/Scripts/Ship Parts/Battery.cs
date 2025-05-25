using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : ShipPart
{
    [SerializeField] int power;
    protected override void OnCooldownEnd()
    {

    }
    protected override void Tick()
    {

    }
    public override void OnAdd()
    {
        ShipController.stats.maxPower += power;
        
    }
    public override void OnRemove()
    {
        ShipController.stats.maxPower -= power;
    }
}
