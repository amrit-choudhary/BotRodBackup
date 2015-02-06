using UnityEngine;
using System.Collections;

public class TrackCleanupScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col) {
        if (col.tag == "infiTrack") {
            GameManagerScript.instance.SpawnNewTrack(col.gameObject);
        }
        if (col.name == "greenOrb(Clone)" || col.name == "redOrb(Clone)")
            GameObject.Destroy(col.gameObject);
    }
}
