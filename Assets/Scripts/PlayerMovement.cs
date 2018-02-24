﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public const float maxXVelocity = 10.0f;
    public const float XVelocityScalingFactor = 0.2f; // scales deltaY of controller from ground / lower collider

    private long lLowerTick = 0;
    private long lUpperTick = 0;

    private long rLowerTick = 0;
    private long rUpperTick = 0;

    [SerializeField] private GameObject lowerCollider;
    [SerializeField] private GameObject upperCollider;
    [SerializeField] private GameObject leftControllerCollider;
    [SerializeField] private GameObject rightControllerCollider;

    TriggerTime lowerColliderScript;
    TriggerTime upperColliderScript;

	// Use this for initialization
	void Start () {
        lowerColliderScript = lowerCollider.GetComponent<TriggerTime>();
        upperColliderScript = upperCollider.GetComponent<TriggerTime>();
        //gameObject.GetComponent<Rigidbody>().AddForce(0.0f, 0.0f, 10.0f, ForceMode.VelocityChange);
    }
	
	// Update is called once per frame
	void Update () {
        lUpperTick = upperColliderScript.leftCollisionTime;

        if (lLowerTick != lowerColliderScript.leftCollisionTime) {
            lLowerTick = lowerColliderScript.leftCollisionTime;

            Debug.Log("left " + 0.3f / (float) (lLowerTick - lUpperTick) * (float) TimeSpan.TicksPerSecond);

            gameObject.GetComponent<Rigidbody>().AddForce(0.0f, Mathf.Max(0.0f, Mathf.Min(0.3f / (float) (lLowerTick - lUpperTick) * (float)TimeSpan.TicksPerSecond, 10.0f)), 0.0f, ForceMode.VelocityChange);
        }

        rUpperTick = upperColliderScript.rightCollisionTime;

        if (rLowerTick != lowerColliderScript.rightCollisionTime) {
            rLowerTick = lowerColliderScript.rightCollisionTime;

            Debug.Log("right " + 0.3f / (float) (rLowerTick - rUpperTick) * (float) TimeSpan.TicksPerSecond);

            gameObject.GetComponent<Rigidbody>().AddForce(0.0f, Mathf.Max(0.0f, Mathf.Min(0.3f / (float) (rLowerTick - rUpperTick) * (float) TimeSpan.TicksPerSecond, 10.0f)), 0.0f, ForceMode.VelocityChange);
        }

        
    }
}
