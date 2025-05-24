using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public static ShipStats stats;
    bool thrusting;
    bool turning;
    int turnDir = 0;

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

        thrusting = Input.GetKey(KeyCode.W);
        if(Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            turning = true;
            turnDir = 1;
        }
        else if(Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            turning = true;
            turnDir = -1;
        }
        else
        {
            turning = false;
        }
        Debug.Log(stats.power);
    }
	private void FixedUpdate()
	{
        if (shop)
        {
            rb.velocity = Vector2.zero;
            return;
        }
     
        if (thrusting) rb.AddForce(transform.up * stats.thrust, ForceMode2D.Force);
        rb.AddForce(-rb.velocity, ForceMode2D.Force);

        if (turning) transform.RotateAround(transform.position, Vector3.forward, turnDir * stats.turnThrust * Time.fixedDeltaTime);
        
	}
	void PartsTick()
    {
        foreach (ShipPart part in ShipGrid.instance.parts) 
        {
            part.Power();
            if (thrusting) part.Thrust();
            if (turning) part.Turn();
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
