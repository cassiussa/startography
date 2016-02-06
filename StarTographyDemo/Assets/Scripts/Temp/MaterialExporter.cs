using UnityEngine;
using System.Collections;
using UnityEditor;

public class MaterialExporter : MonoBehaviour {

	bool run = false;
	public Material[] mats;
	public Shader customMat;
	public Texture tex;
	// Use this for initialization
	void Update () {

		if (run == false && Time.time >= 3.0f) {
			int number = 0;
			number = GetComponent<Renderer> ().materials.Length;

			mats = new Material[number];

			for(int a=0;a<number;a++) {
				if(a == 1) {
					Material tempa = new Material(GetComponent<Renderer> ().materials[a]);
					tex = tempa.GetTexture("_AtmosphereLut");
					mats[a] = new Material(tempa);
					mats[a].shader = customMat;
					mats[a].SetTexture("Atmosphere LUT", tex);

					//mats[a] = new Material(GetComponent<Renderer> ().materials[a]);
					Material tempMesh = (Material)UnityEngine.Object.Instantiate(mats[a]);
					AssetDatabase.CreateAsset (tempMesh, "Assets/" + tempMesh.name + "_resized.mat");
					AssetDatabase.SaveAssets ();
					//mats[a] = new Material(mt);
				}
				//Debug.LogError ("mats = "+mats[a].name);

			}
			run = true;
		}
	}

}
