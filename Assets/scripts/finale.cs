using UnityEngine;
using System.Collections;

public class finale : MonoBehaviour {
	void Start () {
        gameObject.GetComponent<Animator>().Play("finale");
	}
}
