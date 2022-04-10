using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShip : MonoBehaviour
{
    private Rigidbody2D rb2d_as;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb2d_as = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2d_as.velocity = Vector2.left * moveSpeed;
    }
}
