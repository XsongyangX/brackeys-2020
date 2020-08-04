using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the tape mechanics and interacts with the linked monster
/// </summary>
public class TapeManager : MonoBehaviour
{
    // Reference to the Monster linked to this tape
    [SerializeField] private MonsterAI monsterAI = default;

    // Called whenever we interact with the tape
    public void Interact()
    {
        monsterAI?.OnTapeInteract();
    }
}
