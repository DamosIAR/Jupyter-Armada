using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    private PlayerControl playerInput;

    private void Awake()
    {
        playerInput = new PlayerControl();
        controller =GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
         playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }


    

    private void Start()
    {

    }

    void Update()
    {
        /*groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }*/

        Vector2 MovementInput = playerInput.Player.Move.ReadValue<Vector2>();
        Vector2 move = new Vector2(MovementInput.x, MovementInput.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        /*if (move != Vector2.zero)
        {
            gameObject.transform.forward = move;
        }*/

        // Makes the player jump
        /*if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }*/

        //playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
