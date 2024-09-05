using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Transform References")]
    [SerializeField] private Transform movementTrans;
    [SerializeField] private Transform modelTrans;

    [Header("Movement")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float gravitationalAcceleration;
    [SerializeField] private float jumpForce;

    [Header("Rotation")]
    [SerializeField][Range(0f, 5f)] private float rotationSpeed;

    [Header("animation")]
    [SerializeField] private Animator playerAni;

    [Header("Debug")]
    [SerializeField] private bool isGround;


    private float horizontalInput;
    private float verticalInput;
    private bool jumpFlag;
    private bool runFlag;

    private CharacterController m_characterController;
    private PlayerGorundCheck m_groundChecker;
    private Vector3 velocity;
    private Vector3 lastFixedPosition;
    private Quaternion lastFixedRotation;
    private Vector3 nextFixedPosition;
    private Quaternion nextFixedRotation;



    // Start is called before the first frame update
    void Start()
    {
        m_characterController = GetComponent<CharacterController>();
        m_groundChecker = GetComponent<PlayerGorundCheck>();
        velocity = new Vector3(0, 0, 0);
        lastFixedPosition = transform.position;
        lastFixedRotation = transform.rotation;
        nextFixedPosition = transform.position;
        nextFixedRotation = transform.rotation;

        horizontalInput = 0.0f;
        verticalInput = 0.0f;
        jumpFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
            jumpFlag = true;
        if (Input.GetKeyDown(KeyCode.LeftShift))
            runFlag = true;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            runFlag = false;

        if (Input.GetKey(KeyCode.Q))
            RotatePlayer(-rotationSpeed);
        if (Input.GetKey(KeyCode.E))
            RotatePlayer(rotationSpeed);

        modelTrans.localRotation = Quaternion.Euler(0, Mathf.Atan2(horizontalInput, verticalInput) * Mathf.Rad2Deg, 0);

        float interpolationAlpha = (Time.time - Time.fixedTime) / Time.fixedDeltaTime;
        m_characterController.Move(Vector3.Lerp(lastFixedPosition, nextFixedPosition, interpolationAlpha) - transform.position);
    }

    private void FixedUpdate()
    {
        Vector3 ResultVelocity = this.transform.position;

        ResultVelocity.x = Mathf.Abs(ResultVelocity.x - nextFixedPosition.x) > 0 ? Mathf.Lerp(ResultVelocity.x, nextFixedPosition.x, 0.5f) : nextFixedPosition.x;
        ResultVelocity.y = Mathf.Abs(ResultVelocity.y - nextFixedPosition.y) > 0 ? Mathf.Lerp(ResultVelocity.y, nextFixedPosition.y, 0.5f) : nextFixedPosition.y;
        ResultVelocity.z = Mathf.Abs(ResultVelocity.z - nextFixedPosition.z) > 0 ? Mathf.Lerp(ResultVelocity.z, nextFixedPosition.z, 0.5f) : nextFixedPosition.z;

        nextFixedPosition = ResultVelocity;
        lastFixedPosition = nextFixedPosition;

        Vector3 InputVelocity = GetXZVelocity(horizontalInput, verticalInput);

        if (InputVelocity.magnitude != 0)
        {
            if (runFlag)
            {
                playerAni.SetBool("run", true);
                playerAni.SetBool("walk", false);
            }
            else
            {
                playerAni.SetBool("walk", true);
                playerAni.SetBool("run", false);
            }
        }
        else
        {
            playerAni.SetBool("walk", false);
            playerAni.SetBool("run", false);
        }

        float yVelocity = GetYVelocity();
        velocity = new Vector3(InputVelocity.x, yVelocity, InputVelocity.z);

        nextFixedPosition += velocity * Time.fixedDeltaTime;
    }

    private Vector3 GetXZVelocity(float HorizontalInput, float VerticalInput)
    {
        Vector3 MoveVelocity = movementTrans.forward * VerticalInput + movementTrans.right * HorizontalInput;
        Vector3 MoveDirection = MoveVelocity.normalized;
        float Speed = runFlag ? runSpeed : walkSpeed;
        float MoveSpeed = Mathf.Min(MoveVelocity.magnitude, 1.0f) * Speed;

        return MoveDirection * MoveSpeed;
    }

    /// <summary>
    /// 점프판정 체크
    /// </summary>
    /// <returns></returns>
    private float GetYVelocity()
    {
        if (isGround = !m_groundChecker.IsGrounded()) // 땅에 닿아있지 않는경우
        {
            return velocity.y - gravitationalAcceleration * Time.fixedDeltaTime;
        }

        if (jumpFlag) // 점프를 시전했을때
        {
            jumpFlag = false;
            return velocity.y + jumpForce;
        }
        else
        {
            return Mathf.Max(0.0f, velocity.y);
        };
    }

    private void RotatePlayer(float rotationAmount)
    {
        Vector3 rotation = Vector3.up * rotationAmount * 100 * Time.deltaTime;

        m_characterController.transform.Rotate(rotation);
    }

    private int ReturnRot(float InputValue)
    {
        if (InputValue < -0.1)
            return -1;
        else if (InputValue > 0.1)
            return 1;
        return 0;
    }
}
