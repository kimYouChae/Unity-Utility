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
    // 싱글톤
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
        playerTest = new PlayerTestClass("김감자" , 0);
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

        // 켤때마다 initilize 해야함 
        // ex) 랭킹 관련 / 인벤토리 같은 데이터가 잘 바뀌니까 켤때마다 초기화해야함
        temp.Initialize(playerTest , PlayerLevelUp);
    }

    private void PlayerLevelUp() 
    {
        Debug.Log($"{playerTest.name}의 레벨 : {playerTest.level}");
    }
}
