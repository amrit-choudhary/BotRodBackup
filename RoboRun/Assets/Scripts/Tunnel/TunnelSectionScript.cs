using UnityEngine;
using System.Collections;

public class TunnelSectionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.z > 3) {
            TunnelCreatorScript.instance.GenerateBlock();
            Destroy(gameObject);
        }
	}

    void FixedUpdate() {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + TunnelGameManager.instance.speed * Time.deltaTime);
    }


}
