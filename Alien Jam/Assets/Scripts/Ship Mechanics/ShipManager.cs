using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ShipManager : MonoBehaviour
{
    Vector2Int currentMouseTile;
    [SerializeField] GameObject shipTile;
    [SerializeField] AudioSource purchaseSound;
    Dictionary<Vector2Int, ShipTile> tiles;

    bool shop;

    [SerializeField] int shipLevel = 0;
    [SerializeField]GameObject[] shipSprites;
    [SerializeField] GameObject upgradeButton;
    private void Start()
    {
        SetShipSize();
        SetShipGrid();
        SetUpgradeButtonText();
        BuildGrid();
        AddPart(PartName.generator1, new Vector2Int(0, 0));
        AddPart(PartName.thruster1, new Vector2Int(1, 3));
        AddPart(PartName.turner1, new Vector2Int(1, 1));
        AddPart(PartName.gun1, new Vector2Int(2, 0));
        DestroyGrid();
    }
    // Update is called once per frame
    void Update()
    {
        if (!shop) return;
        Vector2Int newMouseTile = FindMouseTile();
        if (newMouseTile != currentMouseTile && !Input.GetMouseButton(0))
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
                tiles[currentMouseTile].OnStopHover();
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
        instPart.GetComponentInChildren<SpriteRenderer>().sortingLayerName = "Ship Parts";
        instPart.transform.parent = gameObject.transform;
		ShipPart sp = instPart.GetComponent<ShipPart>();
        if (ShipGrid.instance.AddToGrid(sp, pos))
        {
            sp.gridPosition = pos;
            sp.OnAdd();
            return true;
        }
        Destroy(instPart);
        return false;
    }
    public bool CheckBuy(ShipPart part)
    {
        if (currentMouseTile.x == -1) return false;
        if (ShipController.stats.money < part.price) return false;
        if (ShipGrid.instance.AddToGrid(part, currentMouseTile)) return true;
        return false;
    }
    public void BuyPart(ShipPart part)
    {
        part.OnHover();
        ShipController.stats.money -= part.price;
        part.gridPosition = currentMouseTile;
        part.gameObject.GetComponentInChildren<SpriteRenderer>().sortingLayerName = "Ship Parts";
        part.transform.position = tiles[currentMouseTile].transform.position;
        part.transform.parent = gameObject.transform;
            
    }
    void DeletePart(Vector2Int pos) 
    {
        ShipPart part = tiles[pos].part;
        if (part == null) return;
        part.OnRemove();
        ShipController.stats.money += part.price / 2;
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
    void SetShipSize()
    {
        for(int i = 0; i < shipSprites.Length; i++)
        {
            if (i == shipLevel) shipSprites[i].SetActive(true);
            else shipSprites[i].SetActive(false);
        }
    }
    void SetShipGrid()
    {
        switch (shipLevel)
        {
            case 0:
                ShipGrid.instance.currentWidth = 3;
                ShipGrid.instance.currentHeight = 4;
                break;
            case 1:
                ShipGrid.instance.currentWidth = 4;
                ShipGrid.instance.currentHeight = 5;
                break;
            case 2:
                ShipGrid.instance.currentWidth = 5;
                ShipGrid.instance.currentHeight = 7;
                break;
            default:
                break;
        }
    }
    public void UpgradeShip()
    {
        if (shipLevel >= shipSprites.Length) return;
        int price;
        if (shipLevel == 0) price = 50;
        else price = 300;
        if (ShipController.stats.money < price) return;
        ShipController.stats.money -= price;
        shipLevel++;
        purchaseSound.time = 0;
        purchaseSound.Play();
        SetShipSize();
        SetShipGrid();
        DestroyGrid();
        BuildGrid();
        if (shipLevel >= 2) upgradeButton.SetActive(false);
        else SetUpgradeButtonText();
    }
    void SetUpgradeButtonText()
    {
        TMP_Text text = upgradeButton.GetComponentInChildren<TMP_Text>();
        switch(shipLevel)
        {
            case 0:
                text.text = "Upgrade Ship - $50";
                break;
            case 1:
                text.text = "Upgrade Ship - $300";
                break;
        }
    }
}
