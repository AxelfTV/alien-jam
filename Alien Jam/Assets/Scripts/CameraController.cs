using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject ship;

    float targetZoom;
    Vector3 targetPosition;
    Quaternion targetRotation;
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
        Camera.main.orthographicSize += (targetZoom - Camera.main.orthographicSize) * Time.deltaTime; 
        transform.position += (targetPosition - transform.position) * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 90 * Time.deltaTime);
    }
    public void OnShop() 
    {
        targetZoom = 10;
        targetPosition = new Vector3(ship.transform.position.x, ship.transform.position.y, -10);
        targetRotation = ship.transform.rotation;
    }
    public void OffShop() 
    {
		targetZoom = 50;
		targetPosition = new Vector3(0, 0, -10);
		targetRotation = Quaternion.identity;
	}
}
