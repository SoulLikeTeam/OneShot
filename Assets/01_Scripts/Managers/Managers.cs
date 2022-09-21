using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers instance;
    static Managers Instance { get { Init(); return instance; } }

    #region CORE
    //Manager

    //Property

    #endregion

    private void Start()
    {
        Init();
    }
    static void Init()
    {

    }

    static void Clear()
    {

    }
}
