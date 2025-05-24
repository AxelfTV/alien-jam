using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTile : MonoBehaviour
{
    public Vector2Int gridPosition;
    public ShipPart part = null;
    [SerializeField] Color hoverColor;
    // Start is called before the first frame update
    void Start()
    {
		part = ShipGrid.instance.GetPart(gridPosition);
	}
    public void OnClick() 
    {
        Debug.Log(gridPosition);
    }
    public void OnHover() 
    {
        part = ShipGrid.instance.GetPart(gridPosition);
        if (part != null) part.OnHover();
        gameObject.GetComponent<SpriteRenderer>().color = hoverColor;
    }
    public void OnStopHover()
    {
		part = ShipGrid.instance.GetPart(gridPosition);
		if (part != null) part.OnStopHover();
		gameObject.GetComponent<SpriteRenderer>().color = Color.white;
	}
}
