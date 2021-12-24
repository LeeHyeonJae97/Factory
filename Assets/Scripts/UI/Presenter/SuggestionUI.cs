using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extension;

public class SuggestionUI : MonoBehaviour
{
    [SerializeField] private SuggestionSlot[] _slots;
    [SerializeField] private Canvas _statusCanvas;
    [SerializeField] private Canvas _tabCanvas;
    [SerializeField] private Canvas _agencyCanvas;

    private Canvas _canvas;
    private AgencySO _agency;

    private void Start()
    {
        _canvas = GetComponent<Canvas>();
    }

    public void Suggest(AgencySO agency)
    {
        _agency = agency;
        StartCoroutine(SuggestCoroutine());
    }

    public void Resuggest()
    {
        StartCoroutine(SuggestCoroutine());
    }

    private IEnumerator SuggestCoroutine()
    {
        WaitForSeconds interval = new WaitForSeconds(0.2f);

        ChiefSO[] chiefs = _agency.Suggest(10);

        for (int i = 0; i < _slots.Length; i++)
            _slots[i].gameObject.SetActive(false);
        for (int i = 0; i < chiefs.Length; i++)
        {
            _slots[i].SetInfo(chiefs[i]);
            _slots[i].gameObject.SetActive(true);
            yield return interval;
        }
    }

    public void SetActive(bool value)
    {
        _statusCanvas.SetActive(!value);
        _tabCanvas.enabled = !value;
        _agencyCanvas.enabled = !value;
        _canvas.enabled = value;
    }
}
