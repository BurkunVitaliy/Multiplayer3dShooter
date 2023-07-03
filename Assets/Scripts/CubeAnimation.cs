using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class CubeAnimation : MonoBehaviour
{
    private Animation anim;
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
        anim.Play("CubeAnimation", PlayMode.StopAll);
        //anim.PlayQueued("CubeAnimation", QueueMode.PlayNow);
        
    }

    void DisplayInfo(string words)
    {
        Debug.Log(words);
    }

    
}
