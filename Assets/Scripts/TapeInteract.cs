using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script should be added on the PlayerControler Object.
/// Usess raycasts to see whats in front of the player, to be interacted with.
/// Shows tooltip if Player can interact with object
/// </summary>


public class TapeInteract : MonoBehaviour {
    [Tooltip("Add the playerObject here")]
    [SerializeField]
    GameObject playerObject;

    [Tooltip("Set the distance of the raycast for interacting with tapes(Should be arms lenght)")]
    [SerializeField]
    float distanceToInteract;


    float playerReach = 1;

    //TEMP VARS

    RaycastHit hit;
    Vector3 playerForwardDirection;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        ShootRaycast();

    }

    void ShootRaycast() {

        playerForwardDirection = playerObject.transform.TransformDirection(Vector3.forward);


        if (Physics.BoxCast(playerObject.transform.position, new Vector3(playerReach, playerReach), playerForwardDirection, out hit, Quaternion.identity, distanceToInteract)) {
            //do something if hit object ie
            Debug.Log(hit.collider.gameObject);



        }
    }

    /// <summary>
    /// This can be disabled, used to just show how the player finds objects and
    /// where is the player looking in a given time.
    /// </summary>
    void OnDrawGizmos() {

        Gizmos.DrawRay(playerObject.transform.position, playerForwardDirection * hit.distance);

        Gizmos.DrawWireCube(playerObject.transform.position + playerForwardDirection * hit.distance, new Vector3(playerReach, playerReach));
    }
}

