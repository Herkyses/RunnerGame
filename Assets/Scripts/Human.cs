using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Human : Singleton<Human>
{
    
    public Animator thisAnimator;
    float positionValue;
    public Camera cam;
    private Touch touch;
    
    private Vector3 inputMouse;
    private Vector2 startPos;
    private Vector3 debugPos;
    
    public List<GameObject> pick;
    private Vector3 firstTouch, lastTouch;
    
    public Vector3 camPosition;
    public HumanParent parentSpeed;
    public float swipeSpeed = 15;
    public GameObject pickParent;
    public GameObject pickObject;
    public GameObject camFollow;

    public float pickCubePosition = 0;
    private int count = 0;
    private int stairsCubeCount;
    public float stairsCount = 0;
    private float stairsCount2 = -.2f;
    
    public bool touched = false;
    public bool camMove = false;
    public bool stairsUp = false;
    public bool finishControl = false;
    private bool answerControl = false;
    public GameObject pickCubes;
    void Start()
    {
        
        thisAnimator = gameObject.GetComponent<Animator>();
        parentSpeed = GameObject.FindObjectOfType<HumanParent>();
        pick = new List<GameObject>();
        cam = Camera.main;

    }

    private void OnEnable()
    {
        GameManager.OnAnswerControl+=OnAnswerControl;
    }

    private void OnDisable()
    {
        GameManager.OnAnswerControl-=OnAnswerControl;
    }

    private void OnAnswerControl(int times)
    {
        float deltaValue = 0.2f;
        var counter = Mathf.Abs(times);
        //var counter = times;
        
            if (times > LevelData.pickCountValue)
            {
                for (int i = 0; i < times - LevelData.pickCountValue; i++)
                {
                    pickCubePosition += deltaValue;
                    GameObject newObject = Instantiate(pickCubes,pickParent.transform);
                    newObject.transform.localPosition = new Vector3(0, pickCubePosition, 0);
                    newObject.GetComponent<Collider>().enabled = false;
                    pick.Add(newObject);
                }
                

            }
            else if (times < LevelData.pickCountValue)
            {
                for (int j = 0; j < LevelData.pickCountValue - times;j++)
                {
                    if (pick.Count >0 )
                    {
                        GameObject playerObjectPick = pick[pick.Count - 1];
                        playerObjectPick.SetActive(false);
                        playerObjectPick.transform.SetParent(null);
                        pick.Remove(playerObjectPick);
                        pickCubePosition -= deltaValue;
                    }
                    else
                    {
                        break;
                    }
                }

            }
            
        

    }

    
    void Update() {
        
        if(stairsUp == true) {

            cam.transform.position = Vector3.Lerp(cam.transform.position,new Vector3(cam.transform.position.x, camPosition.y ,cam.transform.position.z),.2f*Time.deltaTime);
            
        }
        if(finishControl == false) {
            
            if(Input.touchCount <= 1) {
                
                if(Input.GetMouseButtonDown(0)) {

                    firstTouch = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, 0, 0));

                }
                if(Input.GetKey(KeyCode.Mouse0)) {

                    lastTouch = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, 0, 0));
                    Vector3 diff = lastTouch - firstTouch;
                    transform.position += diff * swipeSpeed * Time.deltaTime;
                    firstTouch = lastTouch;

                }

                /*
                touch = Input.GetTouch(0);
                if(touch.phase == TouchPhase.Moved) {
                    transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * Time.deltaTime*.1f, transform.position.y, transform.position.z);
                } */
            }
            
            
        }
        
        
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "sea")
        {
            GameManager.Failed.Invoke();
            CubesEmpty();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "finishAnim")
        {
            GameManager.Victory.Invoke();
            CubesEmpty();
        }
    }
    

    public void CubesEmpty()
    {
        foreach(GameObject cube in pick) {
            if(cube.activeSelf == true)
                cube.SetActive(false);
        }
    }
    

}
