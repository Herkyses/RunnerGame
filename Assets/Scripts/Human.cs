using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    private Animator thisAnimator;
    float positionValue;
    public Camera cam;
    private Touch touch;
    
    private Vector3 inputMouse;
    private Vector2 startPos;
    private Vector3 debugPos;
    
    public List<GameObject> pick;
    public List<GameObject> images;
    private Vector3 firstTouch, lastTouch;
    private Vector3 particlePosition;
    private Vector3 particlePosition2,particlePosition3,particlePosition4;
    private Vector3 camPosition;
    HumanParent parentSpeed;

    public float swipeSpeed = 15;
    public GameObject pickParent;
    public GameObject pickObject;
    public GameObject tryAgainPanel;
    public GameObject finishPanel;
    public GameObject backButton;
    public GameObject camFollow;
    public ParticleSystem victoryParticle;
    public ParticleSystem victoryParticle2;
    public ParticleSystem victoryParticle3;
    public ParticleSystem victoryParticle4;

    private float pickCubePosition = 0;
    private int count = 0;
    private int v;
    private int imageCount;
    public float stairsCount = 0;
    public float stairsCount2 = 0;
    
    public bool touched = false;
    public bool camMove = false;
    public bool stairsUp = false;
    private bool finishControl = false;
    private bool answerControl = false;
    // Start is called before the first frame update
    void Start()
    {
        thisAnimator = gameObject.GetComponent<Animator>();
        parentSpeed = GameObject.FindObjectOfType<HumanParent>();
        particlePosition = victoryParticle.transform.position;
        particlePosition2 = victoryParticle2.transform.position;
        particlePosition3 = victoryParticle3.transform.position;
        particlePosition4 = victoryParticle4.transform.position;
        victoryParticle.transform.position = new Vector3(10f, 10f, 10f);
        victoryParticle2.transform.position = new Vector3(10f, 10f, 10f);
        victoryParticle3.transform.position = new Vector3(10f, 10f, 10f);
        victoryParticle4.transform.position = new Vector3(10f, 10f, 10f);
        victoryParticle.Pause();
        victoryParticle2.Pause();
        victoryParticle3.Pause();
        victoryParticle4.Pause();
        imageCount = 0;
        cam = Camera.main;
        pick = new List<GameObject>();
        
        Debug.Log("fark" + (cam.transform.position.y - transform.position.y));
    }
    void Update() {
        
        if(stairsUp == true) {

            cam.transform.position = Vector3.Lerp(cam.transform.position,new Vector3(cam.transform.position.x, camPosition.y ,cam.transform.position.z),.2f*Time.deltaTime);
            //camPosition = cam.transform.position;
        }
        Debug.Log("listedeki eleman say�s�"+pick.Count);
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

    
    
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "pickup") {

            pickCubePosition += 0.2f;
            count++;
            //Debug.Log("selam");
            thisAnimator.SetBool("pick", true);
            thisAnimator.SetBool("return", false);
            other.gameObject.transform.SetParent(pickParent.transform);
            other.transform.localPosition = new Vector3(0, pickCubePosition, 0);
            //ParticleSystem pickSystem = Instantiate(victoryParticle2, other.transform);
            other.gameObject.GetComponent<Collider>().enabled = false;
            pick.Add(other.gameObject);

            //Debug.Log(count);
            
        }
        if(other.tag == "answer1") {
            //Debug.Log("sea");
            
            for(int j = 0; j < 3; j++) {
                pickCubePosition += 0.2f;
                GameObject newObject = Instantiate(pickObject);
                thisAnimator.SetBool("pick", true);
                thisAnimator.SetBool("return", false);
                newObject.gameObject.transform.SetParent(pickParent.transform);
                newObject.transform.localPosition = new Vector3(0, pickCubePosition, 0);
                newObject.GetComponent<Collider>().enabled = false;
                pick.Add(newObject);
                
                //Destroy(other.GetComponentInParent<GameObject>());
                //Debug.Log(pick.Count);

            }
            
            if(imageCount < 4 ) {
                images[imageCount].SetActive(false);
                imageCount++;
            }
            other.transform.parent.gameObject.SetActive(false);

        }
        else if(other.tag == "answer2") {
            other.gameObject.GetComponentInParent<Transform>().position = new Vector3(0, 0, 0);
            if(pick.Count >= 3) {
                
                for(int i = 1; i < 4; i++) {
                    pick[pick.Count - 1].SetActive(false);
                    pick[pick.Count - 1].transform.SetParent(null);
                    pick.Remove(pick[pick.Count - 1]);
                    if(pick.Count == 0) {
                        thisAnimator.SetBool("return", true);
                        thisAnimator.SetBool("pick", false);
                    }
                    pickCubePosition -= 0.2f;
                }
                            
            }
            if(pick.Count == 0) {
                thisAnimator.SetBool("return", true);
                thisAnimator.SetBool("pick", false);
                Debug.Log("listeBo�");
            }

            if(imageCount < 4) {
                images[imageCount].SetActive(false);
                imageCount++;
            }
            other.transform.parent.gameObject.SetActive(false);

        }
        else if(other.tag == "stairs") {
            if(touched == false) {
                if(pick.Count > 7) {
                    v = 7;
                }
                else {
                    v = pick.Count;
                }
                touched = true;
            }
                
            for (int c = 0; c < v; c++) {

                Debug.Log("passionpunch" + pick.Count);
                stairsCount += .3f;
                pick[pick.Count - 1].tag = "Untagged";
                pick[pick.Count - 1].transform.SetParent(null);
                pick[pick.Count - 1].transform.position = new Vector3(transform.position.x,transform.position.y + 1 + stairsCount-stairsCount2-1.4f, transform.position.z+stairsCount);
                pick[pick.Count - 1].GetComponent<Collider>().enabled = true;
                pick[pick.Count - 1].GetComponent<Collider>().isTrigger = false;
                pick.Remove(pick[pick.Count - 1]);
                stairsCount2 += 0.2f;
                pickCubePosition -= 0.2f;
                
                if(pick.Count == 0) {
                    gameObject.GetComponent<Animator>().SetBool("return", true);
                    gameObject.GetComponent<Animator>().SetBool("pick", false);
                    Debug.Log("listeBo�");
                }
            }
            camPosition = cam.transform.position + new Vector3(0, .35f, .1f);
            //cam.transform.position += new Vector3(0, .3f, -.2f);
            stairsUp = true;
            stairsCount = 0;
            stairsCount2 = 0;
            touched = false;
        }
        else if(other.tag == "finishAnim") {
            finishControl = true;
            parentSpeed.speed = 0f;
            backButton.SetActive(false);
            foreach(GameObject cube in pick) {
                if(cube.activeSelf == true)
                    cube.SetActive(false);
            }
            thisAnimator.SetBool("victory", true);
            victoryParticle.transform.position = particlePosition;
            victoryParticle2.transform.position = particlePosition2;
            victoryParticle3.transform.position = particlePosition3;
            victoryParticle4.transform.position = particlePosition4;
            victoryParticle.Play();
            victoryParticle2.Play();
            victoryParticle3.Play();
            victoryParticle4.Play();
            finishPanel.SetActive(true);
        }

    }
    private void OnCollisionEnter(Collision collision) {
       if(collision.collider.tag == "finish") {
            backButton.transform.localScale = new Vector3(0, 0, 0);
            tryAgainPanel.SetActive(true);
            finishControl = true;
            parentSpeed.speed = 0f;
            collision.collider.GetComponent<Collider>().enabled = false;
            foreach(GameObject cube in pick) {
                if(cube.activeSelf == true)
                    cube.SetActive(false);
            }
        }
    }


}
