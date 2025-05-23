using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject ship;
    // Start is called before the first frame update
    void Start()
    {
        ShopMode();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) 
        {
            ShopMode();
        }
        if (Input.GetKeyDown(KeyCode.C)) 
        {
            CombatMode();
        }
    }
    void ShopMode() 
    {
        //Turn Off Controls
        ship.GetComponent<ShipController>().OnShop();
        //Move Camera
        Camera.main.GetComponent<CameraController>().OnShop();
		//Turn On Grid
		ship.GetComponent<ShipManager>().BuildGrid();
		//Open Shop UI
	}
    void CombatMode()
    {
        //Close Shop UI
        //Turn Off Grid
        ship.GetComponent<ShipManager>().DestroyGrid();
		//Turn On Controls
		ship.GetComponent<ShipController>().OffShop();
		//Move Camera
		Camera.main.GetComponent<CameraController>().OffShop();
	}
}
