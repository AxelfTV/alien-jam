using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject ship;
    [SerializeField] GameObject shop;
    CombatManager combatManager;

    bool shopBool;
    // Start is called before the first frame update
    void Start()
    {
        combatManager = GetComponent<CombatManager>();
        ShopMode();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) 
        {
            ShopOver();
        }
    }
    public void CombatOver()
    {
        ShopMode();
    }
    public void ShopOver()
    {
        CombatMode();
    }
    void ShopMode() 
    {
        if (shopBool) return;
        //Turn Off Controls
        ship.GetComponent<ShipController>().OnShop();
        //Move Camera
        Camera.main.GetComponent<CameraController>().OnShop();
		//Turn On Grid
		ship.GetComponent<ShipManager>().OnShop();
        //Open Shop UI
        shop.GetComponent<Shop>().OnShop();

        combatManager.OnShop();

        shopBool = true;
    }
    void CombatMode()
    {
        if (!shopBool) return;
        //Close Shop UI
        shop.GetComponent<Shop>().OffShop();
        //Turn Off Grid
        ship.GetComponent<ShipManager>().OffShop();
		//Turn On Controls
		ship.GetComponent<ShipController>().OffShop();
		//Move Camera
		Camera.main.GetComponent<CameraController>().OffShop();
        //Start Wave
        combatManager.OffShop();

        shopBool = false;
	}
}
