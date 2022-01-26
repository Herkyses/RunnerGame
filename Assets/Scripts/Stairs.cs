using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    private Human playerObject;
    public GameObject pickDownParent;
    private int stairsCubeCount;
    private float stairsCount = 0;
    private float stairsCount2 = -.2f;
    
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerObject = other.GetComponent<Human>();
            //GameObject playerObjectPick = playerObject.pick[playerObject.pick.Count - 1];
            if(playerObject.touched == false) {
                if(playerObject.pick.Count > 7) {
                    stairsCubeCount = 7;
                }
                else {
                    stairsCubeCount = playerObject.pick.Count;
                }
                playerObject.touched = true;
            }
                
            for (int c = 0; c < stairsCubeCount; c++) 
            {
                GameObject playerObjectPick = playerObject.pick[playerObject.pick.Count - 1];
                stairsCount += .3f;
                stairsCount2 += .1f;
                playerObjectPick.tag = "Untagged";
                playerObjectPick.transform.SetParent(null);
                playerObjectPick.transform.position = new Vector3(other.transform.position.x,other.transform.position.y + stairsCount2, other.transform.position.z+stairsCount);
                playerObjectPick.transform.SetParent(pickDownParent.transform);
                playerObjectPick.GetComponent<Collider>().enabled = true;
                playerObjectPick.GetComponent<Collider>().isTrigger = false;
                playerObject.pick.Remove(playerObjectPick);
                playerObject.pickCubePosition -= 0.2f;
                
                if(playerObject.pick.Count == 0) {
                    playerObject.GetComponent<Animator>().SetBool("return", true);
                    playerObject.GetComponent<Animator>().SetBool("pick", false);
                }
            }
            playerObject.camPosition = playerObject.cam.transform.position + new Vector3(0, .35f, .1f);
            //cam.transform.position += new Vector3(0, .3f, -.2f);
            playerObject.stairsUp = true;
            stairsCount = 0;
            stairsCount2 = -0.2f;
            playerObject.touched = false;
        }
    }
}
