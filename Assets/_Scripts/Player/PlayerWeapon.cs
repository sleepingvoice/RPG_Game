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
        Debug.Log("����");
        if (other.GetComponent<MonsterBase>())
        {
            if (EnterAction == null)
                return;

            EnterAction.Invoke(other.GetComponent<MonsterBase>());
            Debug.Log("�÷��̾� ����");
        }
    }
}