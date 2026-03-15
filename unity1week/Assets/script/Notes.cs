using System.Runtime.CompilerServices;
using UnityEngine;

public class Notes : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= transform.up * speed * Time.deltaTime;
    }
}
