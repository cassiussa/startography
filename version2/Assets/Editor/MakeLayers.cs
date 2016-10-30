using UnityEngine;
using System.Collections;
using UnityEditor;

[InitializeOnLoad]
public class Tags {
	
	//STARTUP
	static Tags() {
		CreateLayer();
	}
	
	
	//creates a new layer
	static void CreateLayer() {
		SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
		
		SerializedProperty it = tagManager.GetIterator();
		bool showChildren = true;
		while (it.NextVisible(showChildren)) {
			//set your tags here
			if (it.name == "User Layer 8") {
				it.stringValue = "meter";
			} else if (it.name == "User Layer 9") {
				it.stringValue = "kilometer";
			} else if (it.name == "User Layer 10") {
				it.stringValue = "megameter";
			} else if (it.name == "User Layer 11") {
				it.stringValue = "gigameter";
			} else if (it.name == "User Layer 12") {
				it.stringValue = "terameter";
			} else if (it.name == "User Layer 13") {
				it.stringValue = "petameter";
			} else if (it.name == "User Layer 14") {
				it.stringValue = "exameter";
			} else if (it.name == "User Layer 15") {
				it.stringValue = "zetameter";
			} else if (it.name == "User Layer 16") {
				it.stringValue = "yottameter";
			}
		}
		tagManager.ApplyModifiedProperties();
	}

}