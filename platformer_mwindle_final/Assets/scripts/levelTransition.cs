using UnityEngine;
using System.Collections;

public class levelTransition : MonoBehaviour {
    public string level;

    void OnTriggerEnter(Collider other)
    {
        Application.LoadLevel(level);
    }    
}
