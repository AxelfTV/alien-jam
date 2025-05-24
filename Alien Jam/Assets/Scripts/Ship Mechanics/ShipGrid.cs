using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGrid : MonoBehaviour
{
    public static ShipGrid instance;
	public int currentWidth = 3;
	public int currentHeight = 4;
    const int maxWidth = 30;
    const int maxHeight = 30;
    ShipPart[,] grid;
	public List<ShipPart> parts;
	private void Awake()
	{
        instance = this;
		grid = new ShipPart[maxWidth, maxHeight];
		parts = new List<ShipPart>();
	}
	// Start is called before the first frame update
	void Start()
    {
		
	}

	// Update is called once per frame
	void Update()
	{
        if (Input.GetKeyDown(KeyCode.T)) 
        {
			for (int i = 0; i < maxWidth; i++)
			{
				for (int j = 0; j < maxHeight; j++)
				{
					if(grid[i,j] != null) Debug.Log(grid[i, j]);
				}
			}
		}
    }
    public bool AddToGrid(ShipPart part, Vector2Int pos) 
    {
		parts.Add(part);
        List<Vector2Int> gridSpaces = new List<Vector2Int>();
        for (int i = 0; i < part.width; i++)
        {
            for (int j = 0; j < part.height; j++) 
            {
                Vector2Int gridSpace = pos + Vector2Int.right * i + Vector2Int.up * j;

                if (!CheckGridSpace(gridSpace)) return false;

                gridSpaces.Add(gridSpace);
            }
        }
        foreach (Vector2Int gs in gridSpaces) {
            grid[gs.x, gs.y] = part;
        }
		return true;
    }
	public void RemoveFromGrid(ShipPart part)
	{
		parts.Remove(part);
		for (int i = 0; i < part.width; i++)
		{
			for (int j = 0; j < part.height; j++)
			{
				Vector2Int gridSpace = part.gridPosition + Vector2Int.right * i + Vector2Int.up * j;
                if (gridSpace.x >= maxWidth || gridSpace.y >= maxHeight || gridSpace.x < 0 || gridSpace.y < 0) continue;
				grid[gridSpace.x, gridSpace.y] = null;
			}
		}
	}
	bool CheckGridSpace(Vector2Int pos) 
    {
        if (pos.x >= currentWidth || pos.y >= currentHeight || pos.x < 0 || pos.y < 0) return false;
        return (grid[pos.x,pos.y]==null);
    }
	public ShipPart GetPart(Vector2Int pos) 
	{
		if (!(pos.x >= maxWidth || pos.y >= maxHeight || pos.x < 0 || pos.y < 0)) return grid[pos.x, pos.y];
		return null;
	}
}
