using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public List<GameObject> enemiesInRange;
    public static ShipStats stats;
    bool thrusting;
    bool turning;
    bool attacking;
    int turnDir = 0;

    Rigidbody2D rb;

    bool shop;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = new ShipStats();
        enemiesInRange = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        

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
        attacking = (enemiesInRange.Count > 0);
        if (!shop) PartsTick();
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
            if (attacking) part.Attack(enemiesInRange);
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemiesInRange.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(collision.gameObject);
        }
    }
}
