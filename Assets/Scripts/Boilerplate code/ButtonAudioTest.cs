using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAudioTest : MonoBehaviour
{
    public void WhenClicked()
    {
        GetComponent<FMODUnity.StudioEventEmitter>().Play();
        Debug.Log("clicked");
    }

    private void Start()
    {
        StartCoroutine(Delay(5f));
    }

    IEnumerator Delay(float t)
    {
        yield return new WaitForSeconds(t);
        GetComponent<FMODUnity.StudioEventEmitter>().Play();
    }
}
