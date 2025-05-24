using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTile : MonoBehaviour
{
    public Vector2Int gridPosition;
    public ShipPart part = null;
    [SerializeField] Color hoverColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnHover()
    {
        part = Shop.grid[gridPosition.x,gridPosition.y];
        if (part != null) part.OnHover();
        gameObject.GetComponent<SpriteRenderer>().color = hoverColor;
    }
    public void OnStopHover()
    {
        part = Shop.grid[gridPosition.x, gridPosition.y];
        if (part != null) part.OnStopHover();
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
