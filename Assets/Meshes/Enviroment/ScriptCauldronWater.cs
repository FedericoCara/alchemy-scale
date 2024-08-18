using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCauldronWater : MonoBehaviour
{
    void Update()
    {
        transform.eulerAngles += Vector3.up*30f*Time.deltaTime;
    }
}
