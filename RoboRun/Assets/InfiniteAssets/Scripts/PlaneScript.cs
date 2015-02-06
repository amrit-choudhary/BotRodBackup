using UnityEngine;
using System.Collections;

public class PlaneScript : MonoBehaviour {

    Mesh mesh;
    MeshCollider meshCollider;
    Vector3[] vertices = new Vector3[4];
    Vector2[] uv = new Vector2[4];
    int[] traingles = new int[6];

	// Use this for initialization
	void Start () {
        mesh = GetComponent<MeshFilter>().mesh;
        meshCollider = GetComponent<MeshCollider>();

        //transform.position = (GameManagerScript.instance.v0 + GameManagerScript.instance.v1 + GameManagerScript.instance.v2 + GameManagerScript.instance.v3) / 4;

        vertices[0] = GameManagerScript.instance.v0;
        uv[0] = new Vector2(0, 0);
        vertices[1] = GameManagerScript.instance.v1;
        uv[1] = new Vector2(1, 0);
        vertices[2] = GameManagerScript.instance.v2;
        uv[2] = new Vector2(1, 1);
        vertices[3] = GameManagerScript.instance.v3;
        uv[3] = new Vector2(0, 1);

        traingles[0] = 0; traingles[1] = 2; traingles[2] = 1; traingles[3] = 0; traingles[4] = 3; traingles[5] = 2;

        mesh.vertices = vertices;
        mesh.triangles = traingles;
        mesh.uv = uv;
        mesh.RecalculateNormals();
        meshCollider.sharedMesh = mesh;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Reposition() {
        mesh = GetComponent<MeshFilter>().mesh;
        meshCollider = GetComponent<MeshCollider>();

        vertices[0] = GameManagerScript.instance.v0;
        vertices[1] = GameManagerScript.instance.v1;
        vertices[2] = GameManagerScript.instance.v2;
        vertices[3] = GameManagerScript.instance.v3;

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();                                               // recalculate bounds for camera frustum culling
        meshCollider.sharedMesh = null;                                         // must reset the sharemesh before reassigning or it wont register chagne
        meshCollider.sharedMesh = GetComponent<MeshFilter>().mesh;
    }






}
