using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class swingScript : MonoBehaviour
{
    //Get componenets
    [SerializeField] private LineRenderer line;
    private DistanceJoint2D distJoint;

    //Create Stuff
    private RaycastHit2D rayHit;
    private Vector3 targetLoc;
    public static Vector2 myVector;
  
    //Serialized fields
    [SerializeField] private float maxDist = 1000f;
    [SerializeField] private LayerMask swingLayer;
    [SerializeField] private float step = 20f;

    //Free variable
    public static bool onRope; 

    // Start is called before the first frame update
    void Start()
    {
        distJoint = GetComponent<DistanceJoint2D>();
        distJoint.enabled = false;
        line.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        onRope = distJoint.enabled;
        if(distJoint.distance > 0.1f && Input.GetMouseButton(0))
        {
            distJoint.distance -= step * Time.deltaTime;
        }
        
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            targetLoc = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetLoc.z = 0;
            rayHit = Physics2D.Raycast(transform.position, targetLoc - transform.position, maxDist, swingLayer);
            

            if(rayHit.collider != null && rayHit.collider.gameObject.GetComponent<Rigidbody2D>())
            {
                distJoint.enabled = true;
                distJoint.connectedAnchor = rayHit.point - new Vector2(rayHit.collider.transform.position.x, rayHit.collider.transform.position.y);
                distJoint.connectedBody = rayHit.collider.gameObject.GetComponent<Rigidbody2D>();
                distJoint.distance = Vector2.Distance(transform.position, rayHit.point);
                line.enabled = true;
                line.SetPosition(0, transform.position);
                line.SetPosition(1, rayHit.point + 0.1f * (rayHit.point - new Vector2(transform.position.x, transform.position.y) ));
                   
            }

        }

        if(Input.GetKey(KeyCode.E))
        {
            line.SetPosition(0, transform.position);
        }
        if(Input.GetKeyUp(KeyCode.E))
        {
            distJoint.enabled = false;
            line.enabled = false;
        }
    }
}
