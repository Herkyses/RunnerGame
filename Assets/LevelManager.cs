using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public LevelData[] allLevels;
    public LevelData currentPortalData;
    public Answer[] answers;
    public List<string> processSymbol = new List<string>();
    public List<GameObject> levels = new List<GameObject>();

    private void Awake()
    {
        Time.timeScale = 0f;
    }

    public void SetLevel(int index)
    {
        Instantiate(levels[index]);
        
        currentPortalData = allLevels[index];
        
        answers = currentPortalData.InstantiateAnswers();
        
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
            answer.GetComponentInChildren<TextMesh>().text = processSymbolValue + answer.answerNumber.ToString();
        }
    }
}
