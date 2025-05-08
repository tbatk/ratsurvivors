using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector2 m_Move;
    void Start()
    {
        
    }
    public void OnMove(InputValue value) {
        m_Move = value.Get<Vector2>();
        if (m_Move.x == -1) {
            //Move left
        }
        if (m_Move.x == 1) {
            //Move right 
        }
        if (m_Move.y == -1) {
            //Move down 
        }
        if (m_Move.y == 1) {
            //Move up 
        }


    }

    // Update is called once per frame
    void Update()
    {
    }
}
