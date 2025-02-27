using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Answer : MonoBehaviour
{
    private Transform parentTransform;
    private Human playerObject;
    private Stairs stairObject;
    private bool pickAnimatorControl;
    private bool returnAnimatorControl;
    private float cubePosition;
    public int answerNumber;
    public int pickValue;
    public PortalType answerPortal;
    // Start is called before the first frame update
    void Start()
    {
        bool answerIsTrue = gameObject.tag == "true";
        pickAnimatorControl = answerIsTrue;
        returnAnimatorControl = !answerIsTrue;
        cubePosition = answerIsTrue ?  0.2f : -0.2f;
        //PortalProcess();
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            int result = LevelData.ProcessControl(answerPortal, answerNumber);
            
            GameManager.OnAnswerControl(result);
            Debug.Log("result"+result);
            //Debug.Log("result2"+result2);
        }
    }

    public void AnswerControl()
    {
        playerObject.GetComponent<Animator>().SetBool("pick",pickAnimatorControl);
        playerObject.GetComponent<Animator>().SetBool("return",returnAnimatorControl);
    }

    public void PortalProcess()
    {
        //portalTypes.processType += portalValue;
        //portalTypes.UsePortalValue();
    }

    
}
