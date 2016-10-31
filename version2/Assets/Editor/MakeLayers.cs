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
			for(int a=8;a<26;a++) {
				if (it.name == ("User Layer "+a) ) {  // "User Layer 8", "User Layer 9", etc
					it.stringValue = "Camera Layer "+(a-7);
				}
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