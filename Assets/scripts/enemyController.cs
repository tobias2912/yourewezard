using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    CapsuleCollider collidercomp;
    GameObject impactBodypart;

    public void ragdoll(){
        collidercomp.enabled = false;
        animator.enabled = false;
    }
    private void knockback(Vector3 source){
        ragdoll();
        Rigidbody part = impactBodypart.GetComponent<Rigidbody>();
        Vector3 dir = transform.position - source;
        dir.Normalize();
        part.AddForce(dir*6f, ForceMode.Impulse);
    }


    internal void handleProjectileCollision(projectile projectile, Collision collision)
    {
        print("ai handles projectile");
        knockback(projectile.transform.position);
        // animator.SetTrigger("shortSpell");
        projectile proj = collision.gameObject.GetComponent<projectile>();
        proj.explode();
    }
}
