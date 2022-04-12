using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray : MonoBehaviour
{
    UnityEngine.Ray ray;
    RaycastHit hit;

    public GameObject computeOutline;
    public GameObject pickup;
    public bool canPick;

    public static bool isComputer;
    //屏幕参考点的位置
    Vector3 pos = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f);
    // Start is called before the first frame update
    void Start()
    {
        canPick = true;
        DontDestroyOnLoad(GameObject.Find("computer"));
    }

    // Update is called once per frame
    void Update()
    {
        //生成射线
        ray = Camera.main.ScreenPointToRay(pos);
        //射线与物体碰撞
        if(Physics.Raycast(ray,out hit, 10.0f))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red);
            if(hit.transform.name == "computer" && canPick)
            {
                computeOutline.SetActive(true);
                pickup.SetActive(true);
            }
            else
            {
                computeOutline.SetActive(false);
                pickup.SetActive(false);
            }


            if (hit.transform.name == "computer" && Input.GetKey(KeyCode.E ))
            {
                isComputer = true;
                canPick = false;
            }
            else
            {
                isComputer = false;
            }
        }
        
    }
}
