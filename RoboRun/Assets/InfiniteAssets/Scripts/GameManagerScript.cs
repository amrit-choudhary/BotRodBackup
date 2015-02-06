using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

    public static GameManagerScript instance;
    public GameObject plane;
    public GameObject track;
    public GameObject greenOrb, redOrb;
    [HideInInspector]
    public Vector3 v0, v1, v2, v3, forward;
    GameObject temp;
    int singleTrackLength = 1;
    bool autoGenerateTrack = true;

    float timer, autoGenTimer;
    static float autoGenTime = 2.5f;

    void Awake() {
        instance = this;
    }

    // Use this for initialization
	void Start () {
        v0 = new Vector3(0, 0, 0);
        v1 = new Vector3(2, 0, 0);
        v2 = new Vector3(2, 0, 2);
        v3 = new Vector3(0, 0, 2);
        forward = ((v2 + v3) / 2 - (v0 + v1)/2).normalized;

        timer = 0; autoGenTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
            RestartGame();

        if (autoGenerateTrack) {
            timer += Time.deltaTime;
            autoGenTimer += Time.deltaTime;

            if (timer > 0.01f) {
                Spawn();
                timer = 0;
            }

            if (autoGenTimer > autoGenTime)
                autoGenerateTrack = false;
        }
	}

    void Spawn() {
        temp = (GameObject)GameObject.Instantiate(plane);
        temp.transform.parent = track.transform;

        v1 = v2;
        v0 = v3;
        v2 = v1 + forward * 1;
        v3 = v0 + forward * 1;
        forward = ((v2 + v3) / 2 - (v0 + v1) / 2).normalized;
        ModifyForward();
    }

    void ModifyForward() {
        float noiseX, noiseY, noiseZ, factorX, factorY, factorZ, temp;
        factorX = 0.06f; factorY = 0.02f; factorZ = 0.02f; temp = 0.1f; float temp2 = 2.0f;
        noiseX = factorX * (Mathf.PerlinNoise(Time.time * temp, 0) - 0.5f);
        noiseY = factorY * (Mathf.PerlinNoise(Time.time * temp, 1) - 0.5f);
        noiseZ = factorZ * (Mathf.PerlinNoise(Time.time * temp, 2) - 0.5f);
        Quaternion quaternion = new Quaternion(noiseX, noiseY, noiseZ, 1);
        forward = quaternion * forward;
    }

    public void SpawnNewTrack(GameObject track) {
        if (!autoGenerateTrack)
            RelocateTrack(track);
        else
            Destroy(track);
    }

    void RelocateTrack(GameObject track) {
        v1 = v2;
        v0 = v3;
        v2 = v1 + forward * 1;
        v3 = v0 + forward * 1;
        forward = ((v2 + v3) / 2 - (v0 + v1) / 2).normalized;
        ModifyForward();
        track.gameObject.GetComponent<PlaneScript>().Reposition();
        SpawnOrb();
    }

    void SpawnOrb() {
        int rand = Random.Range(0, 40);
        GameObject orb;

        if (rand < 1) {
            Vector3 temp = (v0 + v1 + v2 + v3) / 4;
            int rand2 = Random.Range(0, 2);
            if(rand2 < 1)
                orb = (GameObject)GameObject.Instantiate(greenOrb, temp, Quaternion.identity);
            else
                orb = (GameObject)GameObject.Instantiate(redOrb, temp, Quaternion.identity);
        }
    }

    public void RestartGame(){
        Application.LoadLevel(Application.loadedLevel);
    }


}
