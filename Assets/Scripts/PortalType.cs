using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public enum Type
{
    Divide,
    Plus,
    Minus,
    Multiply
};

[CreateAssetMenu(fileName = "New Level Data",menuName = "Level Data")]

public class PortalType : ScriptableObject
{
    public List<PortalValue> portals = new List<PortalValue>();
    
}

[System.Serializable]
public class PortalValue
{
    public GameObject portalObject;
    public int processValue;
    public Type portalType;
    
    public int portalNumber;
    
    //public PortalValue(GameObject _portalObject, int _processValue,Type _portalType)
    //{
    //    _portalObject = portalObject;
    //    _processValue = processValue;
    //    _portalType = portalType;
    //}

    public void CallProcess()
    {
        ProcessControl(portalType);
    }
    public void ProcessControl(Type selectType)
    {
        int value;
        if (selectType == Type.Divide)
        {
            value = Human.Instance.pick.Count / processValue;
            Debug.Log("selam"+value);
            ReturnValue(value);
        }
        else if (selectType == Type.Minus)
        {
            value = Human.Instance.pick.Count - processValue;
            Debug.Log("seladsam"+value);
            ReturnValue(value);
        }
        else if (selectType == Type.Multiply)
        {
            Debug.Log("esaasm"+Human.Instance.pick.Count);
            value = Human.Instance.pick.Count * processValue;
            ReturnValue(value);
        }
        else if (selectType == Type.Plus)
        {
            Debug.Log("qwesd"+Human.Instance.pick.Count);
            value = Human.Instance.pick.Count + processValue;
            ReturnValue(value);
        }
    }

    public void ReturnValue(int returnValue)
    {
        
    }
    
}


