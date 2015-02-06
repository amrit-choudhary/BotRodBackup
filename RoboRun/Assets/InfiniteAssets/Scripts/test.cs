using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

    Mesh mesh;
    MeshCollider meshCollider;
    Vector3[] vertices = new Vector3[4];
    Vector2[] uv = new Vector2[4];
    int[] traingles = new int[6];

    public Vector3 vec0, vec1, vec2, vec3;

    // Use this for initialization
	void Start () {
        mesh = GetComponent<MeshFilter>().mesh;
        meshCollider = GetComponent<MeshCollider>();

        vertices[0] = vec0;
        uv[0] = new Vector2(0, 0);
        vertices[1] = vec1;
        uv[1] = new Vector2(1, 0);
        vertices[2] = vec2;
        uv[2] = new Vector2(1, 1);
        vertices[3] = vec3;
        uv[3] = new Vector2(0, 1);

        traingles[0] = 0; traingles[1] = 2; traingles[2] = 1; traingles[3] = 0; traingles[4] = 3; traingles[5] = 2;

        mesh.vertices = vertices;
        mesh.triangles = traingles;
        mesh.uv = uv;
        mesh.RecalculateNormals();

	}
	
	// Update is called once per frame
    void Update() {
        if (Input.GetKeyUp(KeyCode.Return)) {
            vertices[0] = vec0;
            vertices[1] = vec1;
            vertices[2] = vec2;
            vertices[3] = vec3;
            mesh.vertices = vertices;
            mesh.RecalculateNormals();
            meshCollider.sharedMesh = mesh;
        }

    }

}
