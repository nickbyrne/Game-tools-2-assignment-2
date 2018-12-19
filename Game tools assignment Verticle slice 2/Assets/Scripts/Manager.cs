using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Manager : MonoBehaviour {

    [SerializeField] float m_playerHealth;
    void Start() {

    }


    public void DecreaseHealth()
    {
        m_playerHealth = m_playerHealth - 0.1f;
        CheckHealth();
        Debug.Log(m_playerHealth);
    }

    private void CheckHealth()
    {
        if (m_playerHealth <= 0)
        {
            GameOver();
        }
    }
        

    private void GameOver()
    {
        SceneManager.LoadScene(0);
    }
}
