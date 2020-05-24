﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** This class takes care of the AI for Enemy Skeleton.
 * @author ShifatKhan
 */
[RequireComponent(typeof(Controller2D))]
public class EnemySkeleton : EnemyGrounded
{
    [SerializeField]
    private float chaseDistance = 6; // Distance where enemy will chase target.
    [SerializeField]
    private float attackDistance = 0.8f; // Distance where enemy will attack target.

    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 homePosition; // Place to return to if player is gone.

    private bool followTarget;

    public override void Start()
    {
        base.Start();

        // Set default target to be the Player.
        if (target == null)
        {
            target = GameObject.FindWithTag("Player").transform;
        }

        homePosition = gameObject.transform.position;
    }

    public override void FixedUpdate()
    {
        CheckDistance();
        base.FixedUpdate();
        
    }

    /** Check if target is in radius. If so, enemy follows target until it is in attack range.
     * If player is out of sight, enemy returns to initial position.
     */
    void CheckDistance()
    {
        // TODO: Change distance calculation to only check for X (not position)
        // If target is inside chase radius and outside attack radius.
        if (Vector2.Distance(target.position, transform.position) <= chaseDistance
            && Vector2.Distance(target.position, transform.position) > attackDistance)
        {
            directionalInput = (target.position - transform.position).normalized;
        }
        else if ((Vector2.Distance(homePosition, transform.position) <= chaseDistance && Vector2.Distance(homePosition, transform.position) > attackDistance) 
            && Vector2.Distance(target.position, transform.position) >= chaseDistance)
        {
            // Target is gone, so return to home position.
            directionalInput = (homePosition - transform.position).normalized;
        }
        else
        {
            directionalInput.x = 0;
        }
    }

    public void Jump()
    {
        if (controller.collisions.below)
        {
            velocity.y = maxJumpVelocity;
        }
    }

    public IEnumerator AttackCo()
    {
        animator.SetBool("attacking1", true);
        yield return null; // Wait 1 frame
        animator.SetBool("attacking1", false);
        //yield return null; // Wait 1 frame

    }
}
