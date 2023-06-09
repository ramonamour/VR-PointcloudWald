using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BottomUpGesture : MonoBehaviour
{
    [SerializeField] private GameObject joinedHands;
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

    // Start is called before the first frame update
    void Start()
    {

        wald.SetFloat("TimeAfterForestSwitch", 0f);
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

        if (wald.GetFloat("TimeAfterForestSwitch") > 0) 
        {
            wald.SetFloat("TimeAfterForestSwitch", wald.GetFloat("TimeAfterForestSwitch") - .01f);
        }
        else {
            
            wald.SetFloat("TimeAfterForestSwitch", 0f);
        }
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("Entered Bottom");
        movementXDirection = 0;
        movementYDirection = 0;
        movementZDirection = 0;
        //wald.SetBool("TopDownGesture",true);
        joinedHands.GetComponent<VisualEffect>().SetInt("TriggerBox", 2);

    }
    private void OnTriggerExit(Collider other) 
    {
        Debug.Log("Exit");
        Debug.Log("X: " + movementXDirection);
        Debug.Log("Z: " + movementZDirection);

        if (movementXDirection < .5 && movementZDirection < .5 && wald.GetBool("TopDownGesture") == true){
            wald.SetBool("TopDownGesture", false);
            wald.SetFloat("TimeAfterForestSwitch", 3f);


            StartCoroutine (AudioFade.FadeOut (oceanAudio, 1f));
            StartCoroutine (AudioFade.FadeOut (oceanMelody, .5f));
            StartCoroutine (AudioFade.FadeIn (waldAudio, .5f));
            //waldAudio.Stop();
            //oceanAudio.Play();

        }
        else if (wald.GetBool("TopDownGesture") == true) {
            StartCoroutine (AudioFade.FadeOut (waldAudio, 1f));
        }


        joinedHands.GetComponent<VisualEffect>().SetInt("TriggerBox", 0);
    }
}
