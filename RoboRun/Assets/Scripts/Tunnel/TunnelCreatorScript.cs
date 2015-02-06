using UnityEngine;
using System.Collections;

public class TunnelCreatorScript : MonoBehaviour {

    public static TunnelCreatorScript instance;
    
    public GameObject tunnel;
    public GameObject tunnelPiece, tunnelPieceWithPowerups;
    public GameObject tunnelSection;
    public float pieceOffset, pieceFactor, pieceCutoff, distanceBetweenSection;

    float pieceCounter;
    GameObject oldSection;
    Vector3 oldSectionPos = new Vector3(), newSectionPos = new Vector3();

    void Awake() {
        instance = this;
    }
    
    // Use this for initialization
	void Start () {
        pieceCounter = 0;
        oldSection = gameObject;
        GenerateBlock();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GenerateBlock() {
        pieceCounter++;
        GameObject temp;
        oldSectionPos = oldSection.transform.position;
        newSectionPos = oldSectionPos + new Vector3(0, 0, -distanceBetweenSection);
        GameObject tempTunnelSection = (GameObject)Instantiate(tunnelSection, newSectionPos, Quaternion.identity);
        tempTunnelSection.transform.parent = tunnel.transform;
        Quaternion quater;

        for (int i = 0; i < 8; i++) {
            float rand = Mathf.PerlinNoise(i * pieceOffset + pieceFactor * pieceCounter, 0);
            if (rand > pieceCutoff) {
                quater = Quaternion.EulerAngles(0, 0, ((i * 45)  + transform.rotation.eulerAngles.z)* Mathf.PI / 180);

                float randPup = Random.Range(1.0f, 10.0f);
                if(randPup < 1.5f)
                    temp = (GameObject)Instantiate(tunnelPieceWithPowerups, transform.position, quater);
                else
                    temp = (GameObject)Instantiate(tunnelPiece, transform.position, quater);
                
                temp.transform.parent = tempTunnelSection.transform;
            }
        }
        //oldSection = null;
        oldSection = tempTunnelSection;
    }


}
