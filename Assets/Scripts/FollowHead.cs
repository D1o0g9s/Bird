using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHead : MonoBehaviour {

    public GameObject camera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.localPosition = camera.transform.localPosition;
        //Debug.Log(camera.transform.rotation.eulerAngles.y);
        this.transform.localRotation = Quaternion.Euler(0.0f, camera.transform.rotation.eulerAngles.y, 0.0f);
    }
}
