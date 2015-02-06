using UnityEngine;
using System.Collections;

public class FuelMeterHandController : MonoBehaviour {

    int maxHandAngle = 240;
    int minHandAngle = 120;

    int currentHandAngle;

    // Use this for initialization
	void Start () {
        currentHandAngle = 240;
	}
	
	// Update is called once per frame
	void Update () {
        currentHandAngle = 120 + (int)((GameManager.instance.fuel / 100.0f) * 120);
        transform.rotation = Quaternion.Euler(270, currentHandAngle, 0);
	}



}
