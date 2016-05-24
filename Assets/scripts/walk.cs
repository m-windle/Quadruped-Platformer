using UnityEngine;
using System.Collections;

public class walk : MonoBehaviour {

    public float speed = 5f;
    public float stopDrag = 3f;
    public float rotation_speed = 10f;
    public float jumpForce = 10f;
    public float airDrag = 10f; 
    public GameObject player = null;
    public AudioSource hooves;

    private Animator childAnim;
    private float origDrag;
    private bool jumping = false;
    private Vector3 spawn;
    private ParticleSystem ps; 


    void Awake()
    {
        // Particle System
        ps = GetComponent<ParticleSystem>();
        ps.Play();
        ps.enableEmission = false;

        // Audio 
        hooves.Play();
        hooves.mute = true;
    }

	void Start () {
        spawn = transform.position;
        origDrag = gameObject.GetComponent<Rigidbody>().drag;
        childAnim = player.GetComponentInChildren<Animator>();
        childAnim.Play("idle");
	}
	
	void FixedUpdate () {

        if (!jumping)
        {
            // Forward Movement
            if (Input.GetKey(KeyCode.UpArrow))
            {
                gameObject.GetComponent<Rigidbody>().drag = origDrag;
                gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * speed);

                childAnim.Play("walk");
                hooves.mute = false;
            }

            // JUMP
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameObject.GetComponent<Rigidbody>().velocity = (transform.up * jumpForce) + (transform.forward * jumpForce);
                gameObject.GetComponent<Rigidbody>().drag = airDrag;
                
                childAnim.Play("jump");
                jumping = true;
                hooves.mute = true;
            }

            // Prevent sliding upon stopping
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                gameObject.GetComponent<Rigidbody>().drag = stopDrag;

                childAnim.Play("idle");
                hooves.mute = true;
            }
        }

        // Turning
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(-Vector3.up * rotation_speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(Vector3.up * rotation_speed * Time.deltaTime);
	}

    void OnCollisionEnter(Collision collision)
    {
        gameObject.GetComponent<Rigidbody>().drag = origDrag;
        jumping = false;

        if (collision.gameObject.tag == "lava")
        {
            ps.enableEmission = true;
            StartCoroutine(Respawn());
            ps.enableEmission = false;
        }
        if (collision.gameObject.tag == "movingPlatform")
            gameObject.transform.parent = collision.transform;

        childAnim.Play("idle");
    }

    void OnCollisionStay(Collision collision)
    {
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "movingPlatform")
            gameObject.transform.parent = null;
    }


    // Respawn script reference
    // http://answers.unity3d.com/questions/643081/help-me-with-simple-respawn-script-please-c.html
    // Originally I only had the player just jump back to spawn, this looked a lot better
    IEnumerator Respawn()
    {
        // transform.GetComponentsInChildren<Renderer>().enabled = false;
        Renderer[] lChildRenderers = gameObject.GetComponentsInChildren<Renderer>();

        foreach (Renderer lRenderer in lChildRenderers)
        {
            lRenderer.enabled = false;
        }

        yield return new WaitForSeconds(2.0f);
        transform.position = spawn;
        transform.rotation = Quaternion.identity;
        // transform.GetChild(0).GetChild(0).GetComponent<Renderer>().enabled = true;
        
        foreach (Renderer lRenderer in lChildRenderers)
            lRenderer.enabled = true;
    }
}
