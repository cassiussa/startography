using UnityEngine;
using System.Collections;
using UnityEditor;

public class StarMeshResize : MonoBehaviour {
	public float resizeScale = 1f;
	public Mesh mesh;
	public Vector3[] verticesOrig;
	public Vector3[] vertices;
	public Vector2[] uvsOrig;
	public Vector2[] uvs;
	public Vector2[] uvsOrig1;
	public Vector2[] uvs1;
	public Vector2[] uvsOrig2;
	public Vector2[] uvs2;

	bool run = false;

	//public float[] trisOrigin;
	//public float[] tris;
	// Use this for initialization
	void Update () {
		if (Time.time > 3.0f && run == false) {
			run = true;
			mesh = GetComponent<MeshFilter> ().mesh;
			//bah = mesh;
			verticesOrig = new Vector3[mesh.vertices.Length];
			vertices = new Vector3[mesh.vertices.Length];
			uvsOrig = new Vector2[mesh.uv.Length];
			uvs = new Vector2[mesh.uv.Length];
			uvsOrig1 = new Vector2[mesh.uv1.Length];
			uvs1 = new Vector2[mesh.uv1.Length];
			uvsOrig2 = new Vector2[mesh.uv2.Length];
			uvs2 = new Vector2[mesh.uv2.Length];
			//trisOrigin = new float[mesh.triangles.Length];
			//tris = new float[mesh.triangles.Length];


			verticesOrig = mesh.vertices;
			uvsOrig = mesh.uv;
			uvsOrig1 = mesh.uv1;
			uvsOrig2 = mesh.uv2;
			//trisOrigin = mesh.triangles;


			int i = 0;
			while (i < verticesOrig.Length) {
				vertices [i] = verticesOrig [i] * resizeScale;
				uvs [i] = new Vector2 (uvsOrig [i].x * resizeScale, uvsOrig [i].y * resizeScale);
				i++;
			}
			i = 0;
			while (i < uvsOrig1.Length) {
				uvs1 [i] = new Vector2 (uvsOrig1 [i].x * resizeScale, uvsOrig1 [i].y * resizeScale);
				i++;
			}
			i = 0;
			while (i < uvsOrig2.Length) {
				uvs2 [i] = new Vector2 (uvsOrig2 [i].x * resizeScale, uvsOrig2 [i].y * resizeScale);
				i++;
			}
			/*i = 0;
			while (i < trisOrigin.Length) {
				tris[i] = trisOrigin[i] * resizeScale;
				i++;
			}*/
			/*mesh.vertices = vertices;
			mesh.uv = uvs;
			mesh.uv1 = uvs1;`
			mesh.uv2 = uvs2;
			mesh.triangles = tris;*/

			Mesh newMesh = new Mesh();
			newMesh.vertices = vertices;
			newMesh.uv = uvs;
			newMesh.uv1 = uvs1;
			newMesh.uv2 = uvs2;
			//newMesh.triangles = tris;


			AssetDatabase.CreateAsset (mesh, "Assets/sphere_resized7.asset");
			AssetDatabase.SaveAssets ();
		}

	}

}
