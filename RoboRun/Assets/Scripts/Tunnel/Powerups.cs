using UnityEngine;
using System.Collections;

public class Powerups : MonoBehaviour {

    public Texture2D gold, oil;
    
    // Use this for initialization
	void Start () {
        float rand = Random.Range(0.0f, 1.0f);
        if (rand < 0.5f) {
            renderer.material.mainTexture = gold;
            gameObject.name = "gold";
        }
        else {
            renderer.material.mainTexture = oil;
            gameObject.name = "oil";
        }
	}
	
	// Update is called once per frame
	void Update () {
        renderer.material.SetFloat("_DistanceTravelled", TunnelGameManager.instance.distanceTravelled);
	}

    void FixedUpdate() {
        transform.Rotate(transform.up, 2);
    }

}
