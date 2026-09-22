using UnityEngine;

public class Jump : MonoBehaviour
{
    Rigidbody2D rb;
    public float force = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    void OnMouseDown()
    {
        Vector2 up = new Vector2(0f, 1f) * force;
        rb.AddForce(up);
        Debug.Log("마우스 눌림");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
