using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Transform groundCheck;
    public LayerMask groundMask;
    public Slider slider;
    public float walkSpeed;
    public float sprintSpeed;
    float speed;
    public float turnSmoothTime = 0.1f;
    public float gravity = -9.81f;
    public float jumpHeigt=3f;
    public float groundDistance =1f;
    public bool isGrounded;
    Vector3 velocity;
    float turnSmoothVelocity;
    public bool isRunning;
    float stamina;
    public float maxStamina;


    private void Start()
    {
        slider.maxValue = maxStamina;
        slider.minValue = 0;
        stamina = maxStamina;
        speed = walkSpeed;
    }
    void Update()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

 
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
        {
            SetRunning(true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            SetRunning(false);
        }
     
        if (isRunning)
        {
            stamina -= Time.deltaTime;
            slider.value = stamina;
            if (stamina < 0)
            {
                stamina = 0;
                SetRunning(false);
            }
        }
        else if(stamina < maxStamina)
        {
            stamina += Time.deltaTime;
        }
        

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
                    

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
      
        if (isGrounded && velocity.y <=0f)
        {
            velocity.y = -2f;
            isGrounded = true;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeigt * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void SetRunning(bool isRunning)
    {
        slider.gameObject.SetActive(isRunning);
        this.isRunning = isRunning;
        speed = isRunning  ? sprintSpeed : walkSpeed;
    }

}
