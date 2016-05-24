using UnityEngine;
using System.Collections;

public class platformMov : MonoBehaviour {
    public GameObject toObject;
    public Vector3 origPoint;
    float distance;
    bool reached = false;

    public void Start()
    {
        origPoint = transform.position;
    }

    public void Update()
    {
        if (!reached)
        {
            distance = Vector3.Distance(transform.position, toObject.transform.position);
            if (distance > .1)
            {
                move(transform.position, toObject.transform.position);
            }
            else
            {
                reached = true;
            }
        }
        else
        {
            distance = Vector3.Distance(transform.position, origPoint);
            if (distance > .1)
            {
                move(transform.position, origPoint);
            }
            else
            {
                reached = false;
            }
        }
    }

    void move(Vector3 pos, Vector3 towards)
    {
        transform.position = Vector3.MoveTowards(pos, towards, .1f);
    }

    //public float speed = 10f; 

    //private Vector3 direction = Vector3.forward; 

    //// Use this for initialization
    //void Start () {
        
    //}
	
    //// Update is called once per frame
    //void Update () {
    //    //gameObject.GetComponent<Rigidbody>().velocity = direction * speed;
    //    transform.Translate(direction * speed * Time.deltaTime);
    //}

    //void OnCollisionEnter(Collision collision) {
    //    direction = -Vector3.forward;
    //}
}
