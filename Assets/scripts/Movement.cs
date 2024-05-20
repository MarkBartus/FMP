using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    Animator anim;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jumpHeight = 3f;

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public float jumpCooldown = 3f;
    bool readyToJump = true;

    Vector3 velocity;
    bool isGrounded;

    bool combatMode;
    bool attacking;
    public float spin = 2f;
    bool readyToSpin = true;
    public float attackingCooldown = 2f;

  
    private void Start()
    {

        anim = GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    // Update is called once per frame
    void Update()
    {
        Attack();

        movement();

        speed = 4;



    }
    private void ResetJump()
    {
        readyToJump = true;
    }

    void Attack()
    {

        anim.SetBool("attack1", false);
        anim.SetBool("attack2", false);
     


        if (Input.GetKey(KeyCode.Mouse0))
        {

            anim.SetBool("attack1", true);
            
        }


    }

    void movement()
    {
        anim.SetBool("walk", false);
        anim.SetBool("Run", false);
        anim.SetBool("jrun", false);
        anim.SetBool("attack2", false);

        if (Input.GetKey(KeyCode.Mouse1) && readyToSpin)
        {
            anim.SetBool("attack2", true);
            StartCoroutine(CoolDownFunction1());
        }
        IEnumerator CoolDownFunction1()
        {
            readyToSpin = false;
            yield return new WaitForSeconds(spin);
            readyToSpin = true;
        }
        if (attacking == false)
        {


            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                {
                    anim.SetBool("walk", false);
                    anim.SetBool("Run", true);
                    speed = 6;



                    if (Input.GetButtonDown("Jump") && isGrounded && (Input.GetKey(KeyCode.LeftShift) && readyToJump))
                    {
                        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
                        readyToJump = false;

                        anim.SetBool("jrun", true);
                        anim.SetBool("walk", false);

                        speed = 2f;

                        StartCoroutine(CoolDownFunction());
                    }
                }
            }

            IEnumerator CoolDownFunction()
            {
                readyToJump = false;
                yield return new WaitForSeconds(jumpCooldown);
                readyToJump = true;
            }

            if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("walk", true);

            }
            if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("walk", true);

            }
            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("walk", true);

            }
            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("walk", true);

            }

            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);

        }



    }
}
