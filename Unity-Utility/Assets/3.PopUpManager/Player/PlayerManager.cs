using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerTestClass
{
    public string name;
    public int level;

    public PlayerTestClass(string n, int l) 
    { 
        this.name = n;
        this.level = l; 
    }
}

public class PlayerManager : MonoBehaviour
{
    // �̱���
    private static PlayerManager instance;
    public static PlayerManager Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = new GameObject("PlayerManager").AddComponent<PlayerManager>();
            return instance;
        }
    }

    public PlayerTestClass playerTest;

    private void Start()
    {
        playerTest = new PlayerTestClass("�谨��" , 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) 
        {
            PlayerUiPopUp();
        }
        if (Input.GetKeyDown(KeyCode.P)) 
        {
            playerTest.level++;
        }
    }

    private void PlayerUiPopUp() 
    {
        // Up PopUp
        var temp = UIManager.Instance.ShowPopUp<PopupPlayer>();

        // �Ӷ����� initilize �ؾ��� 
        // ex) ��ŷ ���� / �κ��丮 ���� �����Ͱ� �� �ٲ�ϱ� �Ӷ����� �ʱ�ȭ�ؾ���
        temp.Initialize(playerTest , PlayerLevelUp);
    }

    private void PlayerLevelUp() 
    {
        Debug.Log($"{playerTest.name}�� ���� : {playerTest.level}");
    }
}
