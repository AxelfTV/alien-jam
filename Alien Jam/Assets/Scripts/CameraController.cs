using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject ship;
    [SerializeField] Vector3 shipShopPos;
    float targetZoom;
    Vector3 targetPosition;
    Quaternion targetRotation;

    bool shop;
    // Start is called before the first frame update
    void Start()
    {
        targetZoom = 50;
        targetPosition = new Vector3(0, 0, -10);
        targetRotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        if (shop)
        {
            ship.transform.position += (shipShopPos - ship.transform.position) * Time.deltaTime;
            ship.transform.rotation = Quaternion.RotateTowards(ship.transform.rotation, Quaternion.identity, 90 * Time.deltaTime);
        }
        Camera.main.orthographicSize += 3 * (targetZoom - Camera.main.orthographicSize) * Time.deltaTime; 
        //transform.position += (targetPosition - transform.position) * Time.deltaTime;
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 90 * Time.deltaTime);
    }
    public void OnShop() 
    {
        shop = true;
        targetZoom = 10;
        targetPosition = new Vector3((ship.transform.position.x), (ship.transform.position.y), -10) + ship.transform.right * 12 + ship.transform.up * -3;
        targetRotation = ship.transform.rotation;
    }
    public void OffShop() 
    {
        shop = false;
		targetZoom = 25;
		targetPosition = new Vector3(0, 0, -10);
		targetRotation = Quaternion.identity;
	}
}
