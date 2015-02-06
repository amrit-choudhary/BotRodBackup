using UnityEngine;
using System.Collections;

public class SphereRoll : MonoBehaviour {

    public int speed;
    
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate() {
        float x = Input.GetAxis("Horizontal") * -1;
        float y = Input.GetAxis("Vertical") * -1;

        Vector3 movement = new Vector3(x, 0, y);

        rigidbody.AddForce(movement * speed * Time.deltaTime);
    }
}
