using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCharger : ShipPart
{
    [SerializeField] int charge;
    protected override void Tick()
    {
        if (!Activate()) return;

        ShipController.stats.shield += charge;
        if(ShipController.stats.shield > ShipController.stats.maxShield) ShipController.stats.shield = ShipController.stats.maxShield;
    }
    protected override void OnCooldownEnd()
    {
        
    }
    public override void ShieldRecharge()
    {
        Tick();
    }
}
