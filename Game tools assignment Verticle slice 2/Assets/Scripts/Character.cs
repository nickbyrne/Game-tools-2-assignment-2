using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    private Animator m_animator;
    private bool m_picked;


    private bool m_enableIK;
    private float m_weightIK;
    private Vector3 m_positionIK;

    // Use this for initialization
    void Start()
    {
        // Initialize Animator
        m_animator = GetComponent<Animator>();
    }

    public void Move(float turn, float forward, bool jump, bool picked)
    {
        m_animator.SetFloat("Turn", turn);
        m_animator.SetFloat("Forward", forward);

        m_picked = picked;

        if (jump)
        {
            m_animator.SetTrigger("Jump");
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (m_picked)
        {
            var pickable = other.GetComponent<Pickable>();

            if (m_picked && pickable != null && !pickable.picked)
            {
                Transform righthand = m_animator.GetBoneTransform(HumanBodyBones.RightHand);
                pickable.BePicked(righthand);
                m_animator.SetTrigger("Pick");
                StartCoroutine(updateIK(other));//start coruntine to update position and weight
            }
        }
    }

    private IEnumerator updateIK (Collider other)
    {
        while(m_enableIK)
        {
            m_positionIK = other.transform.position;
            m_weightIK = m_animator.GetFloat("IK");
            yield return null;
        }
    }

    public void DisableIK()
    {
        m_enableIK = false;
    }
    private void OnAnimatorIK(int layerIndex)
    {
        if (m_enableIK)
        {
            m_animator.SetIKPosition(AvatarIKGoal.RightHand, m_positionIK);
            m_animator.SetIKPositionWeight(AvatarIKGoal.RightHand, m_weightIK);
        }
    }
}

