using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : Enemy
{
    [SerializeField] int damage;
    [SerializeField] float speed;
    Rigidbody2D rb;
    GameObject player;
    int dir = 1;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        speed *= 1 + CombatManager.wave / 20;
        health += CombatManager.wave - 1;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        transform.position += new Vector3(0, Mathf.Sin(Time.time * 20), 0) * Time.fixedDeltaTime * 2;
        rb.velocity = dir * speed * (player.transform.position - transform.position).normalized * 3;


        if(dir*rb.velocity.x < 0) spriteRenderer.flipX = true;
        else spriteRenderer.flipX = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent.GetComponent<ShipController>().TakeDamage(damage);
            StopCoroutine("Retreat");
            StartCoroutine("Retreat");
        }
    }
    IEnumerator Retreat()
    {
        dir = -1;
        yield return new WaitForSeconds(0.7f);
        dir = 1;
    }
}
