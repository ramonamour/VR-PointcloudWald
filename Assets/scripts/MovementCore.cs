using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MovementCore : MonoBehaviour
{
    [SerializeField] GameObject joinedHands;
    [SerializeField] GameObject core;
    [SerializeField] GameObject PlayerSystem;

    float coreDistance;
    Vector3 movementDirection;
    float coreDistanceOld;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //X & Z Position der UserPosition (XR) und Y der JoinedHandsPosition
        core.transform.position = new Vector3(0,joinedHands.transform.position.y, 0);

        joinedHands.GetComponent<VisualEffect>().SetVector3("corePosition", core.transform.position);
        joinedHands.GetComponent<VisualEffect>().SetVector3("joinedPosition", joinedHands.transform.position);

        //distance to core

        coreDistance = Vector3.Distance(core.transform.position, joinedHands.transform.position);
        coreDistanceOld = coreDistance;

        if (joinedHands.activeSelf && coreDistance > 0.7 && (joinedHands.GetComponent<VisualEffect>().GetInt("TriggerBox") == 0 || joinedHands.GetComponent<VisualEffect>().GetInt("TriggerBox") == 3))
        {
            joinedHands.GetComponent<VisualEffect>().SetInt("TriggerBox", 3);
            Debug.Log("coreDistance = " + coreDistance);
            //coreDistanceOld = coreDistance;
        }
        else if (joinedHands.activeSelf && joinedHands.GetComponent<VisualEffect>().GetInt("TriggerBox") == 3 && coreDistanceOld >= coreDistance && coreDistance > 0.2) {
            movementDirection = (joinedHands.transform.position - core.transform.position).normalized/10;
            PlayerSystem.transform.position = PlayerSystem.transform.position + movementDirection;
            Debug.Log(PlayerSystem.transform.position);
        }

        else if (joinedHands.GetComponent<VisualEffect>().GetInt("TriggerBox") == 3) {
            joinedHands.GetComponent<VisualEffect>().SetInt("TriggerBox", 0);
        }
        
    }
}
