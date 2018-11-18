using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key_pickup : MonoBehaviour
{
    public LayerMask Keylayer;
    public float collisionRadius, CollisionDistance;
    public int KeytoAdd;
    public Text ScoreText;

    public AudioSource GotIt;

    private int _score;
    private bool _isKey;

    private void OnCollisionEnter(Collision collision)
    {
        if (Keylayer != (1 << collision.gameObject.layer))
            {
            return;
        }
        else{
            if (RaycastHit(transform.position,collisionRadius, Vector3.down,CollisionDistance,Keylayer))
            {
                Destroy(collision.gameObject);
                _score += KeytoAdd;
                ScoreText.text = "Keys: " + _score + "/6";
             
            }
        }
        if (_score >= 6)
        {
            GameObject go = GameObject.Find("Exit");
            if(go)
            {
                Destroy(go.gameObject);
                Debug.Log(name + "has been destroyed");
            }
        }
    }

    private bool RaycastHit(Vector3 position, float collisionRadius, Vector3 down, float collisionDistance, LayerMask keylayer)
    {
        throw new NotImplementedException();
    }
}
