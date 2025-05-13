using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float speed = 3.0f;
    private Vector2 move = new Vector2(0,0);
    void Start()
    {
        
    }
    public void OnMove(InputValue value) {
        move = value.Get<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(move.x, move.y, 0) * speed * Time.deltaTime;
    }
}
