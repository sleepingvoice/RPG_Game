using System;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [HideInInspector] public event Action<MonsterBase> EnterAction = null;

    private void Awake()
    {
        if (!this.GetComponent<MeshCollider>())
        {
            var Col = this.gameObject.AddComponent<MeshCollider>();
            Col.convex = true;
            Col.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("들어옴");
        if (other.GetComponent<MonsterBase>())
        {
            if (EnterAction == null)
                return;

            EnterAction.Invoke(other.GetComponent<MonsterBase>());
            Debug.Log("플레이어 공격");
        }
    }
}