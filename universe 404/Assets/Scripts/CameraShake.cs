using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;
    // Start is called before the first frame update

    private void Start()
    {
        Instance = this;
    }
  
    public void canShake(float ShakeRange,float ShakeTime)
    {
        StopAllCoroutines();
        StartCoroutine(Shake(ShakeRange ,ShakeTime));
    }

    public IEnumerator Shake(float ShakeRange,float ShakeTime)
    {
        Vector3 currentPos = transform.position;
        
        while (ShakeTime >= 0)
        {
            if(ShakeTime <= 0)
            {
                break;
            }
            ShakeTime -= Time.deltaTime;
            Vector3 pos = transform.position;
            pos.x += Random.Range(-ShakeRange, ShakeRange);
            pos.y += Random.Range(-ShakeRange, ShakeRange);
            transform.position = pos;

            yield return null;
        }
        transform.position = currentPos;
    }
}
