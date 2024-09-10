using BaseClass;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private List<PlayerWeapon> weaponList;
    private PlayerAnimation _ani;

    [Header("Debug")]
    [SerializeField] private InputMgr _input;
    [SerializeField] private CoolMgr _cool;

    private int DoubleAttack = 0;
    private float AttakTime;
    private bool CheckDouble;

    private void Start()
    {
        _ani = this.GetComponent<PlayerAnimation>();
        _ani.aniHandler.OnAnimationComplete.AddListener((string s) =>
        {
            if ((s == "Attack01" && DoubleAttack == 1) || s == "Attack02")
            {
                DoubleAttack = 0;
                _ani.PlayerAttack((PS_Attack)0);
                CheckDouble = false;
            }
        });
    }

    private void Update()
    {
        if (_input.AttackInput && DoubleAttack == 0) // ù���� üũ
        {
            _ani.Noraml();
            DoubleAttack++;

            _ani.PlayerAttack(PS_Attack.attack01);
            CheckDouble = true;
        }
        if (DoubleAttack == 1) // �ι�° ���� üũ
        {
            if (CheckDouble)
                CheckDouble = _input.AttackInput;
            else if (_input.AttackInput)
            {
                DoubleAttack = 2;
                _ani.PlayerAttack(PS_Attack.attack02);
            }
        }
    }

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