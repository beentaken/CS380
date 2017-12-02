using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour {

    public float dragSpeed = 2;
    private Vector3 dragOrigin;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
        }

        if (!Input.GetMouseButton(0))
            return;
        
        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(-pos.x * dragSpeed, -pos.y * dragSpeed, 0.0f);

        transform.Translate(move, Space.World);
    }
}
