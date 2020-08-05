using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic Inventory System
/// </summary>
public class PlayerInventory : MonoBehaviour
{
    // Wheter the player has a tape in hands
    public bool HasTapeInHands { get; private set; }
    // Reference to the monster linked to the tape that we have in hands
    public MonsterAI MonsterLinkedToTape { get; private set; }

    /// <summary>
    /// Sets HasTapeInHands to true and stores the reference to the monster
    /// </summary>
    /// <param name="monster">The monster linked to the tape</param>
    public void PickupTape(MonsterAI monster)
    {
        HasTapeInHands = true;
        MonsterLinkedToTape = monster;
    }
}
