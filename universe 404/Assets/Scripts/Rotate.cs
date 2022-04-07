using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private float Angle;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
       
        Angle += speed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, Angle);

    }
}
