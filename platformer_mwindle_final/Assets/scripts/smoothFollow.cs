using UnityEngine;
using System.Collections;

public class smoothFollow : MonoBehaviour {

    // Referenced this for assistance http://answers.unity3d.com/questions/38526/smooth-follow-camera.html
    // Made changes to suit my needs and also rewrote for C#

    public Transform target;
    public float distance = 10.0F;
    public float height = 5.0F;
    public float heightDamping = 2.0F;
    public float rotationDamping = 3.0F;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void LateUpdate()
    {
        // Early out if we don't have a target
        if (!target)
            return;

        // Calculate the current rotation angles
        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;
        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        gameObject.transform.position = target.position;
        gameObject.transform.position -= currentRotation * Vector3.forward * distance;

        // Set the height of the camera
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, currentHeight, gameObject.transform.position.z);

        // Always look at the target
        transform.LookAt(target);
    }
}
