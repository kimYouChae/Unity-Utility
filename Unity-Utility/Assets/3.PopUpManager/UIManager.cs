using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;      // list.Any 사용 
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // 싱글톤
     private static UIManager instance;
    public static UIManager Instance
    {
        get 
        {
            if (instance != null)
                return instance;

            instance = new GameObject("UIManager").AddComponent<UIManager>();
            return instance;
        }
    }

    // 팝업 저장 리스트 
    [SerializeField] private List<UiPopUp> popUps;

    // 캔버스 오브젝트
    [SerializeField] private GameObject canvas;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        popUps = new List<UiPopUp>();

        // 캔버스 찾기 (이름으로)
        canvas = GameObject.Find("Canvas");
        if (canvas == null)
            Debug.LogError($"UIManager : canvas를 불러오는데 실패 했습니다");
    }

    #region 함수 추가 
    // 해당 UiPopUp이 리스트에 있는지 검사 
    private bool ContainsUiPopup(string name) 
    {
        // typeof : 컴파일타임에
        // Gettype() : 런타임에
        // 있으면 true, 없으면 false
        return popUps.Any(pop => pop.GetType().Name == name);
    }

    // string에 해당하는 UiPopup 을 return
    private UiPopUp ReturnPopUpByName(string name) 
    {
        // Find : 리스트내 아이템 검색
        return popUps.Find(pop => pop.GetType().Name == name);
    }

    #endregion

    // 원래 함수이름 : ShowPopUp
    public T ShowPopUp<T>() where T : UiPopUp 
    {
        // T로 매개변수를 넘길수는 없다 , 넘기려면 리플렉션 사용
        // 배열에 있으면 
        if (ContainsUiPopup(typeof(T).Name)) 
        {
            UiPopUp temp = ReturnPopUpByName(typeof(T).Name);
            temp.gameObject.SetActive(true);
            return temp as T;
        }

        // 1. 클래스 이름을 문자열로 가져옴
        // 2. UiPopUp 타입을 T 타입으로 변환해서 return
        // ex) T가 PopupPlayer일 때,
        //     ShowPopup에서 return된 UiPopup을 PopupPlayer로 바꿔서 return
        return InstancePopUp(typeof(T).Name) as T;
    }

    // 팝업 프리팹 들고오기 (Resource/PopUp 내 )
    public UiPopUp InstancePopUp(string popupName) 
    {
        // ##TODO : 나중에 Resource 로더 만들면 좋을듯 ?
        // Resource 하위 PopUp 폴더내에 이름으로 프리팹 가져오기 
        GameObject upPrefab = Resources.Load($"PopUp/{popupName}", typeof(GameObject)) as GameObject;

        // null이면 return
        if (upPrefab == null) 
        {
            Debug.LogWarning($"UIManager : InstancePopUp 도중 {popupName} 불러오기 실패");
            return null;
        }

        // 오브젝트 인스턴스화, canvas 하위에
        GameObject popupInstance = null;
        try
        {
            popupInstance = Instantiate(upPrefab, canvas.transform);
        }
        catch (Exception e) { Debug.Log($"UIManager : {upPrefab.name} 프리팹 생성 오류 {e}"); }

        // 리스트에 넣은 후 return 
        return AddtoPopUpList(popupInstance);
    }

    // 리스트에 넣기
    private UiPopUp AddtoPopUpList(GameObject instance) 
    {
        UiPopUp uiPopUp = null;
        try 
        {
            uiPopUp = instance.GetComponent<UiPopUp>();
        }
        catch (Exception e) { Debug.Log($"UIManager : {instance.name} 프리팹에서 GetComponent 오류 {e}"); }

        // 항상 첫번째로 들어가게 insert
        popUps.Insert(0, uiPopUp);

        // 켜기
        instance.SetActive(true);

        return uiPopUp;
    }
}
