using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTime : MonoBehaviour {

    [SerializeField] private GameObject leftBox;
    [SerializeField] private GameObject rightBox;

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
    }
}
