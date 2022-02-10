using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Answer : MonoBehaviour
{
   
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
        bool answerIsTrue = gameObject.tag == "true";
        pickAnimatorControl = answerIsTrue;
        returnAnimatorControl = !answerIsTrue;
        cubePosition = answerIsTrue ?  0.2f : -0.2f;
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerObject = other.GetComponent<Human>();
            if (gameObject.tag == "true")
            {
                GameManager.OnAnswerControl.Invoke(cubePosition);
                for (int j = 0; j < 3; j++)
                {
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
                    GameManager.OnAnswerControl.Invoke(cubePosition);
                    
                    for (int i = 1; i < 4; i++)
                    {
                        
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
