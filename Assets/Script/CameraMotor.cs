using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour {
    public Transform lookAt;

    private Vector3 offset = new Vector3(0, 0, -6);

    // Use this for initialization
	private void Start () {
        Debug.Log(lookAt.name);
	}
	
	// Update is called once per frame
	private void LateUpdate () {
        transform.position = lookAt.transform.position + offset;
	}
}
