using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answer : MonoBehaviour
{
    public GameObject pickCubes;
    public GameObject cubeFalseParent;
    private Transform parentTransform;
    private Human playerObject;
    private Stairs stairObject;

    private bool pickAnimatorControl;
    private bool returnAnimatorControl;

    private float cubePosition;
    // Start is called before the first frame update
    void Start()
    {
        
        if (gameObject.tag == "true")
        {
            //parentTransform = playerObject.pickParent.transform;
            pickAnimatorControl = true;
            returnAnimatorControl = false;
            cubePosition = 0.2f;
        }
        else if (gameObject.tag == "false")
        {
            //parentTransform = playerObject.pickDownParent.transform;
            pickAnimatorControl = false;
            returnAnimatorControl = true;
            cubePosition = -0.2f;
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerObject = other.GetComponent<Human>();
            if (gameObject.tag == "true")
            {
                for (int j = 0; j < 3; j++)
                {
                    
                    playerObject.pickCubePosition += cubePosition;
                    GameObject newObject = Instantiate(pickCubes,playerObject.pickParent.transform);
                    //newObject.transform.SetParent(playerObject.pickParent.transform);
                    newObject.transform.localPosition = new Vector3(0, playerObject.pickCubePosition, 0);
                    newObject.GetComponent<Collider>().enabled = false;
                    playerObject.pick.Add(newObject);

                    if (j == 1)
                    {

                        AnswerControl();

                    }

                }


                transform.parent.gameObject.SetActive(false);
            }
            else if (gameObject.tag == "false")
            {
                gameObject.GetComponentInParent<Transform>().position = new Vector3(0, 0, 0);

                if (playerObject.pick.Count >= 3)
                {
                    //GameObject playerObjectPick = playerObject.pick[playerObject.pick.Count - 1];
                    for (int i = 1; i < 4; i++)
                    {
                        GameObject playerObjectPick = playerObject.pick[playerObject.pick.Count - 1];
                        playerObjectPick.SetActive(false);
                        playerObjectPick.transform.SetParent(cubeFalseParent.transform);
                        playerObject.pick.Remove(playerObjectPick);
                        playerObject.pickCubePosition += cubePosition;

                        if (playerObject.pick.Count == 0)
                        {
                            AnswerControl();
                        }

                    }

                }
                else if (playerObject.pick.Count == 0)
                {

                    AnswerControl();
                    

                }

                transform.parent.gameObject.SetActive(false);
            }
        }
    }

    public void AnswerControl()
    {
        
        playerObject.GetComponent<Animator>().SetBool("pick",pickAnimatorControl);
        playerObject.GetComponent<Animator>().SetBool("return",returnAnimatorControl);
    }
}
