using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Script keeps track of level progress. 
/// Checks if the player has won.
/// </summary>
public class GameManager : MonoBehaviour
{

    [Tooltip("Set this equal to how many enemies are gonna spawn in the level")]
    [SerializeField]
    int maxEnemies = default;

    int enemiesLeft;




    private void Start() 
    {
        enemiesLeft = maxEnemies;    
    }

    public int GetMaxEnemies() {
        return maxEnemies;
    }
    public int GetEnemiesLeft() {
        return enemiesLeft;
    }

    public void IncrementEnemyCounter() {
        enemiesLeft++;
        if (enemiesLeft > maxEnemies) enemiesLeft = maxEnemies;



    }

    public void DecrementEnemyCounter() {
        enemiesLeft--;
        if (enemiesLeft < 0) enemiesLeft = 0;

        if(enemiesLeft == 0) 
        {
            //Win condition
            //1.Stop players movement.
            //2.Victory screen UI
            //3.Play Victory music
            Debug.Log("You have won");
        }
    }
}
