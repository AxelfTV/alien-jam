using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipPart : MonoBehaviour
{
    public int width = 1;
    public int height = 1;

	public Vector2Int gridPosition;

	float timer = 0;
	bool active;
	[SerializeField] protected float cooldown;
	[SerializeField] float cost;
	private void Start()
	{
		timer = cooldown;
	}
	private void Update()
	{
		timer += Time.deltaTime;
		if (timer >= cooldown && active) 
		{
			active = false;
			OnCooldownEnd();
		}
		
	}
	public virtual void Power() { }
	public virtual void Attack() { }
	public virtual void Thrust() { }
	public virtual void Turn() { }
	public virtual void ShieldRecharge() { }
	protected abstract void Tick();
	protected abstract void OnCooldownEnd();
	public void OnHover()
	{
		
	}
	public void OnStopHover()
	{
		
	}
	protected bool Activate() 
	{
		if (timer >= cooldown && ShipController.stats.power >= cost)
		{
			timer = 0;
			active = true;
			ShipController.stats.power -= cost;
			return true;
		}
		return false;
	}
	public static GameObject GetPart(PartName name)
	{
		switch (name)
		{
			case PartName.test:
				return (GameObject)Resources.Load("Parts/Test Part");
			case PartName.testGen:
				return (GameObject)Resources.Load("Parts/Test Generator");
			case PartName.turner:
                return (GameObject)Resources.Load("Parts/Turner");
            default:
				return null;
		}

	}
}
