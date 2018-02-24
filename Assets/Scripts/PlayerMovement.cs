using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private long lowerTick = 0;
    private long upperTick = 0;

    GameObject lowerCollider;
    GameObject upperCollider;

    TriggerTime lowerColliderScript;
    TriggerTime upperColliderScript;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        upperTick = upperColliderScript.collisionTime;

        if (lowerTick !=lowerColliderScript.collisionTime)
	}
}
