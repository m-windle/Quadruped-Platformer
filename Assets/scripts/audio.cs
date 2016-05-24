using UnityEngine;
using System.Collections;

public class audio : MonoBehaviour {

    private AudioSource hooves;

    void Awake()
    {
        hooves = gameObject.GetComponent<AudioSource>();
        hooves.Play();
        hooves.mute = true;
    }

	void Update () {
        if (Input.GetKey(KeyCode.UpArrow))
            hooves.mute = false;

        if (Input.GetKeyUp(KeyCode.UpArrow))
            hooves.mute = true;
	}
}
