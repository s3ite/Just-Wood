using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Script.NetworkCenter;
using UnityEngine;

public class DeplacementElfe : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float crouchedSpeed = 1f;
    public float jumpSpeed = 10f;
    private float gravity = 20f;
    public Vector3 _moveDirection = Vector3.zero;

    private Animator _anim;
    private CharacterController _cc;

    private readonly string idleString = "Idle";
    private readonly string walkString = "Walk";
    private readonly string runString = "Run";
    private readonly string jumpString = "Jump";
    private readonly string CrouchedIdleString = "CrouchIdle";
    private readonly string CrouchedWalkString = "CrouchWalk";
    private bool isCrouched = true;


    private float horizontalPress;
    private float verticalPress;
    private bool spacePress;
    private bool keyApress;
    private bool crouchPress;


    private AudioManager audioManager;

    private readonly string stepSound = "Foot step";
    private readonly string jumpound = "JumpBreathe";


    private bool isAudioPlayed = false;
    
    // =================================================== Joystick ==========================================
    public Joystick joystick;
    public JoyJumpButton joyJumpButton;
    public JoyCrouchButton joyCrouchButton;


    // ========================================================================================================


    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        _cc = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();

        
        // Code For Android
        if (Application.platform == RuntimePlatform.Android)
        {
            //joystick = FixedJoystick.GetComponent<Joystick> ();
            //joybutton = FindObjectOfType<joybutton>();
        }

        //son
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            
            horizontalPress = joystick.Horizontal;
            verticalPress = joystick.Vertical;

            spacePress = joyJumpButton.isJumpPressed;
            if (spacePress)
            {
                joyJumpButton.isJumpPressed = false;
            }

            crouchPress = joyCrouchButton.isCrouchPressed;

        }
        else
        {
            horizontalPress = Input.GetAxis("Horizontal");
            verticalPress = Input.GetAxis("Vertical");
            spacePress = Input.GetButton("Jump");

            keyApress = Input.GetKey("a"); // press a/A to run 
            crouchPress = Input.GetKey(KeyCode.W);

            joyCrouchButton.gameObject.SetActive(true); 
            joyJumpButton.gameObject.SetActive(true); //================================== Confusion cousin de paul. ce boutton donne la valeur  du spacePress l75 a corriger //=========
            joystick.gameObject.SetActive(true);
        }
        

        transform.Rotate(Vector3.up * horizontalPress * Time.deltaTime * 50);

        if (_cc.isGrounded)
        {
            float speed;
            if (crouchPress)
            {
                isCrouched = true;
                PlayAnim(CrouchedIdleString);
            }
            else
            {
                isCrouched = false;
            }

            if (verticalPress == 0 && !isCrouched)
                PlayAnim(idleString);

            if (verticalPress != 0)
            {
                if (isCrouched)
                {
                    PlayAnim(CrouchedWalkString);

                    speed = crouchedSpeed;
                }
                else if (Input.GetKey(KeyCode.LeftShift))
                {
                    PlayAnim(runString);
                    speed = runSpeed;
                }
                else
                {
                    if (!isAudioPlayed)
                    {
                        isAudioPlayed = true;
                        PlayAudio(stepSound);
                    }
                    
                    PlayAnim(walkString);
                    speed = walkSpeed;
                }

                _moveDirection = new Vector3(0, 0, verticalPress) * speed;
                _moveDirection = transform.TransformDirection(_moveDirection);
            }
            
            else
            {
                _moveDirection.z = 0;
                _moveDirection.x = 0;

                isAudioPlayed = false;
                StopAudio(stepSound);

            }

            if (spacePress)
            {
                _moveDirection.y = jumpSpeed;
                PlayAnim(jumpString);
                
                if (!isAudioPlayed)
                {
                    PlayAudio(jumpound);
                }
            }
        }
        else
        {
            _moveDirection.y -= gravity * Time.deltaTime;
        }

        _cc.Move(_moveDirection * Time.deltaTime);
    }


    public void PlayAnim(string name)
    {
        _anim.SetBool(runString, false);

        _anim.SetBool(jumpString, false);

        _anim.SetBool(CrouchedIdleString, false);
        _anim.SetBool(CrouchedWalkString, false);

        _anim.SetBool(walkString, false);

        _anim.SetBool(name, true);
    }


    public void PlayAudio(string name)
    {
        if (audioManager)
            audioManager.Play(name);
        
    }
    
    public void StopAudio(string name)
    {
        if (audioManager)
            audioManager.Pause(name);
    }
}