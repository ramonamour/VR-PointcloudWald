using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class waterInteraction : MonoBehaviour
{

    [SerializeField] private VisualEffect wald;
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        wald.SetVector3("leftHand", leftHand.transform.position);
        wald.SetVector3("rightHand", rightHand.transform.position);

    }
}
