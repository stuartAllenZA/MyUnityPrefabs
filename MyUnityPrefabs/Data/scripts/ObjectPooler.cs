using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

	public static ObjectPooler pooler;
	public int poolAmount;
	public bool dynamicSize = true; 
	public GameObject objectToPool;

	List<GameObject> pooledObjects; 

	// invoke singleton here
	void Awake() {
		pooler = this;
	}

	void Start() {
		pooledObjects = new List<GameObject> ();
		for (int i = 0; i < poolAmount; i++) {
			GameObject temp = (GameObject)Instantiate (objectToPool);
			temp.SetActive (false);
			pooledObjects.Add (temp); 
		}
	} 

	public GameObject GetInstance () {
		for (int i = 0; i < pooledObjects.Count; i++) {
			if (!pooledObjects [i].activeInHierarcy) {
				return pooledObjects [i];
			}
		}

		if (dynamicSize) {
			GameObject temp = (GameObject)Instantiate (objectToPool);
			pooledObjects.Add (temp);
			return temp;
		}

		return null;
	}
}

/*
	Example use 

	void someFunc() {
		GameObject temp = ObjectPooler.pooler.GetInstance();
		if (temp == null) return;
		// else use like normal
	}
* /