using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTime : MonoBehaviour {

    public long collisionTime;

    private void OnTriggerEnter(Collider other) {
        collisionTime = DateTime.Now.Ticks;
        Debug.Log(collisionTime);
    }
}
