using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGenerator : ShipPart
{
	[SerializeField] float power;
	protected override void Tick()
	{
		if (!Activate()) return;

		ShipController.stats.power += power;
	}
	protected override void OnCooldownEnd()
	{}
	public override void Power()
	{
		Tick();
	}
}
