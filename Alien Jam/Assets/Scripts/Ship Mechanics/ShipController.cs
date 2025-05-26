using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] float shieldRechargeCooldown;
    [SerializeField] AudioSource hitSound;
    [SerializeField] AudioSource thrustSound;
    [SerializeField] AudioSource purchaseSound;
    public List<GameObject> enemiesInRange;
    public static ShipStats stats;
    bool thrusting;
    bool turning;
    bool attacking;
    bool recharging;
    bool canRecharge;
    int turnDir = 0;
    float thrustDir = 0;

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(8);
        }
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            thrusting = true;
            thrustDir = 1;
        }
        else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            thrusting = true;
            thrustDir = -0.5f;
        }
        else
        {
            
            thrusting = false;
        }

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
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
        if(turning || thrusting)
        {
            if (!thrustSound.isPlaying) thrustSound.Play();
        }
        else if(thrustSound.isPlaying) thrustSound.Pause();
        attacking = (enemiesInRange.Count > 0);
        recharging = (canRecharge && stats.shield < stats.maxShield);
        if (!shop) PartsTick();
    }
	private void FixedUpdate()
	{
        if (shop)
        {
            rb.freezeRotation = true;
            rb.velocity = Vector2.zero;
            return;
        }
        rb.freezeRotation = false;
        if (thrusting) rb.AddForce(transform.up * stats.thrust * thrustDir, ForceMode2D.Force);
        rb.AddForce(-rb.velocity, ForceMode2D.Force);

        if (turning && stats.turnThrust > 0) transform.RotateAround(transform.position, Vector3.forward, turnDir * (stats.turnThrust + 5) * Time.fixedDeltaTime);
        
	}
	void PartsTick()
    {
        foreach (ShipPart part in ShipGrid.instance.parts) 
        {
            part.Power();
            if (thrusting) part.Thrust();
            if (turning) part.Turn();
            if (attacking) part.Attack(enemiesInRange);
            if (recharging) part.ShieldRecharge();
        }
    }
    public void OnShop()
    {
        shop = true;
        stats.power = 0;
        stats.shield = stats.maxShield;
    }
    public void OffShop() 
    {
        shop = false;
        
	}
    public void HealButton()
    {
        int missingHealth = stats.maxHealth - stats.health;
        purchaseSound.time = 0;
        purchaseSound.Play();
        if (missingHealth > stats.money)
        {
            Heal(stats.money);
            stats.money = 0;
            return;
        }
        stats.money -= missingHealth;
        Heal(stats.maxHealth);
    }
    void Heal(int health)
    {
        stats.health += health;
        if(stats.health> stats.maxHealth) stats.health = stats.maxHealth;
    }
    public void TakeDamage(int damage)
    {
        if (shop) return;
        hitSound.time = 0;
        hitSound.Play();
        StopCoroutine("ShieldRechargeCooldown");
        StartCoroutine("ShieldRechargeCooldown");
        if (stats.shield > 0)
        {
            stats.shield -= damage;
            if(stats.shield < 0) stats.shield = 0;
            
        }
        else
        {
            stats.health -= damage;
            if(stats.health < 0)
            {
                stats.health = 0;
                Die();
            }
        }
    }
    void Die()
    {
        GameManager.GameOver();
    }
    IEnumerator ShieldRechargeCooldown()
    {
        canRecharge = false;
        yield return new WaitForSeconds(shieldRechargeCooldown);
        canRecharge = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Border"))
        {
            Vector3 vel = -transform.position.normalized;
            rb.velocity = Vector3.zero;
            rb.AddForce(vel * 10, ForceMode2D.Impulse);
        }
    }
}
