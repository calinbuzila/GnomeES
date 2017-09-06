using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dwarfMain_Controller : MonoBehaviour
{

    // Use this for initialization

    public float speed;
    public float rotationSpeed;
    private CharacterController characterController;
    public float gravity;
    public float jumpSpeed;
    private float tempSpeed;
    private bool escaped = false;
    private Animator animatorController = null;
	GameController mainGameController = null;


    public bool Escaped
    {
        get { return escaped; }
        set { escaped = value; }
    }

    private bool? isJumping;

    public bool? IsJumping
    {
        get { return isJumping; }
        set
        {
            isJumping = value != null ? isJumping = value : isJumping = false;
        }
    }

    //initialisation of own class components
    void Awake()
    {
        animatorController = this.GetComponent<Animator>();
    }

    void Start()
    {
        characterController = this.GetComponent<CharacterController>();
		mainGameController = GameObject.FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    // general movement will be done in fixed update, not dependent on framerate
    void FixedUpdate()
    {
        if (!escaped)
        {
            Vector3 movementVector = Vector3.zero;

            if (Input.GetKey(KeyCode.W))
            {
                movementVector = transform.forward * speed;
				mainGameController.controlTimer = true;
            }

            if (Input.GetKey(KeyCode.S))
            {
                movementVector = -transform.forward * speed;
            }

            //next iteration of fixed update, the gravity will be applied to vertical movement;
            movementVector.y = tempSpeed;
            //Debug.Log(vel);
            //Debug.Log(characterController.isGrounded);
            characterController.Move(movementVector * Time.deltaTime);
            if (characterController.isGrounded)
            {
                tempSpeed = 0;
                if (Input.GetKey(KeyCode.Space))
                {
                    IsJumping = true;
                    tempSpeed = jumpSpeed;
                    //animatorController.SetBool("IsJumping", true);

                }

            }
            else
            {
                //StartCoroutine(ResetJump());
                // normalize gravity apply it to movement, when vertically the character is jumping normalize it to 0 over time.
                tempSpeed -= gravity * Time.deltaTime;
            }
            //Debug.Log(characterController.isGrounded);
            this.transform.Rotate(0, Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime, 0);
        }
    }

    public IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(2);
        //animatorController.SetBool("IsJumping", false);
    }
}
