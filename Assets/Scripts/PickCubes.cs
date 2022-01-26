using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickCubes : MonoBehaviour
{
    private Human playerObject;   
  
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            playerObject = other.GetComponent<Human>();
            playerObject.pickCubePosition += 0.2f;
            transform.SetParent(playerObject.pickParent.transform);
            playerObject.thisAnimator.SetBool("pick", true);
            playerObject.thisAnimator.SetBool("return", false);
            transform.localPosition = new Vector3(0, playerObject.pickCubePosition, 0);
            gameObject.GetComponent<Collider>().enabled = false;
            playerObject.pick.Add(gameObject);
            
        }
    }
}
