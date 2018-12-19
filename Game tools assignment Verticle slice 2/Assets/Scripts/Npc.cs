using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Npc : MonoBehaviour {

    private enum NpcState {CHASE, PATROL};
    private NpcState m_NpcState;
    private NavMeshAgent m_NavMeshAgent;
    private int m_CurrentWaypoint;
    private bool m_isPlayerNear;
    private Animator m_Animator;

    [SerializeField] float m_FieldOfView;
    [SerializeField] float m_ThresholdDistance;
    [SerializeField] private Transform[] m_Waypoints;
    [SerializeField] GameObject m_Player;
    [SerializeField] Manager m_manager;



    void Start () {
        m_NpcState = NpcState.PATROL;
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_CurrentWaypoint = 0;
        m_Animator = GetComponent<Animator>();

        m_NavMeshAgent.updatePosition = false;
        m_NavMeshAgent.updateRotation = true;

        HandleAnimation();
    }
	

	void Update () {
        CheckPlayer();
        m_NavMeshAgent.nextPosition = transform.position;

        switch (m_NpcState)
        {
            case NpcState.CHASE:
                Chase();
                break;
            case NpcState.PATROL:
                Patrol();
                break;
            default:
                break;
                
        }
	}

    void  CheckPlayer()
    {
        if(m_NpcState == NpcState.PATROL && m_isPlayerNear && CheckFieldOfView() && CheckOclusion())
        {
            m_NpcState = NpcState.CHASE;
            HandleAnimation();
            return;
        }

        if( m_NpcState==NpcState.CHASE && !CheckOclusion())
        {
            m_NpcState = NpcState.PATROL;
            HandleAnimation();
        }
    }

    void Chase()
    {
        Debug.Log("chasing");
        m_NavMeshAgent.SetDestination(m_Player.transform.position);
    }

    bool CheckFieldOfView()
    {
        Vector3 direction = m_Player.transform.position - this.transform.position;
        Vector3 angle = (Quaternion.FromToRotation(transform.forward, direction)).eulerAngles;


        if (angle.y > 180.0f) angle.y = 360.0f - angle.y;
        else if (angle.y < -180.0f) angle.y = angle.y + 360.0f;


        if (angle.y < m_FieldOfView / 2)
        {
            return true;
        }

        return false;
    }

    bool CheckOclusion()
    {
        RaycastHit hit;
        Vector3 direction = m_Player.transform.position - transform.position;

        if (Physics.Raycast(this.transform.position, transform.forward, out hit, 5.0f))
        {
            if (hit.collider.gameObject == m_Player)
            {
                return true;
            }
        }
        return false;
    }


    void Patrol()
    {
        Debug.Log("Patrolling");

        CheckWayPointDistance();
        m_NavMeshAgent.SetDestination(m_Waypoints[m_CurrentWaypoint].position);
    }

    void CheckWayPointDistance()
    {
        if(Vector3.Distance(m_Waypoints[m_CurrentWaypoint].position,this.transform.position) <m_ThresholdDistance)
        {
            m_CurrentWaypoint = (m_CurrentWaypoint + 1) & m_Waypoints.Length;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
           m_isPlayerNear = true;
           m_manager.DecreaseHealth();
        }
    }

    private void OnCollisionStay(Collision other)
    {
        //m_manager.DecreaseHealth();
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            m_isPlayerNear = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 5.0f);

        Gizmos.color = Color.red;
        Vector3 direction = m_Player.transform.position - transform.position;
        Gizmos.DrawRay(transform.position, direction);

        Vector3 rightDirection = Quaternion.AngleAxis(m_FieldOfView / 2, Vector3.up) * transform.forward;
        Vector3 leftDirection = Quaternion.AngleAxis(-m_FieldOfView / 2, Vector3.up) * transform.forward;

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, rightDirection * 5.0f);
        Gizmos.DrawRay(transform.position, leftDirection * 5.0f);
    }

    void HandleAnimation()
    {
        if (m_NpcState == NpcState.CHASE)
        {
            m_Animator.SetFloat("Forward", 2);
        }
        else
        {
            m_Animator.SetFloat("Forward", 1);
        }
    }

}
