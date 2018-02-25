using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHead : MonoBehaviour {

    public GameObject camera;
    private AudioSource aud;
    public GameObject player;

	// Use this for initialization
	void Start () {
        aud = GetComponent<AudioSource>();
        aud.Play();
	}
	
	// Update is called once per frame
	void Update () {
        aud.volume = player.GetComponent<PlayerMovement>().speed / 15.0f;
        this.transform.localPosition = camera.transform.localPosition;
        //Debug.Log(camera.transform.rotation.eulerAngles.y);
        this.transform.localRotation = Quaternion.Euler(0.0f, camera.transform.rotation.eulerAngles.y, 0.0f);
    }
}
