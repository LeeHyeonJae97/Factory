using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private TaskUI _taskUI;

    private Task _task;
    private int _typeLength;

    private void Awake()
    {
        _typeLength = System.Enum.GetValues(typeof(TaskType)).Length;
    }

    private void Start()
    {
        Generate();
    }

    public void Handle(int type)
    {
        if (_task.Type == (TaskType)type)
        {
            // Gain AbilityPoint
            StatPoint.Get(StatType.AbilityPoint).Gain();

            // Effect

            // Handle task
            _task.Handled();

            // Generate next task
            Generate();
        }
        else
        {
            Debug.Log("Fail Handling");

            // Effect
        }
    }

    private void Generate()
    {
        _task = PoolingManager.Spawn("Task", transform).GetComponent<Task>();
        _task.Generated((TaskType)Random.Range(0, _typeLength), _taskUI);
    }
}
