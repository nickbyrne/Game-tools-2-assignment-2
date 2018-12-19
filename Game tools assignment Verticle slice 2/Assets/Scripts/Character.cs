using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour {

    private Animator m_animator;
    private bool m_picked;
    private bool m_aim;

    public UnityEvent OnFire;

    private float deltaX;
    private Quaternion spineRotation;
    private bool m_enableIK;
    private float m_weightIK;
    private Vector3 m_positionIK;

    
    void Start()
    {
        // Initialize Animator
        m_animator = GetComponent<Animator>();
    }

    public void Move(float turn, float forward, bool jump, bool picked, bool Dance, bool PowerUp)
    {
        m_animator.SetFloat("Turn", turn);
        m_animator.SetFloat("Forward", forward);

        m_picked = picked;
        if (picked)
        {
            m_animator.SetTrigger("pick");
        }

        if (jump)
        {
            m_animator.SetTrigger("Jump");
        }
        if (Dance)
        {
            m_animator.SetTrigger("Dance");
        }
        if(PowerUp)
        {
            m_animator.SetTrigger("PowerUp");
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Pickable")
        {
            var pickable = other.GetComponent<Pickable>();

            //Debug.Log("PickingTrigger");
            //Debug.Log(pickable.picked);

            if (m_picked && pickable != null && !pickable.picked)
            {
                // do something
                Transform rightHand = m_animator.GetBoneTransform(HumanBodyBones.RightHand);
                pickable.BePicked(rightHand);

                m_animator.SetTrigger("Pick");
                StartCoroutine(UpdateIK(other));// Start corroutine to update position and weight
            }
        }
    }

    private IEnumerator UpdateIK(Collider other)
    {
        m_enableIK = true;

        while (m_enableIK)
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
        if (m_aim)
        {
            //do spine rotation
            Vector3 rotationEuler = new Vector3(0, deltaX, 0);
            Quaternion rotationOffeset = Quaternion.Euler(rotationEuler);
            spineRotation = Quaternion.Lerp(spineRotation, spineRotation * rotationOffeset, Time.deltaTime * 50.0f);

            rotationEuler = spineRotation.eulerAngles;
            if (rotationEuler.y > 180)
            {
                rotationEuler.y -= 360;
            }
            if (rotationEuler.y < 180)
            {
                rotationEuler.y += 360;
            }
            rotationEuler.y = Mathf.Clamp(rotationEuler.y, -60.0f, +60.0f);
            m_animator.SetBoneLocalRotation(HumanBodyBones.Spine, Quaternion.Euler(rotationEuler));
        }
    }

    public void AimFire(bool aimDown, bool aimHold, bool fire, float deltaX)
    {
        m_animator.SetBool("Aim", aimHold);


        if (aimDown) // Get spine rotation only on first frame
        {
            spineRotation = m_animator.GetBoneTransform(HumanBodyBones.Spine).localRotation;
        }

        m_aim = aimHold;
        this.deltaX = deltaX;

        m_animator.SetBool("Aim", m_aim);

        if (m_aim && fire)
        {
            Fire();
        }


    }

    private void Fire()
    {
        m_animator.SetTrigger("Fire");

        if (OnFire != null)
        {
            OnFire.Invoke();
        }

    }
}

