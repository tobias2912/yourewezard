using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {

    }




    void FixedUpdate()
    {

    }

    internal void handleProjectileCollision(projectile projectile, Collision collision)
    {
        animator.SetTrigger("shortSpell");
        projectile proj = collision.gameObject.GetComponent<projectile>();
        proj.setTargetDirection(transform.rotation.eulerAngles);
    }
}
