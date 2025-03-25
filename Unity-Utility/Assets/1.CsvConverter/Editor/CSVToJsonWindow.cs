using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class CSVToJsonWindow : EditorWindow
{
    // 클래스 명 리스트 
    public List<string> className;
    
    // 리스트 표시 object
    private SerializedObject serializedObject;
    private SerializedProperty classNameProperty;

    [MenuItem("Window/CustomEditor/CSV To Json Window")]
    public static void ShowWindow() 
    {
        // 현재 활성화한 윈도우 창만, 없으면 새로 생성 
        CSVToJsonWindow window = (CSVToJsonWindow)GetWindow(typeof(CSVToJsonWindow));
        window.Show();

        // 윈도우 타이틀 지정
        window.titleContent.text = "Csv to Json Editor";
    }


    private void OnEnable()
    {
        serializedObject = new SerializedObject(this);
        classNameProperty = serializedObject.FindProperty("className");
    }

    private void OnGUI()
    {
        // 굵은 글씨에 넣을 스타일 
        Color originColor = EditorStyles.boldLabel.normal.textColor;
        EditorStyles.boldLabel.normal.textColor = Color.yellow;

        // 공백 + 메뉴 박스 출력 
        EditorUtility.DrawMemoBox(
            new string[] {
                "CSV to JSON 변환기",
                "클래스 이름 목록을 입력한 후 Convert 버튼을 클릭하세요."
            },
            EColor.Gray,
            EColor.Yellow
        );
        GUILayout.Space(20f);

        // InputField를 리스트로 
        serializedObject.Update();
        EditorGUILayout.PropertyField(classNameProperty, true);
        serializedObject.ApplyModifiedProperties();

        // 공백 + 메뉴 박스 출력 
        GUILayout.Label("R E M I N D", EditorStyles.boldLabel);
        EditorUtility.DrawMemoBox(
            new string[] {
                "1. 해당 클래스 명으로 된 CSV 파일이 존재해야 합니다.",
                "2. Resource/CsvConverter 폴더 내에 파일을 배치해주세요",
                "3. 해당 클래스는 ICsvParsable 인터페이스를 구현해야 합니다. "
            },
            EColor.Gray,
            EColor.Red
        );
        GUILayout.Space(20f);

        // 버튼 , 이벤트 추가 
        if (GUILayout.Button("Convert"))
        {
            CsvToJsonConverter.CsvConverByName(className);
        }

        // 여기 안넣어주면 Asset,Resource 등 폴더이름도 노랑색으로 됨 
        EditorStyles.boldLabel.normal.textColor = originColor;
    }

    

}
