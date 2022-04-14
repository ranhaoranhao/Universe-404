using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Interaction3D : MonoBehaviour
{
 
    UnityEngine.Ray ray;
    RaycastHit hit;
    //
    public GameObject computeOutline;
    public GameObject erjiOutline;
    public GameObject nikeOutline;


    public GameObject pickup;
    public bool canPick = true;


    //ÆÁÄ»²Î¿¼µãµÄÎ»ÖÃ
    public static Vector3 pos;
    
    // Start is called before the first frame update
    void Start()
    {
        pos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ray = Camera.main.ScreenPointToRay(pos);
        if (Physics.Raycast(ray, out hit, 10.0f))
        {
            var _hittag = hit.transform.tag;
            Debug.DrawLine(ray.origin, hit.point, Color.red);

            Debug.Log(hit.transform);

            var comp = _hittag == "computer" && canPick;
            var headphone = _hittag == "headset" && canPick;
            var nike = _hittag == "Nike" && canPick;

            // 启用对应高亮
            computeOutline.SetActive(comp);
            erjiOutline.SetActive(headphone);
            nikeOutline.SetActive(nike);
            pickup.SetActive(comp || headphone || nike);

            // 进行互动
            if (Input.GetKey(KeyCode.E))
            {
                if (GameManager.instance.ActivateAbility(hit.transform))
                {
                    canPick = false;
                }
            }
        }
        else
        {
            nikeOutline.SetActive(false);
            pickup.SetActive(false);
        }
        
    }
   
}
