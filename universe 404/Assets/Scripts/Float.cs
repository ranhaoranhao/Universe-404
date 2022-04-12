using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{
    public float speed;
    public Transform movePos;
  
    private Vector3 _newPos;
    private Vector3 _oldPos;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _newPos = transform.position;
        transform.position = Vector2.MoveTowards(transform.position, movePos.position, speed * Time.deltaTime);
        if(_oldPos.x > _newPos.x )
        {
          transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        if (_oldPos.x < _newPos.x )
        {
          transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        _oldPos = _newPos;
    }
   
}
