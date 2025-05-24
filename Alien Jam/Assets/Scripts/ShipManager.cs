using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.PlayerSettings;

public class ShipManager : MonoBehaviour
{
    Vector2Int currentMouseTile;
    [SerializeField] GameObject shipTile;
    Dictionary<Vector2Int, ShipTile> tiles;

    bool shop;

    // Update is called once per frame
    void Update()
    {
        if (!shop) return;
        Vector2Int newMouseTile = FindMouseTile();
        if (newMouseTile != currentMouseTile)
        {
            //On Not Hover
            if (currentMouseTile.x != -1) tiles[currentMouseTile].OnStopHover();
            currentMouseTile = newMouseTile;
			//On Hover
			if (currentMouseTile.x != -1) tiles[currentMouseTile].OnHover();
		}
		if (Input.GetMouseButtonDown(1))
		{
            if (currentMouseTile.x != -1) 
            { 
                DeletePart(currentMouseTile);
				tiles[currentMouseTile].OnHover();
			}
		}
	}
    void BuildGrid() 
    {
        tiles = new Dictionary<Vector2Int, ShipTile>();
		for (int i = 0; i < ShipGrid.instance.currentWidth; i++)
		{
			for (int j = 0; j < ShipGrid.instance.currentHeight; j++)
			{
				ShipTile tile = Instantiate(shipTile, transform.position + Vector3.zero + transform.right * i - transform.up * j, transform.rotation).GetComponent<ShipTile>();
                Vector2Int gridPos = new Vector2Int(i, j);
				tile.gameObject.transform.parent = gameObject.transform;
                tile.gridPosition = gridPos;
                tiles.Add(gridPos, tile);
			}
		}
	}
    void DestroyGrid()
    {
        foreach (ShipTile tile in tiles.Values)
        {
            Destroy(tile.gameObject);
        }
    }
    public void OnShop() 
    {
        shop = true;
        BuildGrid();
    }
    public void OffShop() 
    {
        shop = false;
        DestroyGrid();
    }
    Vector2Int FindMouseTile() 
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, 10, LayerMask.GetMask("Ship Tile"));
        if (hit) 
        {
            return hit.collider.gameObject.GetComponent<ShipTile>().gridPosition;
        }
        return new Vector2Int(-1,-1);
    }
    bool AddPart(PartName name, Vector2Int pos)
    {
        GameObject part = ShipPart.GetPart(name);
		GameObject instPart = Instantiate(part, tiles[pos].transform);
        instPart.transform.parent = gameObject.transform;
		ShipPart sp = instPart.GetComponent<ShipPart>();
        if (ShipGrid.instance.AddToGrid(sp, pos))
        {

            sp.OnHover();
            sp.gridPosition = pos;
            return true;
        }
        Destroy(instPart);
        return false;
    }
    public bool BuyPart(ShipPart part)
    {
        if (currentMouseTile.x == -1) return false;
        if (ShipGrid.instance.AddToGrid(part, currentMouseTile))
        {
            part.OnHover();
            part.gridPosition = currentMouseTile;
            part.transform.position = tiles[currentMouseTile].transform.position;
            part.transform.parent = gameObject.transform;
            return true;
        }
        return false;
    }
    void DeletePart(Vector2Int pos) 
    {
        ShipPart part = tiles[pos].part;
        if (part == null) return;
        for (int i = 0; i < part.width; i++) 
        {
            for (int j = 0; j < part.height; j++)
            {
                tiles[part.gridPosition + new Vector2Int(i, j)].part = null;
            }
        }
        ShipGrid.instance.RemoveFromGrid(part);
        Destroy(part.gameObject);
    }
}
