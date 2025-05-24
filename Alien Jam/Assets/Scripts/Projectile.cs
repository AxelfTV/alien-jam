using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 dir;
    [SerializeField] float speed;
    [SerializeField] int damage;
    [SerializeField] bool player;
    private void Start()
    {
        Destroy(gameObject, 3);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        transform.position += dir * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player && collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
