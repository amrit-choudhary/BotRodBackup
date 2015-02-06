using UnityEngine;
using System.Collections;

public class CameraBallFollower : MonoBehaviour {

    public Transform ball;
    public int x, y, z;
 
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = ball.position + new Vector3(x, y, z);    
	}
}
