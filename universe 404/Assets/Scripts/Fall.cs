using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name =="Player" ||collision.gameObject.tag =="Ground")
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 2;
            Debug.Log("½øÀ´");
        }
    }
}
