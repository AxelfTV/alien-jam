using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public static ShipStats stats;
    bool thrusting;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = new ShipStats();
    }

    // Update is called once per frame
    void Update()
    {
        PartsTick();
        Debug.Log(stats.power.ToString() + " " +  stats.thrust.ToString());

        thrusting = Input.GetKey(KeyCode.Space);
    }
	private void FixedUpdate()
	{
        if (thrusting) rb.AddForce(transform.up * stats.thrust, ForceMode2D.Force);
        else rb.AddForce(-rb.velocity, ForceMode2D.Force);
	}
	void PartsTick()
    {
        foreach (ShipPart part in ShipGrid.instance.parts) 
        {
            part.Power();
            if (thrusting) part.Thrust();
        }
    }
}
