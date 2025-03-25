using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character
{
    [SerializeField] private float attack;
    [SerializeField] private float defence;
    [SerializeField] private float hp;
    [SerializeField] private float criticalHit;

    public float Attack { get => attack; set => attack = value; }
    public float Defence { get => defence; set => defence = value; }
    public float Hp { get => hp; set => hp = value; }
    public float CriticalHit { get => criticalHit; set => criticalHit = value; }

    public Character(float a, float d, float h, float c)
    {
        this.attack = a;
        this.defence = d;
        this.hp = h;
        this.criticalHit = c;
    }
}

public class CharacterManager : MonoBehaviour
{
    Character ch;

    private void Start()
    {
        ch = new Character(10,4,6,2);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.L)) 
        {
            // Character ���� �˾� 
            var temp = UIManager.Instance.ShowPopUp<CharacterStatusPopUp>();

            // �Ӷ����� initilize �ؾ��� 
            // ex) ��ŷ ���� / �κ��丮 ���� �����Ͱ� �� �ٲ�ϱ� �Ӷ����� �ʱ�ȭ�ؾ���
            temp.SetUpPlayerStatus(ch);
        }
    }
}
