using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum EColor
{
    Black, White, Gray, Red, Green, Blue, Yellow, Cyan, Magenta
}
public static class EditorUtility 
{
    // �޸� �ڽ��� ���� �׸��� ���� ������ ��ƿ��Ƽ �޼���
    public static void DrawMemoBox(string[] contents, EColor boxColor = EColor.Gray, EColor textColor = EColor.White)
    {
        GUIStyle labelStyle = EditorStyles.boldLabel;
        Color originalTextColor = labelStyle.normal.textColor;
        int originalFontSize = labelStyle.fontSize;

        // �ڽ� ����
        float lineHeight = 18f;
        float paddingTop = 8f;
        float paddingBottom = 8f;
        float paddingLeft = 8f;

        // �ڽ� ġ�� ���
        float textHeight = contents.Length * lineHeight;
        float boxHeight = textHeight + paddingTop + paddingBottom;
        float boxWidth = EditorGUILayout.GetControlRect().width;

        // ��� �ڽ� �׸���
        Rect boxRect = EditorGUILayout.GetControlRect(false, boxHeight);
        boxRect.width = boxWidth;

        // ������ ������ Unity �������� ��ȯ
        Color boxColorValue = GetColorFromEnum(boxColor);
        Color textColorValue = GetColorFromEnum(textColor);

        EditorGUI.DrawRect(boxRect, boxColorValue);

        // �ؽ�Ʈ ��Ÿ�� ����
        labelStyle.normal.textColor = textColorValue;
        labelStyle.fontSize = 12;

        // �� �ؽ�Ʈ �� �׸���
        float curPosY = boxRect.y + paddingTop;
        foreach (var text in contents)
        {
            Rect curRect = new Rect(boxRect.x + paddingLeft, curPosY, boxWidth - paddingLeft, lineHeight);
            EditorGUI.LabelField(curRect, text, labelStyle);
            curPosY += lineHeight;
        }

        // ���� ��Ÿ�� ����
        labelStyle.normal.textColor = originalTextColor;
        labelStyle.fontSize = originalFontSize;
    }

    private static Color GetColorFromEnum(EColor color)
    {
        switch (color)
        {
            case EColor.Black: return Color.black;
            case EColor.White: return Color.white;
            case EColor.Gray: return Color.gray;
            case EColor.Red: return Color.red;
            case EColor.Green: return Color.green;
            case EColor.Blue: return Color.blue;
            case EColor.Yellow: return Color.yellow;
            case EColor.Cyan: return Color.cyan;
            case EColor.Magenta: return Color.magenta;
            default: return Color.white;
        }
    }
}
