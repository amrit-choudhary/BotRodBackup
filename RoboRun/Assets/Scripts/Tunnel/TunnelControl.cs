using UnityEngine;
using System.Collections;

public class TunnelControl : MonoBehaviour {

    public static TunnelControl instance;
    public float speed;
    public float turnSpeed;
    public GameObject sky;
    float turn;

    void Awake() {
        instance = this;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.forward, TunnelGameManager.instance.turn * Time.deltaTime);
        sky.transform.Rotate(Vector3.forward, TunnelGameManager.instance.turn * Time.deltaTime);
	}
}
