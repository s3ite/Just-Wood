using UnityEngine;
using System.Collections;

public class Datas : MonoBehaviour
{
    public static Datas instance;

    // Use this for initialization
    void Awake()
    {
        instance = this;
    }

    public int MyIndex;
}
