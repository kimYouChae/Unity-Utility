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
            // Character 스탯 팝업 
            var temp = UIManager.Instance.ShowPopUp<CharacterStatusPopUp>();

            // 켤때마다 initilize 해야함 
            // ex) 랭킹 관련 / 인벤토리 같은 데이터가 잘 바뀌니까 켤때마다 초기화해야함
            temp.SetUpPlayerStatus(ch);
        }
    }
}
