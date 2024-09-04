using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [HideInInspector] public event Action<GameObject> EnterAction;

    private void Awake()
    {
        if (!this.GetComponent<MeshCollider>())
        {
            var Col = this.gameObject.AddComponent<MeshCollider>();
            Col.convex = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        EnterAction.Invoke(other.gameObject);
    }
}