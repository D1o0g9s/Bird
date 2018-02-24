﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTime : MonoBehaviour {
    public const float maxZVelocity = 500.0f;
    public const float ZVelocityIncrement = 2.50f;

    [SerializeField] private GameObject leftBox;
    [SerializeField] private GameObject rightBox;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject camera;
    
    public long leftCollisionTime;
    public long rightCollisionTime;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == leftBox) {
            leftCollisionTime = DateTime.Now.Ticks;
            Debug.Log(leftCollisionTime);
        } else if (other.gameObject == rightBox) {
            rightCollisionTime = DateTime.Now.Ticks;
            Debug.Log(rightCollisionTime);
        }
        

        if (player.GetComponent<Rigidbody>().velocity.z < maxZVelocity) {
            player.GetComponent<Rigidbody>().velocity += camera.transform.forward * ZVelocityIncrement;
            Debug.Log(player.transform.forward);
            //player.GetComponent<Rigidbody>().AddForce(0.0f, 0.0f, ZVelocityIncrement, ForceMode.VelocityChange);
        }
    }
}
