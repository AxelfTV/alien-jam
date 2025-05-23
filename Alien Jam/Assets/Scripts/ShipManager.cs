using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ShipManager : MonoBehaviour
{
    Vector2Int currentMouseTile;
    [SerializeField] GameObject shipTile;
    Dictionary<Vector2Int, ShipTile> tiles;

    // Start is called before the first frame update
    void Start()
    {
        BuildGrid();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2Int newMouseTile = FindMouseTile();
        if (newMouseTile != currentMouseTile)
        {
            //On Not Hover
            if (currentMouseTile.x != -1) tiles[currentMouseTile].OnStopHover();
            currentMouseTile = newMouseTile;
			//On Hover
			if (currentMouseTile.x != -1) tiles[currentMouseTile].OnHover();
		}
		
        if (Input.GetMouseButtonDown(0)) 
        {
            if (currentMouseTile.x != -1) 
            {
                AddPart(PartName.test, currentMouseTile);
				tiles[currentMouseTile].OnHover();

			}
		}
        if (Input.GetKeyDown(KeyCode.G)) 
        {
			if (currentMouseTile.x != -1)
			{
				AddPart(PartName.testGen, currentMouseTile);
				tiles[currentMouseTile].OnHover();

			}
		}
		if (Input.GetMouseButtonDown(1))
		{
            if (currentMouseTile.x != -1) 
            { 
                DeletePart(currentMouseTile);
				tiles[currentMouseTile].OnHover();
			}
		}

        if (Input.GetKeyDown(KeyCode.D))
        {
            DestroyGrid();
        }
        if (Input.GetKeyDown(KeyCode.B)) 
        {
            BuildGrid();
        }
	}
    void BuildGrid() 
    {
        tiles = new Dictionary<Vector2Int, ShipTile>();
		for (int i = 0; i < ShipGrid.instance.currentWidth; i++)
		{
			for (int j = 0; j < ShipGrid.instance.currentHeight; j++)
			{
				ShipTile tile = Instantiate(shipTile, transform.position + Vector3.zero + Vector3.right * i + Vector3.down * j, Quaternion.identity).GetComponent<ShipTile>();
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
