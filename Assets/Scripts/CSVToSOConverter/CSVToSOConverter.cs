using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "CSVToSOConverter", menuName = "ScriptableObject/CSVToSOConverter")]
public class CSVToSOConverter : ScriptableObject
{
    [SerializeField] private string csvPath = "Assets/";
    [SerializeField] private string soPath = "Assets/";
    [SerializeField] private string soClassName;

    [Button("Deserialize", EButtonEnableMode.Editor, upperSpace: 5)]
    public void Deserialize()
    {
        Type type = Type.GetType(soClassName);
        TextAsset ta = AssetDatabase.LoadAssetAtPath<TextAsset>(csvPath);

        // Check header validity
        FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        List<string> headers = CSVSerializer.DeserializeHeader(ta.text);

        //for (int i = 0; i < fields.Length; i++)
        //{
        //    Debug.Log(fields[i]);
        //}

        for (int i = 0; i < headers.Count; i++)
        {
            bool valid = fields.Any((item) => item.Name.Equals(headers[i]));
            if (!valid)
            {
                Debug.LogError($"Wrong header : {headers[i]}");
                return;
            }
        }

        // Convert
        Array array = (Array)CSVSerializer.DeserializeSO(type, ta.text);

        for (int i = 0; i < array.Length; i++)
        {
            object element = array.GetValue(i);
            Debug.Log(i + " " + element);
            AssetDatabase.CreateAsset((UnityEngine.Object)element, soPath + $"/{soClassName}_{i}.asset");
        }
        AssetDatabase.SaveAssets();
    }
}
