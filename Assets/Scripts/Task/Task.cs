using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public enum TaskType { A, B, C, D }

public class Task : MonoBehaviour
{
    [SerializeField] private Color[] _colors;
    [SerializeField] private Vector2 _startPos;
    [SerializeField] private Vector2 _middlePos;
    [SerializeField] private Vector2 _endPos;
    [SerializeField] private float _duration;
    [SerializeField] private SpriteRenderer _sr;

    public TaskType Type { get; private set; }    

    public void Generated(TaskType type, TaskUI taskUI)
    {
        // set type and color
        Type = type;
        _sr.color = _colors[(int)type];

        taskUI.SetInteractable(false);

        gameObject.SetActive(true);
        transform.position = _startPos;

        Tweener tweener = transform.DOMove(_middlePos, _duration).SetEase(Ease.Linear);
        tweener.onComplete += () => taskUI.SetInteractable(true);
    }

    public void Handled()
    {
        transform.DOMove(_endPos, _duration).onComplete += () => PoolingManager.Despawn(gameObject);
    }
}
