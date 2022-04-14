using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CharacterLife : MonoBehaviour
{
    bool _dead = false;
    IEnumerator DoReload()
    {
        yield return new WaitForSeconds(3f);
        GameManager.instance.Reload();
        _dead = false;
    }

    public void Hit(GameObject hitObject)
    {
        if (_dead)
            return;

        // TODO: 死亡动画

        _dead = true;
        StartCoroutine(DoReload());
        // GameManager.instance.Reload();
    }

    public bool Alive()
    {
        return !_dead;
    }
}
