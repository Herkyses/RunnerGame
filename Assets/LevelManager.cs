using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelData currentPortalData;

    public Answer[] answers;
    private void Start()
    {
        var portals = currentPortalData.portals;
        
        Debug.Assert(answers.Length == portals.Count, "Portals and answers must be the same count");

        for (int i = 0; i < answers.Length; i++)
        {
            var answer = answers[i];

            var curPortal = portals[i];

            answer.answerNumber = curPortal.processValue;
            answer.answerPortal = curPortal.portalPortalType;

            answer.GetComponent<TextMesh>().text = answer.answerNumber.ToString();
        }
    }
}
