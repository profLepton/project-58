using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyMain : MonoBehaviour
{
    //Getting componenets
    private Rigidbody2D playerRB;


    //Preference Variables
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField] private float fallJumpMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    [SerializeField] private LayerMask platform;
    

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal != 0)
        {
           playerRB.velocity = new Vector2(horizontal * runSpeed + playerRB.velocity.x  * Time.deltaTime, playerRB.velocity.y);
            
        }

        if(Input.GetButton("Jump"))
        {
            if(playerRB.IsTouchingLayers(platform))
            {
                playerRB.velocity = Vector2.up * jumpSpeed;
            }
            
        }

        if (playerRB.velocity.y < 0)
        {
            playerRB.velocity += Vector2.up * Physics2D.gravity.y * (fallJumpMultiplier - 1) * Time.deltaTime;
        }
        else if (playerRB.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            playerRB.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    
    }

}
