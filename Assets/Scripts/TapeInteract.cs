﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

/// <summary>
/// This script should be added on the PlayerControler Object or the game manager.
/// Uses raycasts to see what's in front of the player, to be interacted with.
/// Shows tooltip if Player can interact with object
/// 
/// Requires: The tape object tag to be set to "Tape"
/// </summary>


public class TapeInteract : MonoBehaviour
{
    [Tooltip("Add the playerObject here")]
    [SerializeField]
    private GameObject playerObject = default;

    [Tooltip("Set the distance of the raycast for interacting with tapes(Should be arms lenght)")]
    [SerializeField]
    private float distanceToInteract = default;

    [SerializeField]
    private GameObject pressEToolTip = default;

    private float playerReach = 1;

    private RaycastHit hit;
    private Vector3 playerForwardDirection;

    /// <summary>
    /// The tape found by raycast, otherwise null
    /// </summary>
    private TapeManager collidedTape;

    // Start is called before the first frame update
    void Start()
    {
        if (pressEToolTip == null)
        {
            //Look up Canvas, and then the pressE Text/Game Object 
            pressEToolTip = GameObject.Find("Canvas").transform.Find("PressE").gameObject;

        }

        pressEToolTip.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ShootRaycast();
    }

    void ShootRaycast()
    {

        //get player forward direction
        playerForwardDirection = playerObject.transform.forward;

        //Cast a box ray in front of the player too look for tapes to interact
        bool foundTape = Physics.BoxCast(playerObject.transform.position, new Vector3(playerReach, playerReach), playerForwardDirection, out hit, Quaternion.identity, distanceToInteract);
        if (foundTape && hit.collider.gameObject.CompareTag("Tape"))
        {
            if (pressEToolTip.activeSelf == false)
                pressEToolTip.SetActive(true);

            collidedTape = hit.collider.gameObject.GetComponent<TapeManager>();
        }
        else
        {
            if (pressEToolTip.activeSelf == true)
                pressEToolTip.SetActive(false);

            collidedTape = null;
        }
    }

    /// <summary>
    /// This can be disabled, used to just show how the player finds objects and
    /// where is the player looking in a given time.
    /// </summary>
    void OnDrawGizmos()
    {

        Gizmos.DrawRay(playerObject.transform.position, playerForwardDirection * hit.distance);

        Gizmos.DrawWireCube(playerObject.transform.position + playerForwardDirection * hit.distance, new Vector3(playerReach, playerReach));
    }

    /// <summary>
    /// Listener to the Interaction action in input system
    /// </summary>
    /// <param name="ctx"></param>
    public void Interaction(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && collidedTape != null) TakeTape();
    }

    /// <summary>
    /// Take the tape, triggers an interaction with the tape in range
    /// </summary>
    private void TakeTape()
    {
        Debug.Log("Interact with tape");
        collidedTape.Interact();
    }
}
