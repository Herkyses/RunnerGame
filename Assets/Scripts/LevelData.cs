using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public enum PortalType
{
    Divide,
    Plus,
    Minus,
    Multiply
};

[CreateAssetMenu(fileName = "New Level Data",menuName = "Level Data")]

public class LevelData : ScriptableObject
{
    public List<PortalValue> portals = new List<PortalValue>();
    public static int pickCountValue;
    public List<string> processSymbol;
    public static int ProcessControl(PortalType selectPortalType,int processValue)
    {
        pickCountValue = Human.Instance.pick.Count;
        
        int value=0;
        string processSymbol;
        if (selectPortalType == PortalType.Divide)
        {
            value = (Human.Instance.pick.Count) / processValue;
        }
        else if (selectPortalType == PortalType.Minus)
        {
            value = Human.Instance.pick.Count  - processValue;
            if (value < 0)
            {
                value = 0;
                Human.Instance.GetComponent<Animator>().SetBool("return",true);
                Human.Instance.GetComponent<Animator>().SetBool("pick",false);
            }
                
        }
        else if (selectPortalType == PortalType.Multiply)
        {
            value = Human.Instance.pick.Count  * processValue;
        }
        else if (selectPortalType == PortalType.Plus)
        {
            value = Human.Instance.pick.Count + processValue;
            Human.Instance.GetComponent<Animator>().SetBool("return",false);
            Human.Instance.GetComponent<Animator>().SetBool("pick",true);
        }
        
        return value;

    }

    
}

[System.Serializable]
public class PortalValue
{
    public GameObject portalObject;
    public int processValue;
    public PortalType portalPortalType;
    public string processSymbol;
    public int portalNumber;
}


