using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private List<PlayerWeapon> weaponList;


    #region ���� ����

    public void SetWeaponList()
    {
        weaponList = FindAllWeaponsInChildren(this.gameObject);
    }

    public List<PlayerWeapon> FindAllWeaponsInChildren(GameObject parentObject)
    {
        List<PlayerWeapon> weaponsList = new List<PlayerWeapon>();

        // ��������� �ڽ� ��ü Ž��
        AddWeaponsInChildren(parentObject.transform, weaponsList);

        return weaponsList;
    }
    private void AddWeaponsInChildren(Transform parentTransform, List<PlayerWeapon> weaponsList)
    {
        foreach (Transform child in parentTransform)
        {
            PlayerWeapon weapon = child.GetComponent<PlayerWeapon>();

            if (weapon != null)
            {
                weaponsList.Add(weapon);
            }

            AddWeaponsInChildren(child, weaponsList);
        }
    }

    #endregion

    public void SetWeaponDamageAct(Action<MonsterBase> ActtacAct)
    {
        for (int i = 0; i < weaponList.Count; i++)
        {
            weaponList[i].EnterAction += ActtacAct;
        }
    }
}

#region ������

[CustomEditor(typeof(PlayerAttack))]
public class PlayerAttackEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Space(10);

        PlayerAttack Target = (PlayerAttack)target;
        if (GUILayout.Button("���� ����"))
        {
            Target.SetWeaponList();
        }
    }
}

#endregion