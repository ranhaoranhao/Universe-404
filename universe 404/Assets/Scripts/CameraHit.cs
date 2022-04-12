using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHit : MonoBehaviour
{
    public Animator camAnim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            camAnim.SetBool("canMove", true);
        }
    }
}
