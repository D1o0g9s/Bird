using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingController : MonoBehaviour {

    [SerializeField] private GameObject leftWing;
    [SerializeField] private GameObject rightWing;

    [SerializeField] private GameObject leftControllerCollider;
    [SerializeField] private GameObject rightControllerCollider;
    public GameObject camera;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        this.transform.localPosition = camera.transform.localPosition + (new Vector3(0.0f,-0.3f,-2.7f));

        float leftX = leftControllerCollider.transform.localPosition.x;
        float leftY = leftControllerCollider.transform.localPosition.y;
        float leftZ = leftControllerCollider.transform.localPosition.z;

        float rightX = rightControllerCollider.transform.localPosition.x;
        float rightY = rightControllerCollider.transform.localPosition.y;
        float rightZ = rightControllerCollider.transform.localPosition.z;

        float leftDx = Mathf.Sqrt(Mathf.Pow(leftX - camera.transform.localPosition.x, 2) + Mathf.Pow(leftZ - camera.transform.localPosition.z, 2));
        float leftDy = leftY - camera.transform.localPosition.y + 0.3f;
        leftWing.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, -Mathf.Rad2Deg * Mathf.Atan(leftDy / leftDx) + 20.0f);

        float rightDx = Mathf.Sqrt(Mathf.Pow(rightX - camera.transform.localPosition.x, 2) + Mathf.Pow(rightZ - camera.transform.localPosition.z, 2));
        float rightDy = rightY - camera.transform.localPosition.y + 0.3f;
        rightWing.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Rad2Deg * Mathf.Atan(rightDy / rightDx) - 20.0f);
    }
}
