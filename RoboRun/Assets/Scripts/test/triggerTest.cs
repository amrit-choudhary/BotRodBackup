﻿using UnityEngine;
using System.Collections;

public class triggerTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col) {
        Debug.Log(col.name);
    }

}
