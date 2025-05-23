using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public static ShipStats stats;
    bool thrusting;

    Rigidbody2D rb;

    bool shop;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = new ShipStats();
    }

    // Update is called once per frame
    void Update()
    {
        if(!shop)PartsTick();

        thrusting = Input.GetKey(KeyCode.Space);
    }
	private void FixedUpdate()
	{
        if (!shop)
        {
            if (thrusting) rb.AddForce(transform.up * stats.thrust, ForceMode2D.Force);
            else rb.AddForce(-rb.velocity, ForceMode2D.Force);
        }
        else 
        {
            rb.velocity = Vector2.zero;
        }
        
	}
	void PartsTick()
    {
        foreach (ShipPart part in ShipGrid.instance.parts) 
        {
            part.Power();
            if (thrusting) part.Thrust();
        }
    }
    public void OnShop()
    {
        shop = true;
    }
    public void OffShop() 
    {
        shop = false;
        stats.power = 0;
	}
}
