using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: Singleton<T>
{
    private static T instance = null;
    private bool full = false;
    public static List<GameObject> playerObjects;
    public static int tryNumber =0;
    // Start is called before the first frame update

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                
                instance = FindObjectOfType(typeof(T)) as T;
                if (instance == null)
                {
                    GameObject newObject = new GameObject();
                    newObject.name = typeof(T).Name;
                    instance = newObject.AddComponent<T>();
                }
                
            }
                instance = FindObjectOfType(typeof(T)) as T;


               
            return instance;
        }
        
    }

    public void Awake()
    {
        
        
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
            
            
        }
        
        
        
           
        else 
        {   
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        Debug.Log("trd"+tryNumber);
    }
}
