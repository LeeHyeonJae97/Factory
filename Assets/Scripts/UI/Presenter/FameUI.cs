using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Image _expFillImage;

    [SerializeField] private FameSO _fame;

    private void OnEnable()
    {
        _fame.onLevelValueChanged += UpdateLevelUI;
        _fame.onExpRatioValueChanged += UpdateExpUI;
    }

    private void OnDisable()
    {
        _fame.onLevelValueChanged -= UpdateLevelUI;
        _fame.onExpRatioValueChanged -= UpdateExpUI;
    }

    public void UpdateLevelUI(int level)
    {
        _levelText.text = $"{_fame.Level}";
    }

    public void UpdateExpUI(float progress)
    {
        _expFillImage.fillAmount = progress;
    }
}
