using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam_motor : MonoBehaviour {
    public Transform LookAt;
    public bool smooth = true;
    float smoothRate = 0.125f;
    Vector3 offset = new Vector3(0, 0, -8.5f);
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 desiredpos = LookAt.transform.position + offset;
        if (smooth)        
            transform.position = Vector3.Lerp(transform.position, desiredpos, smoothRate);
        else
            transform.position = desiredpos;
	}
}
