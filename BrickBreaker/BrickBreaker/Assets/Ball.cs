using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityOSC;

public class Ball : MonoBehaviour
{
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Respawn();
    }

    public void Respawn() {
        transform.position = Vector3.zero;
        GetComponent<Rigidbody2D>().velocity = Random.insideUnitCircle.normalized * speed;
    }

    void Update() {
        // normalizes vertical speed if too high or too low
        if(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) < .4) {
            // Debug.Log("Vertical speed increased from " + GetComponent<Rigidbody2D>().velocity.y.ToString() + " to " + (GetComponent<Rigidbody2D>().velocity.y * 2).ToString());
            if(GetComponent<Rigidbody2D>().velocity.y == 0) {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -0.4f);
            }
            else {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y * 2);
            }
        }
        if(Mathf.Sqrt(Mathf.Pow(GetComponent<Rigidbody2D>().velocity.y, 2) * Mathf.Pow(GetComponent<Rigidbody2D>().velocity.x, 2)) > 9) {
            // Debug.Log("Vertical Speed over 9, reducing.");
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity * .9f;
        }
    }


    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Paddle") {
            // use "(position - position) / width" for proportional to size
            float distance = (transform.position.x - other.gameObject.transform.position.x) / 1.0f;
            //Debug.Log(1 - Mathf.Abs(distance));
            GetComponent<Rigidbody2D>().velocity = new Vector2(1.1f * distance, 1 - Mathf.Abs(distance)) * speed;
            //Debug.Log(transform.position.x - other.gameObject.transform.position.x ); // need to make reflection based on position
        }
    }
}
