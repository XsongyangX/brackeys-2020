using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Script controlling the player movement
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    // Reference to the CharacterController
    [SerializeField] private CharacterController characterController = default;
    [SerializeField] private Camera mainCamera = default;
    // Base movement speed
    [SerializeField] private float movementSpeed = default;
    // Movement speed multiplier (used to run or crouch)
    [SerializeField] private float movementSpeedMultiplier = default;
    // Gravity's force
    [SerializeField] private float gravityForce = -9.81f;

    // Stores the velocity of the player used to apply gravity to the player
    private Vector3 playerVelocity;
    // Stores the inputs from the InputSystem
    private Vector2 inputs;

    /// <summary>
    /// Fixed update may cause stagger issues.
    /// Update is usually recommended for non-physics interactions.
    /// Character controller is not a physics-based component like rigid body
    /// </summary>
    private void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// Moves the player accordingly to the given inputs and applyes the gravity
    /// </summary>
    private void Move()
    {
        // If we're grounded and the y force is less than 0, then set it back to 0f
        if (characterController.isGrounded && playerVelocity.y < 0f)
        {
            playerVelocity.y = 0f;
        }

        // Get the forward direction for the camera
        Vector3 forward = mainCamera.transform.forward;
        // Remove the y component (we don't want the player to face down like the camera)
        forward.y = 0f;

        // Apply the new forward direction to the transform
        transform.forward = forward;

        // Create the forward movement using the user inputs and the forward direction
        Vector3 forwardMovement = transform.forward * inputs.y;
        // Create the right movement using the user inputs and the right direction
        Vector3 rightMovement = transform.right * inputs.x;

        // Create the movement direction using the normalized directions
        Vector3 movement = (forwardMovement + rightMovement).normalized * movementSpeed * movementSpeedMultiplier * Time.deltaTime;

        // Move the object
        characterController.Move(movement);
        
        ApplyGravity();
    }

    /// <summary>
    /// Applies the gravity force to the object
    /// </summary>
    private void ApplyGravity()
    {
        // Add the gravity force (negative) to the y velocity
        playerVelocity.y += gravityForce * Time.deltaTime;
        // Apply the gravity to the object
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    /// <summary>
    /// Event called whenever a Move button (WASD) is pressed
    /// </summary>
    /// <param name="context">The input data</param>
    public void OnPlayerInputMove(InputAction.CallbackContext context)
    {
        // this may need context checks 
        inputs = context.ReadValue<Vector2>();
    }
}
