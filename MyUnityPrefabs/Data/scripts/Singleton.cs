using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine;

// save file should not be available locally for multiplayer games 

public class Singleton : MonoBehaviour {

	// allow extern access (not very secure, but saves time re: getCompontent). easy refactor?
	public static Singleton thisGameObject;

	// decide on data structure responsible for handling player data (perhaps a constructor and copy-constructor for player data, and a method for the singleton)
		// update : scriptable object ^? decouples gameObject and objectData
	// also save file name format -  dynamic
	// persist all save file names for quick retrieval and display in gui? or read directory and list results? Maybe read directory on awake and save data in list?
	public int temp = 0;

	// Use this for initialization
	void Awake () {
		if (thisGameObject == null) {
			DontDestroyOnLoad (gameObject);
			thisGameObject = this;
		} else if (thisGameObject != this)
			Destroy (gameObject); 
	}

	public void Save() {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "playerSaveFile.dat", FileMode.Open);

		PlayerInfos playerData = new PlayerInfos ();
		playerData.temp = temp;

		bf.Serialize (file, playerData);
		file.Close ();
	}

	public void Load() {
		if (File.Exists (Application.persistentDataPath + "playerSaveFile.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "playerSaveFile.dat", FileMode.Open);
			PlayerInfos data = (PlayerInfos)bf.Deserialize (file);
			file.Close ();

			temp = data.temp;
		}
	}
}

// this would be the scriptable object ? -> editor scripting
[Serializable]
class PlayerInfos {
	public int temp;
}