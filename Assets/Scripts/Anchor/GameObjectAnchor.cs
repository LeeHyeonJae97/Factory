using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "GameObjectAnchor", menuName = "ScriptableObject/Anchor/GameObjectAnchor")]
public class GameObjectAnchor : BaseAnchorSO
{
    [Space(5)] [ReadOnly] [SerializeField] private GameObject _anchored;

    public void Set(GameObject go) => _anchored = go;
    public T Get<T>() where T : MonoBehaviour => _anchored.GetComponent<T>();
}
