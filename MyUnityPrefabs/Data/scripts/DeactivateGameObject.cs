using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateGameObject : MonoBehaviour {

	public float lifeSpan = 2.0f;

	void OnEnable() { Invoke("Deactivate", lifeSpan); }

	void Deactivate(float lifeSpan) { gameObject.SetActive (false); }

	void OnDisable() { CancelInvoke (); }
}
