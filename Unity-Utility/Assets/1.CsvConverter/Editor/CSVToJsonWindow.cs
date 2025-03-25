using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class CSVToJsonWindow : EditorWindow
{
    // Ŭ���� �� ����Ʈ 
    public List<string> className;
    
    // ����Ʈ ǥ�� object
    private SerializedObject serializedObject;
    private SerializedProperty classNameProperty;

    [MenuItem("Window/CustomEditor/CSV To Json Window")]
    public static void ShowWindow() 
    {
        // ���� Ȱ��ȭ�� ������ â��, ������ ���� ���� 
        CSVToJsonWindow window = (CSVToJsonWindow)GetWindow(typeof(CSVToJsonWindow));
        window.Show();

        // ������ Ÿ��Ʋ ����
        window.titleContent.text = "Csv to Json Editor";
    }


    private void OnEnable()
    {
        serializedObject = new SerializedObject(this);
        classNameProperty = serializedObject.FindProperty("className");
    }

    private void OnGUI()
    {
        // ���� �۾��� ���� ��Ÿ�� 
        Color originColor = EditorStyles.boldLabel.normal.textColor;
        EditorStyles.boldLabel.normal.textColor = Color.yellow;

        // ���� + �޴� �ڽ� ��� 
        EditorUtility.DrawMemoBox(
            new string[] {
                "CSV to JSON ��ȯ��",
                "Ŭ���� �̸� ����� �Է��� �� Convert ��ư�� Ŭ���ϼ���."
            },
            EColor.Gray,
            EColor.Yellow
        );
        GUILayout.Space(20f);

        // InputField�� ����Ʈ�� 
        serializedObject.Update();
        EditorGUILayout.PropertyField(classNameProperty, true);
        serializedObject.ApplyModifiedProperties();

        // ���� + �޴� �ڽ� ��� 
        GUILayout.Label("R E M I N D", EditorStyles.boldLabel);
        EditorUtility.DrawMemoBox(
            new string[] {
                "1. �ش� Ŭ���� ������ �� CSV ������ �����ؾ� �մϴ�.",
                "2. Resource/CsvConverter ���� ���� ������ ��ġ���ּ���",
                "3. �ش� Ŭ������ ICsvParsable �������̽��� �����ؾ� �մϴ�. "
            },
            EColor.Gray,
            EColor.Red
        );
        GUILayout.Space(20f);

        // ��ư , �̺�Ʈ �߰� 
        if (GUILayout.Button("Convert"))
        {
            CsvToJsonConverter.CsvConverByName(className);
        }

        // ���� �ȳ־��ָ� Asset,Resource �� �����̸��� ��������� �� 
        EditorStyles.boldLabel.normal.textColor = originColor;
    }

    

}
