using Palmmedia.ReportGenerator.Core.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public interface ICsvParsable
{
    void Parse(string[] values);

}

public static class CsvToJsonConverter
{
    /// <summary>
    /// ***Converter ��� �� ���ǻ���***
    /// 1. �Է��� string, Ŭ������, Recources������ csv ������ �̸��� �����ؾ��մϴ�.
    /// 2. Recources���� ������ csv�����Ͱ� �����ؾ��մϴ�.
    /// 3. csv �����͸� �Ľ��ؼ� ����� Ŭ���� ����
    ///     (1) Ŭ���������մϴ�.
    ///     (2) ICsvParsable �������̽��� �����ؾ��մϴ�.
    ///     (3) �Ű������� ���� �����ڸ� ������ �־�� �մϴ�.
    ///
    /// </summary>


    public static void CsvConverByName(List<string> className)
    {
        // Debug.Log("CsvConverter�޼����Դϴ�");

        for (int i = 0; i < className.Count; i++)
        {
            // (����) ���������
            if (className[i] == string.Empty)
                return;

            // ���⼭ type�� ? Ŭ������� �����ϸ� ���� 
            // string�� �´� Ÿ�� ���� 
            Type type = Type.GetType(className[i]);

            if (type == null)
            {
                Debug.LogError($"�ش� Ŭ����({className[i]})�� ã�� �� �����ϴ�.");
                continue;
            }

            if (!typeof(ICsvParsable).IsAssignableFrom(type))
            {
                Debug.LogError($"Ŭ����({className[i]})�� ICsvParsable�� �����ؾ� �մϴ�.");
                continue;
            }

            // CsvDataParsing<> Ŭ������ Ÿ����
            // MakeGeneritType : type���� ���׸� ����
            // convertype : �� CsvDataParsing<Ŭ������>�� �ȴ�
            Type converterType = typeof(CsvDataParsing<>).MakeGenericType(type);

            // CsvDataParsing �ν��Ͻ�ȭ 
            // �Ű������� className[i]
            object converterInstance = Activator.CreateInstance(converterType, className[i]);

            // CsvDataParsing<>�� GetDataArray() �޼��� �������� 
            MethodInfo method = converterType.GetMethod("GetDataArray");

            if (method != null)
            {
                // GetDataArray �޼��� Invoke
                // return ���� List<T>������ objectŸ������ �ڽ�(boxing) �Ͼ 
                // ���� ������ �迭 �������� (List<T> Ÿ��)
                // ������ Ÿ�ӿ��� object, ��Ÿ�Ӷ��� List<T>
                object dataArray = method.Invoke(converterInstance, null);

                // ���� Ÿ���� ������ ä JSON���� ��ȯ
                string json = JsonSerialized.ConvertOriginalListToJson(dataArray, type);

                // ���Ϸ� ����
                JsonSerialized.SaveJsonToFile(json, className[i]);

                Debug.Log($"{className[i]} �����͸� ���������� ��ȯ�߽��ϴ�.");
            }

        }
    }
}
