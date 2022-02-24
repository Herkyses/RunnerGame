using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelData currentPortalData;
    public Answer[] answers;
    public string[] processSymbols;
    public List<string> processSymbol = new List<string>();
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
            
            if (answer.answerPortal == PortalType.Divide)
            {
                processSymbol.Add("/");
            }
            else if (answer.answerPortal == PortalType.Minus)
            {
                processSymbol.Add("-");
            }
            else if (answer.answerPortal == PortalType.Multiply)
            {
                processSymbol.Add("x");
            }
            else if (answer.answerPortal == PortalType.Plus)
            {
                processSymbol.Add("+");
            }

            var processSymbolValue = processSymbol[i];
            answer.GetComponent<TextMesh>().text = processSymbolValue + answer.answerNumber.ToString();
        }
    }
}
