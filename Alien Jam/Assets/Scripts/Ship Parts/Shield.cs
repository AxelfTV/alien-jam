using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : ShipPart
{
    [SerializeField] int shield;
    protected override void OnCooldownEnd()
    {

    }
    protected override void Tick()
    {

    }
    public override void OnAdd()
    {
        ShipController.stats.maxShield += shield;
        ShipController.stats.shield += shield;
    }
    public override void OnRemove()
    {
        ShipController.stats.shield -= shield;
        ShipController.stats.maxShield -= shield;

    }
}
