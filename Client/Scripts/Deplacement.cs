using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Deplacement : MonoBehaviour
{
    public float speed = 7f;
    public float jumpspeed = 8f;
    public float gravity = 20f;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController Cc;
    private Animator anim;
    public int Index;
    
    void Start()
    {
        Cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {            
        if (Index != Datas.instance.MyIndex)
            return;
            
        // Initialising Variable
        float horizontalPress = Input.GetAxis("Horizontal");
        float verticalPress = Input.GetAxis("Vertical");
        bool spacePress = Input.GetButton("Jump");

        MoveTo(horizontalPress, verticalPress, spacePress);
        
        // Network
        /*
        float x = horizontalPress;
        float y = Cc.transform.position.y;
        float z = Cc.transform.position.z;

        float rotX = transform.rotation.x;
        float rotY = transform.rotation.y;
        float rotZ = transform.rotation.z;
        float rotW = transform.rotation.w;*/
        
        ClientSendData.instance.SendMovement(horizontalPress,verticalPress,spacePress ? 1f : 0f,0,0,0,0);  
        
    }

    public void MoveTo(float horizontalPress, float verticalPress,  bool spacePress)
    {
        if (Cc.isGrounded)
        {
            //Translate
            //sprint
            if (verticalPress < 0)
            {
                anim.SetBool("Sprint", true);
                verticalPress = -verticalPress;
            }
            //Course
            if (verticalPress > 0)
                anim.SetBool("Run", true);

            moveDirection = new Vector3(0, 0, verticalPress);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            //==============================Animation Translate========================================//
            //Idle
            if (verticalPress == 0)
            {
                anim.SetBool("Run", false);
                anim.SetBool("Sprint", false);
            }

            // Character Jump
            if (spacePress)
            {
                moveDirection.y = jumpspeed;
                anim.SetBool("Jump", true); //Animation jump
            }
            else 
                anim.SetBool("Jump", false); //Animation jump
        }

        // After Character Jump
        moveDirection.y -= gravity * Time.deltaTime;
        
        // Rotate
        transform.Rotate(Vector3.up * horizontalPress * Time.deltaTime *speed * 20);
        Cc.Move(moveDirection * Time.deltaTime);
        
        if (horizontalPress == 0)
        {
            anim.SetBool("TurnR", false);
            anim.SetBool("TurnL", false);
        }  
        //Turn Right
        if (horizontalPress > 0)
            anim.SetBool("TurnR", true);
        //Turn Left
        if (horizontalPress < 0)
            anim.SetBool("TurnL", true);
    }
}
