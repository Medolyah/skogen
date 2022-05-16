using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 3f;

    public float gravity = -26f;

    public float jumpHeight = 4f;

    public Transform groundCheck;

    public float groundDistance = 0.4f;

    public LayerMask groundMask;

    public float sprintSpeed = 5f;

    public float slideSpeed = 6f;

    Vector3 velocity;
    bool isGrounded;
    Animator animator;
    //Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);


        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -5;

        }

        // Controls
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        bool jump = Input.GetKeyDown("space");
        bool sprint = Input.GetKey("left shift");
        //bool notSprint = Input.GetKeyUp("left shift");
        //bool attack = Input.GetKeyDown(KeyCode.Mouse0);


        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
        print(speed);
        print(sprint);
        print(Input.GetKeyDown(KeyCode.W));

        if (Input.GetKeyDown(KeyCode.W) && !sprint)
        {
            //animator.SetBool("Walking", true);

        }

        if (jump && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if (sprint && Input.GetKey(KeyCode.W) && isGrounded && speed < 6)
        {
            speed += sprintSpeed;

        }
        else if (speed > 3 && speed < 7 && !animator.GetBool("Idle"))
        {
            //if(animator.GetBool("Idle"))
            //animator.SetTrigger("Walking");
            //animator.SetBool("Walking", true);

        }

        if (!sprint && speed > 7)
        {
            speed -= sprintSpeed;
            //animator.SetTrigger("Walking");
            //animator.SetBool("Walking", true);

            //animator.SetTrigger("Walking");
        }


        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    Sliding();
        //}
    }


    void OnCollisionEnter(Collision collision)
    {
        print("Player collision!");
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.layer == 6) return;
        if (hit.gameObject.layer == 7)
        {
            print("KAPSEL!");
        }
        //print(hit.gameObject.layer.ToString());
    }
}