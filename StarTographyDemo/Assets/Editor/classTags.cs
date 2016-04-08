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
	static void CreateLayer(){
		SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);

		SerializedProperty it = tagManager.GetIterator();
		bool showChildren = true;
		while (it.NextVisible(showChildren)) {
			//set your tags here
			if (it.name == "User Layer 8") {
				it.stringValue = "Layer 1\t";
			} else if(it.name == "User Layer 9") {
				it.stringValue = "Layer 2\t";
			} else if(it.name == "User Layer 10") {
				it.stringValue = "Layer 3\t";
			} else if(it.name == "User Layer 11") {
				it.stringValue = "Layer 4\t";
			} else if(it.name == "User Layer 12") {
				it.stringValue = "Layer 5\t";
			} else if(it.name == "User Layer 13") {
				it.stringValue = "Layer 6\t";
			} else if(it.name == "User Layer 14") {
				it.stringValue = "Layer 7\t";
			} else if(it.name == "User Layer 15") {
				it.stringValue = "Layer 8\t";
			} else if(it.name == "User Layer 16") {
				it.stringValue = "Layer 9\t";
			} else if(it.name == "User Layer 17") {
				it.stringValue = "Layer 10\t";
			} else if(it.name == "User Layer 18") {
				it.stringValue = "Layer 11\t";
			} else if(it.name == "User Layer 19") {
				it.stringValue = "Layer 12\t";
			} else if(it.name == "User Layer 20") {
				it.stringValue = "Layer 13\t";
			} else if(it.name == "User Layer 21") {
				it.stringValue = "Layer 14\t";
			} else if(it.name == "User Layer 22") {
				it.stringValue = "Layer 15\t";
			} else if(it.name == "User Layer 23") {
				it.stringValue = "Layer 16\t";
			} else if(it.name == "User Layer 24") {
				it.stringValue = "Layer 17\t";
			} else if(it.name == "User Layer 25") {
				it.stringValue = "Layer 18\t";
			}
		}
		tagManager.ApplyModifiedProperties();
	}
}