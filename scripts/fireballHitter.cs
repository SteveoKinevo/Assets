using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballHitter : MonoBehaviour
{
    void OnParticleCollision(GameObject obj)
    {
        Debug.Log("particle");
    }
}