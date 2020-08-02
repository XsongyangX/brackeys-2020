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

        // Create the movement struct using the user inputs
        Vector3 movement = new Vector3(inputs.x, 0, inputs.y);
        
        // If the movement isn't null, face towards the movement direction
        if (movement != Vector3.zero)
        {
            transform.forward = movement;
        }

        // Move the object
        characterController.Move(movement * Time.deltaTime * movementSpeed * movementSpeedMultiplier);
        
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
        inputs = context.ReadValue<Vector2>();
    }
}
