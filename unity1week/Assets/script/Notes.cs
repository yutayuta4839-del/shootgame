using System.Runtime.CompilerServices;
using UnityEngine;

public class Notes : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] float targettime;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= transform.up * speed * Time.deltaTime;
        if(transform.position.y < -5)
        {
            Destroy(this.gameObject);
        }
    }

   
}
