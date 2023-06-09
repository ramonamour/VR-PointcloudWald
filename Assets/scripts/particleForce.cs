using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class particleForce : MonoBehaviour
{
    [SerializeField] private GameObject joinedHands;
    private VisualEffect handvfx;
    //private float DistanceToJoined;
    // Start is called before the first frame update
    void Start()
    {
        handvfx = GetComponent<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        handvfx.SetVector3("joinedPosition", joinedHands.transform.position);
        handvfx.SetVector3("handPosition", this.transform.position);
        float DistanceToJoined = Vector3.Distance(joinedHands.transform.position, this.transform.position);
        handvfx.SetFloat("Force", DistanceToJoined);
    }
}