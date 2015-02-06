using UnityEngine;
using System.Collections;

public class TunnelPieceScript : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        renderer.material.SetFloat("_DistanceTravelled", TunnelGameManager.instance.distanceTravelled);
	}

}
