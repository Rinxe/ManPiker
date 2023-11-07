using System;
using UnityEngine;

public class CharacterHandleEvent : MonoBehaviour
{
    public Action ActionAnimationAvent = delegate () {};

    public void OnActionAnimationAvent() => ActionAnimationAvent();
}

