using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

/// <summary>
/// This script should be added on the PlayerControler Object.
/// Usess raycasts to see whats in front of the player, to be interacted with.
/// Shows tooltip if Player can interact with object
/// 
/// Requires: The tape object tag to be set to "Tape"
/// </summary>


public class TapeInteract : MonoBehaviour 
{
    [Tooltip("Add the playerObject here")]
    [SerializeField]
    GameObject playerObject = default;

    [Tooltip("Set the distance of the raycast for interacting with tapes(Should be arms lenght)")]
    [SerializeField]
    float distanceToInteract = default;

    [SerializeField]
    GameObject pressEToolTip = default;

    float playerReach = 1;

    RaycastHit hit;
    Vector3 playerForwardDirection;

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


            if(pressEToolTip.activeSelf == false)
                pressEToolTip.SetActive(true);

            if (Keyboard.current.eKey.wasPressedThisFrame) {
                Debug.Log("Interact with tape");
            }
        }
        else 
        {
            if (pressEToolTip.activeSelf == true)
                pressEToolTip.SetActive(false);
        }
    }

    /// <summary>
    /// This can be disabled, used to just show how the player finds objects and
    /// where is the player looking in a given time.
    /// </summary>
    void OnDrawGizmos() 
    {

        Gizmos.DrawRay(playerObject.transform.position, playerForwardDirection * hit.distance);

        Gizmos.DrawWireCube(PlayerObject.transform.position + playerForwardDirection * hit.distance, new Vector3(playerReach, playerReach));
    }
}
