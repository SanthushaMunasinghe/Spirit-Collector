using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour, IObserver
{
    [SerializeField] Subject _scoreSubject;

    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private TextMeshProUGUI lightSTxt;
    [SerializeField] private TextMeshProUGUI darkSTxt;

    public void OnNotify(int amount, ValueType vType)
    {
        if (vType == ValueType.Score)
        {
            CurrentScore.currentScore += amount;
            scoreTxt.text = $"Score: {CurrentScore.currentScore}";
        }
        else if (vType == ValueType.LightSpiritCount)
        {
            CurrentScore.currentLightSpirits += amount;
            lightSTxt.text = $"Light Spirits: {CurrentScore.currentLightSpirits}";
        }
        else
        {
            CurrentScore.currentDarkSpirits += amount;
            darkSTxt.text = $"Dark Spirits: {CurrentScore.currentDarkSpirits}";
        }
    }

    private void OnEnable()
    {
        _scoreSubject.AttachObserver(this);

        scoreTxt.text = $"Score: {CurrentScore.currentScore}";
        lightSTxt.text = $"Light Spirits: {CurrentScore.currentLightSpirits}";
        darkSTxt.text = $"Dark Spirits: {CurrentScore.currentDarkSpirits}";
    }

    private void OnDisable()
    {
        _scoreSubject.DetachObserver(this);
    }
}
