using UnityEngine;

public class Lecture03_SetActive : MonoBehaviour
{
    public GameObject activeGameObject;
    public bool isActive = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        activeGameObject.SetActive(isActive);
    }
}
