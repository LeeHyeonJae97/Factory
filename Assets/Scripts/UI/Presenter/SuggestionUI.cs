using Extension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuggestionUI : MonoBehaviour
{
    [SerializeField] private SuggestionSlot[] _slots;

    private Canvas _canvas;

    private void Start()
    {
        _canvas = GetComponent<Canvas>();
    }

    public void Suggest(AgencySO agency)
    {
        for (int i = 0; i < _slots.Length; i++)
            _slots[i].gameObject.SetActive(false);

        StartCoroutine(SetInfoCoroutine(agency.Suggest(10)));
    }

    private IEnumerator SetInfoCoroutine(Chief[] chiefs)
    {
        WaitForSeconds interval = new WaitForSeconds(0.2f);

        for (int i = 0; i < chiefs.Length; i++)
        {
            _slots[i].SetInfo(chiefs[i]);
            _slots[i].gameObject.SetActive(true);
            yield return interval;
        }
    }

    public void SetActive(bool value)
    {
        _canvas.SetActive(value);
    }
}
