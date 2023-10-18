using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    // start point and path for enemy Bus
    public Transform BusstartPoint;
    public Transform[] busPath;

    // start point and path for enemy clown car
    
    public Transform schoolstartPoint;
    public Transform[] carPath;

    private void Awake()
    {
        main = this;
    }
}
