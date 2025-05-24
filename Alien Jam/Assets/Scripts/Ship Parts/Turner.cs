using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turner : ShipPart
{
    [SerializeField] float turnThrust;
    protected override void Tick()
    {
        if (!Activate()) return;

        ShipController.stats.turnThrust += turnThrust;
    }
    protected override void OnCooldownEnd()
    {
        ShipController.stats.turnThrust -= turnThrust;
    }
    public override void Turn()
    {
        Tick();
    }
}
