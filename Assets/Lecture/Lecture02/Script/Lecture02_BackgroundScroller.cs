using UnityEngine;

public class Lecture02_BackgroundScroller : MonoBehaviour
{
    public bool isGameStarted = false;
    public float scrollDistance = 0;
    public float speed = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStarted)
        {
            transform.Translate(-speed, 0, 0);

            if(transform.position.x < -15.0f)
            {
                transform.position = new Vector3(15.0f, 0f, 0f);
            }
        }

        
    }
}
