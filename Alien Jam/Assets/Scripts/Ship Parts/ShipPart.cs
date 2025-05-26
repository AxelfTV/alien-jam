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

	[SerializeField] string partName;
	[SerializeField] string partDesc;
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
			StartCoroutine(SpriteInactive());
		}
		
	}
	IEnumerator SpriteInactive()
	{
		yield return null;
		if(!active) spriteRenderer.sprite = inactiveSprite;
    }
	public virtual void Power() { }
	public virtual void Attack(List<GameObject> enemiesInRange) { }
	public virtual void Thrust() { }
	public virtual void Turn() { }
	public virtual void ShieldRecharge() { }
	public virtual void OnAdd() { }
	public virtual void OnRemove() { }
	protected abstract void Tick();
	protected abstract void OnCooldownEnd();
	public void OnHover()
	{
		GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>().SetInfoPanel(partName, partDesc,price,cost,cooldown);
	}
	public void OnStopHover()
	{
		GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>().CloseInfoPanel();

    }
	protected bool Activate() 
	{
		if (timer < cooldown) return false;

		if (ShipController.stats.power >= cost)
		{
			timer = 0;
			active = true;
			spriteRenderer.sprite = activeSprite;
			ShipController.stats.power -= cost;
			return true;
		}
		GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>().CantUse();
		return false;
	}
	public static GameObject GetPart(PartName name)
	{
		switch (name)
		{
			case PartName.thruster1:
				return (GameObject)Resources.Load("Parts/thruster1");
            case PartName.thruster2:
                return (GameObject)Resources.Load("Parts/thruster2");
            case PartName.thruster3:
                return (GameObject)Resources.Load("Parts/thruster3");
            case PartName.generator1:
				return (GameObject)Resources.Load("Parts/generator1");
            case PartName.generator2:
                return (GameObject)Resources.Load("Parts/generator2");
            case PartName.generator3:
                return (GameObject)Resources.Load("Parts/generator3");
            case PartName.turner1:
                return (GameObject)Resources.Load("Parts/turner1");
            case PartName.turner2:
                return (GameObject)Resources.Load("Parts/turner2");
            case PartName.turner3:
                return (GameObject)Resources.Load("Parts/turner3");
            case PartName.gun1:
				return (GameObject)Resources.Load("Parts/gun1");
            case PartName.gun2:
                return (GameObject)Resources.Load("Parts/gun2");
            case PartName.gun3:
                return (GameObject)Resources.Load("Parts/gun3");
            case PartName.armour1:
                return (GameObject)Resources.Load("Parts/armour1");
            case PartName.shield1:
                return (GameObject)Resources.Load("Parts/shield1");
            case PartName.charger1:
                return (GameObject)Resources.Load("Parts/charger1");
            case PartName.battery1:
                return (GameObject)Resources.Load("Parts/battery1");
            default:
				return null;
		}

	}
}
