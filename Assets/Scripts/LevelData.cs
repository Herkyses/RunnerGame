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
    public GameObject portalPrefab;
    //public List<PlatformData> platforms;
    public GameObject platformPrefab; //TODO: @incomplete add level platforms as prefab 
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

    public Answer[] InstantiateAnswers()
    {
        var list = new List<Answer>();
        var parentTransform = GameObject.Find("Root").transform;
        
        for (int i = 0; i < portals.Count; i++)
        {
            var portal = GameObject.Instantiate(portalPrefab, parentTransform, false);//Quaternion.identity);
            var answer = portal.GetComponent<Answer>();
            portal.transform.localPosition = portals[i].portalPosition;
            list.Add(answer);
        }

        return list.ToArray();
    }
}

[System.Serializable]
public class PortalValue
{
    public Vector3 portalPosition;
    public int processValue;
    public PortalType portalPortalType;
    public string processSymbol;
    public int portalNumber;
}


