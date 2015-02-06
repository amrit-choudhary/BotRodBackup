using UnityEngine;
using System.Collections;

public class RotationScript : MonoBehaviour {

    public int RotationDirection = 1;
    
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(transform.up, 2 * RotationDirection );
	}
}
