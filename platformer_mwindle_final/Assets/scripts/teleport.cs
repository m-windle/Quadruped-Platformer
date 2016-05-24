using UnityEngine;
using System.Collections;

public class teleport : MonoBehaviour {

    public Transform exit;
    static Transform last;

    // Reference for this script is here
    // http://answers.unity3d.com/questions/37356/teleporting-scripts.html
    // Comments made to show I understand what is happening

    void OnTriggerEnter(Collider other)
    {
        // Check if exit is the previously entered teleporter
        // so that the player does not keep bouncing back and forth
        if (exit == last)
            return;

        Teleport(other);
    }

    void OnTriggerExit(Collider other)
    {
        // Remove reference to last teleporter so that it is "open" again
        if (exit == last)
            last = null;
    }

    void Teleport(Collider other)
    {
        // Set the last teleporter used to the teleporter triggered
        last = transform;

        // Set position of the object that collided to the exit set in Unity interface
        other.transform.position = exit.transform.position;
    }
}
