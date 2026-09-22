using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    int EnterCount = 0;
    int ExitCount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        EnterCount++;
        Debug.Log("OnCollisionEnter2D Counter : " + EnterCount);
    }

    // called when the cube hits the floor
    void OnCollisionExit2D(Collision2D col)
    {
        ExitCount++;
        Debug.Log("OnCollisionExit2D Counter : " + ExitCount);
    }


}
