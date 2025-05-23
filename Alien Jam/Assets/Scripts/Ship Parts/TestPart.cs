using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TestPart : ShipPart
{
	[SerializeField] float thrust;
	protected override void Tick()
	{
		if (!Activate()) return;

		ShipController.stats.thrust += thrust;
	}
	protected override void OnCooldownEnd()
	{
		ShipController.stats.thrust -= thrust;
	}
	public override void Thrust()
	{
		Tick();
	}
}
