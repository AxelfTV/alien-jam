using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRange : MonoBehaviour
{
    [SerializeField] ShipController shipController;
    private void Update()
    {
        transform.position = shipController.transform.position + shipController.transform.right - 1.5f*shipController.transform.up;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            shipController.enemiesInRange.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            shipController.enemiesInRange.Remove(collision.gameObject);
        }
    }
}
