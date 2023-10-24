using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Hand : MonoBehaviour
{
    public SteamVR_Action_Boolean m_GrabAction=null;
    SteamVR_Behaviour_Pose m_Pose=null;
    FixedJoint m_Joint=null;
    Interactable m_CurrentInteractable=null;
    public List<Interactable> m_ContactInteractables=new List<Interactable>();
    private void Awake()
    {
        m_Pose=GetComponent<SteamVR_Behaviour_Pose>();
        m_Joint=GetComponent<FixedJoint>();
    }
    private void Update()
    {
        if(m_GrabAction.GetStateDown(m_Pose.inputSource))
        {
            print(m_Pose.inputSource+"Trigger Down");
            Pickup();
        }
        if(m_GrabAction.GetStateUp(m_Pose.inputSource))
        {
            print(m_Pose.inputSource+"Trigger Up");
            Drop();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag("Interactable"))
        {
            return;
        }
        m_ContactInteractables.Add(other.gameObject.GetComponent<Interactable>());
    }
    private void OnTriggerExit(Collider other)
    {
        if(!other.gameObject.CompareTag("Interactable"))
        {
            return;
        }
        m_ContactInteractables.Remove(other.gameObject.GetComponent<Interactable>());
    }

    public void Pickup()
    {
        m_CurrentInteractable=m_ContactInteractables[0];
        m_CurrentInteractable.activeHand = this;
        Rigidbody targetBody=m_CurrentInteractable.GetComponent<Rigidbody>();
        m_Joint.connectedBody=targetBody;
    }
    public void Drop()
    {
        if(!m_CurrentInteractable)
        {
            return;
        }
        Rigidbody targetBody=m_CurrentInteractable.GetComponent<Rigidbody>();
        targetBody.velocity=m_Pose.GetVelocity();
        targetBody.angularVelocity=m_Pose.GetAngularVelocity();
        m_Joint.connectedBody=null;
        m_CurrentInteractable=null;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }
}
