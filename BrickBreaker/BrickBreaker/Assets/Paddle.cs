using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityOSC;

public class Paddle : MonoBehaviour
{
    Dictionary<string, ServerLog> servers = new Dictionary<string, ServerLog>();

    public float speed = 5f;

    private float input;

    // Start is called before the first frame update
    void Start()
    {

        OSCHandler.Instance.Init();
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/trigger", "ready");
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/playseq", 1);

    }

    // Update is called once per frame
    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");
        
    }

    private void FixedUpdate() 
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * input * speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Check if collision is with ball
        if (other.collider.name == "Ball")
        {
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/playpaddle", 1);
        }
    }
}
