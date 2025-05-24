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
	[SerializeField] public int price;

	[SerializeField] Sprite activeSprite;
	[SerializeField] Sprite inactiveSprite;
	SpriteRenderer spriteRenderer;
	private void Start()
	{
		timer = cooldown;
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
	}
	private void Update()
	{
		timer += Time.deltaTime;
		if (timer >= cooldown && active) 
		{
			active = false;
			OnCooldownEnd();
			spriteRenderer.sprite = inactiveSprite;
		}
		
	}
	public virtual void Power() { }
	public virtual void Attack(List<GameObject> enemiesInRange) { }
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
			spriteRenderer.sprite = activeSprite;
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
			case PartName.testGun:
				return (GameObject)Resources.Load("Parts/Test Gun");
            default:
				return null;
		}

	}
}
