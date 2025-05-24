using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject shopTile;
    [SerializeField] ShipManager ship;
    Dictionary<Vector2Int, ShopTile> tiles;
    public static ShipPart[,] grid;
    List<ShipPart> parts;
    int shopWidth = 6;
    int shopHeight = 12;
    bool shop;
    Vector2Int currentMouseTile;

    public static GameObject holding = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

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
        if (Input.GetMouseButtonDown(0))
        {
            if (currentMouseTile.x != -1 && grid[currentMouseTile.x,currentMouseTile.y] != null)
            {
                holding = grid[currentMouseTile.x, currentMouseTile.y].gameObject;

            }
        }
        if (!Input.GetMouseButton(0) && holding != null)
        {
            if (!ship.BuyPart(holding.GetComponent<ShipPart>()))
            {
                Vector2Int gp = holding.GetComponent<ShipPart>().gridPosition;
                holding.transform.position = tiles[gp].transform.position;
            }
            else
            {
                RemovePart(holding.GetComponent<ShipPart>());
            }
            holding = null;
        }
        if(holding != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            holding.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        }
    }
    Vector2Int FindMouseTile()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, 10, LayerMask.GetMask("Shop Tile"));
        if (hit)
        {
            return hit.collider.gameObject.GetComponent<ShopTile>().gridPosition;
        }
        return new Vector2Int(-1, -1);
    }
    void BuildGrid() 
    {
        tiles = new Dictionary<Vector2Int, ShopTile>();
        grid = new ShipPart[shopWidth, shopHeight];
        parts = new List<ShipPart>();
        for (int i = 0; i < shopWidth; i++)
        {
            for (int j = 0; j < shopHeight; j++)
            {
                ShopTile tile = Instantiate(shopTile, transform.position + transform.right * (i + 5) - transform.up * (j - 5), transform.rotation).GetComponent<ShopTile>();
                Vector2Int gridPos = new Vector2Int(i, j);
                tile.gameObject.transform.parent = gameObject.transform;
                tile.gridPosition = gridPos;
                tiles.Add(gridPos, tile);
            }
        }
    }
    void DestroyGrid()
    {
        foreach (ShopTile tile in tiles.Values)
        {
            Destroy(tile.gameObject);
        }
        foreach(ShipPart part in parts)
        {
            Destroy(part.gameObject);
        }
    }
    public void OnShop() 
    {
        shop = true;
        BuildGrid();
        AddPart(PartName.testGen, new Vector2Int(2, 2));
        AddPart(PartName.test, new Vector2Int(2, 5));
        AddPart(PartName.turner, new Vector2Int(2, 8));


    }
    public void OffShop()
    {
        shop = false;
        DestroyGrid();
    }
    void AddPart(PartName name, Vector2Int pos)
    {
        GameObject part = ShipPart.GetPart(name);
        GameObject instPart = Instantiate(part, tiles[pos].transform);
        instPart.transform.parent = gameObject.transform;
        ShipPart sp = instPart.GetComponent<ShipPart>();
        parts.Add(sp);
        sp.gridPosition = pos;
        for (int i = 0; i < sp.width; i++)
        {
            for (int j = 0; j < sp.height; j++)
            {
                grid[pos.x + i, pos.y + j] = sp;
            }
        }
    }
    void RemovePart(ShipPart part)
    {
        parts.Remove(part);
        Vector2Int pos = part.gridPosition;
        for (int i = 0; i < part.width; i++)
        {
            for (int j = 0; j < part.height; j++)
            {
                grid[pos.x + i, pos.y + j] = null;
            }
        }
    }
}
