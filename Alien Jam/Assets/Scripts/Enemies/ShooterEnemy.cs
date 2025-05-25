using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShooterEnemy : Enemy
{
    [SerializeField] float speed;
    [SerializeField] float shootRange;
    [SerializeField] float shootCooldown;
    [SerializeField] float retreatRange;
    [SerializeField] GameObject projectile;
    bool canShoot = true;
    Rigidbody2D rb;
    GameObject player;
    int dir = 1;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        Vector3 distDiff = player.transform.position - transform.position;
        rb.velocity = dir * speed * (distDiff).normalized * 3;
        if (distDiff.magnitude < retreatRange) dir = -1;
        else if (distDiff.magnitude < shootRange - 4) dir = 0;
        else dir = 1;

        if(distDiff.magnitude < shootRange && canShoot)
        {
            Shoot(distDiff);
        }
        if (rb.velocity.x < 0) spriteRenderer.flipX = true;
        else spriteRenderer.flipX = false;
    }
    void Shoot(Vector3 dir)
    {
        Projectile proj = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
        proj.dir = dir.normalized;
        StartCoroutine(ShootCooldown());
    }
    IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }
}
