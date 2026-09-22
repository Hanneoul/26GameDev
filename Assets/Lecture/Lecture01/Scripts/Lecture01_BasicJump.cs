using UnityEngine;

public class Lecture01_BasicJump : MonoBehaviour
{
    Rigidbody2D rb;
    public float force = 500.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Vector2 upForce = new Vector2(0.0f, 1.0f) * force;
        rb.AddForce(upForce);
    }
}
