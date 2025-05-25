using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGenerator : ShipPart
{
	[SerializeField] float power;
	protected override void Tick()
	{
		if (ShipController.stats.power >= ShipController.stats.maxPower) return;
        if (!Activate()) return;

		ShipController.stats.power += power;
		if(ShipController.stats.power > ShipController.stats.maxPower) ShipController.stats.power = ShipController.stats.maxPower;
	}
	protected override void OnCooldownEnd()
	{}
	public override void Power()
	{
		Tick();
	}
}
