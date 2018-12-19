using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserControl : MonoBehaviour {

    private float m_turn;
    private float m_forward;
    private bool m_jump;
    private bool m_pick;
    private bool m_Dance;
    private bool m_aimDown;
    private bool m_aimHold;
    private bool m_fire;
    private bool m_PowerUp;
    private float m_deltaX;

    private Character m_character;

    private void Start()
    {
        m_character = GetComponent<Character>();
    }
 


    void FixedUpdate()
    {
        // Get Inputs
        m_turn = Input.GetAxis("Horizontal");
        m_forward = Input.GetAxis("Vertical");
        m_jump = Input.GetButtonDown("Jump");
        m_pick = Input.GetKeyDown(KeyCode.C);
        m_Dance = Input.GetKeyDown(KeyCode.Q);
        m_aimDown = Input.GetMouseButtonDown(1);
        m_aimHold = Input.GetMouseButton(1);
        m_fire = Input.GetMouseButtonDown(0);
        m_PowerUp = Input.GetKeyDown(KeyCode.T);


        m_character.Move(m_turn,m_forward, m_jump, m_pick, m_Dance,m_PowerUp);
        m_character.AimFire(m_aimDown, m_aimHold, m_fire, m_deltaX);
    }

   


}
