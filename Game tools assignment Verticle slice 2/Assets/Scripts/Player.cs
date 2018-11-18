using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    void Update()
    {

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 10.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
    }

        private Animator m_animator;
    private float m_turn;
    private float m_forward;

    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    public void Move(float turn, float forward)
    {
        m_animator.SetFloat("Turn", turn);
        m_animator.SetFloat("Forward", forward);


    }
}
   
