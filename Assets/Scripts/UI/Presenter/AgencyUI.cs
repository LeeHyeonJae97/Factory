using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgencyUI : MonoBehaviour
{
    [SerializeField] private SuggestionUI _suggestionUI;

    public void Suggest(AgencySO agency)
    {
        _suggestionUI.Suggest(agency);
    }
}
