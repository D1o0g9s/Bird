using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rings : MonoBehaviour {

    public static int ringsTraveled = 0;

    public GameObject player;
    public GameObject ring;
    public GameObject p;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (player.GetComponent<PlayerMovement>().speed == 0) {
            ring.GetComponent<Renderer>().material.color = Color.red;
            ringsTraveled = 0;
            p.SetActive(false);
        }
	}

    private void OnTriggerEnter(Collider other) {
        ring.GetComponent<Renderer>().material.color = Color.green;
        ringsTraveled++;
            p.SetActive(true);
    }
}
