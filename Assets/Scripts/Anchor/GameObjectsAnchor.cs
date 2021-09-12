using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Linq;

[CreateAssetMenu(fileName = "GameObjectsAnchor", menuName = "ScriptableObject/Anchor/GameObjectsAnchor")]
public class GameObjectsAnchor : BaseAnchorSO
{
    [Space(5)] [ReadOnly] [SerializeField] private List<GameObject> _anchored;

    public void Set(GameObject go) => _anchored.Add(go);
    public List<T> Get<T>() where T : MonoBehaviour => new List<T>(_anchored.Cast<T>());
}
