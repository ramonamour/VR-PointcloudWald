using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TopDownGesture : MonoBehaviour
{
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject joinedHands;
    //[SerializeField] private VisualEffect handVFX;
    [SerializeField] private VisualEffect wald;
    [SerializeField] private AudioSource waldAudio;
    [SerializeField] private AudioSource oceanAudio;
    [SerializeField] private AudioSource oceanMelody;
    

    //private VisualEffect handvfx;
    private float movementXDirection;
    private float movementYDirection;
    private float movementZDirection;


    private float oldXPos;
    private float oldYPos;
    private float oldZPos;


    float handDistance;
    //bool joined;
    // Start is called before the first frame update
    void Start()
    {
       //handvfx = GetComponent<VisualEffect>();
        oldXPos = joinedHands.transform.position.x;
        oldYPos = joinedHands.transform.position.y;
        oldZPos = joinedHands.transform.position.z;

        movementXDirection = 0;
        movementYDirection = 0;
        movementZDirection = 0;
    }

    // Update is called once per frame
    void Update()
    {

        movementXDirection += Mathf.Abs(Mathf.Abs(oldXPos) - Mathf.Abs(joinedHands.transform.position.x));
        movementYDirection += Mathf.Abs(Mathf.Abs(oldYPos) - Mathf.Abs(joinedHands.transform.position.y));
        movementZDirection += Mathf.Abs(Mathf.Abs(oldZPos) - Mathf.Abs(joinedHands.transform.position.z));

        oldXPos = joinedHands.transform.position.x;
        oldYPos = joinedHands.transform.position.y;
        oldZPos = joinedHands.transform.position.z;

        joinedHands.transform.position = Vector3.Lerp(leftHand.transform.position,rightHand.transform.position, 0.5f);
        //Debug.Log(joinedHands.transform.position);
        //handvfx.SetVector3("joinedPosition", joinedHands.transform.position);
        //handvfx.SetVector3("leftHandPosition", leftHand.transform.position);

        handDistance = Vector3.Distance(leftHand.transform.position, rightHand.transform.position);
        //Debug.Log(handDistance);

        if (handDistance <= 0.5){
            joinedHands.SetActive(true);
            //leftHand.SetActive(false);
            //rightHand.SetActive(false);
            leftHand.GetComponent<VisualEffect>().SetBool("HandsAreJoined", true);
            rightHand.GetComponent<VisualEffect>().SetBool("HandsAreJoined", true);

            joinedHands.GetComponent<VisualEffect>().SetBool("TopDown", wald.GetBool("TopDownGesture"));
        }
        else {
            joinedHands.SetActive(false);
            //leftHand.SetActive(true);
            //rightHand.SetActive(true);

            leftHand.GetComponent<VisualEffect>().SetBool("HandsAreJoined", false);
            rightHand.GetComponent<VisualEffect>().SetBool("HandsAreJoined", false);
        }

    }

    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("Entered");
        movementXDirection = 0;
        movementYDirection = 0;
        movementZDirection = 0;
        //wald.SetBool("TopDownGesture",true);
        joinedHands.GetComponent<VisualEffect>().SetInt("TriggerBox", 1);

    }
    private void OnTriggerExit(Collider other) 
    {
        Debug.Log("Exit");
        Debug.Log("X: " + movementXDirection);
        Debug.Log("Z: " + movementZDirection);

        if (movementXDirection < .5 && movementZDirection < .5 && wald.GetBool("TopDownGesture") == false){
            wald.SetBool("TopDownGesture", true);
            wald.SetFloat("TimeAfterForestSwitch", 0f);

            StartCoroutine (AudioFade.FadeOut (waldAudio, 1f));
            StartCoroutine (AudioFade.FadeIn (oceanAudio, .5f));
            StartCoroutine (AudioFade.FadeIn (oceanMelody, 2f));
            //waldAudio.Stop();
            //oceanAudio.Play();
        }
        else if (wald.GetBool("TopDownGesture") == false) {
            StartCoroutine (AudioFade.FadeOut (oceanAudio, 1f));
            StartCoroutine (AudioFade.FadeOut (oceanMelody, .5f));
        }


        joinedHands.GetComponent<VisualEffect>().SetInt("TriggerBox", 0);
    }
}