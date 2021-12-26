using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class projectile : MonoBehaviour
{
    public Rigidbody rb;
    private float destructionTime;
    private float speed;
    private Vector3 dir;
    private Vector3 startpoint;
    [SerializeField]
    private float armingDistance;
    public AudioClip impactSound;
    float elapsed = 0f;
    [SerializeField]
    float directionChangeTimer;
    private Vector3 targetDir;

    void Start()
    {
        destructionTime = 5.0f;
        Destroy(gameObject, destructionTime);
        startpoint = transform.position;
    }
    public void setup(float speed, Vector3 dir)
    {
        this.speed = speed;
        this.dir = dir;
        this.targetDir = dir;
        rb.velocity = dir * speed;
    }

    public void setTargetDirection(Vector3 newDir)
    {
        targetDir = newDir;
    }

    void Update()
    {
        checkDirectionChange();
    }

    private void checkDirectionChange()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= directionChangeTimer)
        {
            elapsed = elapsed % directionChangeTimer;
            directionChange();
        }
    }
    private void directionChange()
    {
        this.dir = Vector3.Lerp(this.dir, targetDir, 0.5f);
        rb.velocity = this.dir * speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (Vector3.Distance(startpoint, transform.position) > armingDistance)
        {
            print(collision.gameObject.isStatic);
            if (collision.gameObject.isStatic)
            {
                explode();
            }
            else if (collision.gameObject.tag == "enemy")
            {
                enemyController enemyController = collision.gameObject.GetComponent<enemyController>();
                enemyController.handleProjectileCollision(this, collision);
            }
        }
    }

    public void explode()
    {
        GetComponent<AudioSource>().PlayOneShot(impactSound);
        print("explode");
        rb.velocity = Vector3.zero;
    }
}
