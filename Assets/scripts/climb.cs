using UnityEngine;
using System.Collections;

public class climb : MonoBehaviour {

    // Reference http://answers.unity3d.com/questions/621651/how-to-make-a-simple-ladder.html
    // Modified to work with my ladder which is not vertical
    // orig ladder angle 343.8282

    public GameObject player;
    public float speed = 1;
    public float rotation_speed = 10f;
    
    private bool climbing;

	// Update is called once per frame
	void FixedUpdate () {
        if(climbing){
            player.GetComponent<Rigidbody>().useGravity = false;

            // Movement up
            if (Input.GetKey(KeyCode.UpArrow))
            {
                player.GetComponent<Rigidbody>().velocity = (transform.up * Time.deltaTime * speed) + (transform.forward * Time.deltaTime);
                player.GetComponentInChildren<Animator>().Play("climbUp");
            }

            // Movement down
            if (Input.GetKey(KeyCode.DownArrow))
            {
                player.GetComponent<Rigidbody>().velocity = (-transform.up * Time.deltaTime * speed) + (transform.forward * Time.deltaTime);
                player.GetComponentInChildren<Animator>().Play("climbUp");
            }

            // On key release, idle on ladder
            if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                player.GetComponent<Rigidbody>().velocity = Vector3.zero;
                player.GetComponentInChildren<Animator>().Play("climbIdle");
            }

            // Turning
            if (Input.GetKey(KeyCode.LeftArrow))
                player.GetComponent<Rigidbody>().transform.Rotate(-Vector3.up * rotation_speed * Time.deltaTime);
            if (Input.GetKey(KeyCode.RightArrow))
                player.GetComponent<Rigidbody>().transform.Rotate(Vector3.up * rotation_speed * Time.deltaTime);
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        // If the object colliding w/ ladder is the player
        if (collision.gameObject == player){
            climbing = true;
            collision.gameObject.transform.parent = transform;
            collision.gameObject.GetComponentInChildren<Animator>().Play("climbIdle");
            collision.gameObject.GetComponent<walk>().enabled = false;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == player)
        {
            climbing = false;
            collision.gameObject.transform.parent = null;
            collision.gameObject.GetComponent<walk>().enabled = true;
            player.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        //if (climbing)
        //    collision.gameObject.GetComponentInChildren<Animator>().Play("climbIdle");
    }
}
