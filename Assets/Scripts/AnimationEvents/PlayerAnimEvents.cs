using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{
    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void EndDamage()
    {
        _anim.SetBool("Damage", false);
    }
}
