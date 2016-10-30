using UnityEngine;
using System.Collections;
using UnityEditor;

[InitializeOnLoad]
public class Tags {
	
	// STARTUP
	static Tags() {
		CreateLayer();
	}
	
	
	// Creates a new layer
	static void CreateLayer() {
		SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
		
		SerializedProperty it = tagManager.GetIterator();
		bool showChildren = true;
		while (it.NextVisible(showChildren)) {
			// Set the tags
			if (it.name == "User Layer 8") {
				it.stringValue = "Distance Collider 1";
			} else if (it.name == "User Layer 9") {
				it.stringValue = "Distance Collider 2";
			} else if (it.name == "User Layer 10") {
				it.stringValue = "Distance Collider 3";
			} else if (it.name == "User Layer 11") {
				it.stringValue = "Distance Collider 4";
			} else if (it.name == "User Layer 12") {
				it.stringValue = "Distance Collider 5";
			} else if (it.name == "User Layer 13") {
				it.stringValue = "Distance Collider 6";
			} else if (it.name == "User Layer 14") {
				it.stringValue = "Distance Collider 7";
			} else if (it.name == "User Layer 15") {
				it.stringValue = "Distance Collider 8";
			} else if (it.name == "User Layer 16") {
				it.stringValue = "Distance Collider 9";
			} else if (it.name == "User Layer 17") {
				it.stringValue = "Distance Collider 10";
			} else if (it.name == "User Layer 18") {
				it.stringValue = "Distance Collider 11";
			} else if (it.name == "User Layer 19") {
				it.stringValue = "Distance Collider 12";
			} else if (it.name == "User Layer 20") {
				it.stringValue = "Distance Collider 13";
			} else if (it.name == "User Layer 21") {
				it.stringValue = "Distance Collider 14";
			} else if (it.name == "User Layer 22") {
				it.stringValue = "Distance Collider 15";
			} else if (it.name == "User Layer 23") {
				it.stringValue = "Distance Collider 16";
			} else if (it.name == "User Layer 24") {
				it.stringValue = "Distance Collider 17";
			} else if (it.name == "User Layer 25") {
				it.stringValue = "Distance Collider 18";
			}

			for(int i=0;i<32;i++) {
				for(int b=0;b<32;b++) {
					if(b != i && b != 0)
						Physics.IgnoreLayerCollision (b, i, true);//Physics.GetIgnoreLayerCollision (b,i));
					else if (b == 0)
						Physics.IgnoreLayerCollision (b, i, false);
				}
			}
		}
		tagManager.ApplyModifiedProperties();
	}

}