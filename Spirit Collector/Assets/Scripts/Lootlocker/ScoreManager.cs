using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using LootLocker.Requests;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField playerNameField;
    int leaderboardID = 15355;

    void Start()
    {
        StartCoroutine(LoginRoutine());
    }

    public void SetPlayerName()
    {
        LootLockerSDKManager.SetPlayerName(playerNameField.text, (resoponse) => {
            if (resoponse.success)
            {
                Debug.Log("Successfully set player name");
            }
            else
            {
                Debug.Log("Could not set player name" + resoponse.Error);
            }
        });
    }

    IEnumerator LoginRoutine()
    {
        bool isDone = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (!response.success)
            {
                Debug.Log("error starting LootLocker session");
                isDone = true;
            }
            else
            {
                Debug.Log("successfully started LootLocker session");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                isDone = true;
            }
        });
        yield return new WaitWhile(() => isDone == false);
        yield return SubmitRoutine(CurrentScore.currentScore - Mathf.Abs(CurrentScore.currentDarkSpirits - CurrentScore.currentLightSpirits));
    }

    public IEnumerator SubmitRoutine(int score)
    {
        bool isDone = false;
        string memberID = PlayerPrefs.GetString("PlayerID");
        //LootLockerSDKManager.SubmitScore(memberID, score, leaderboardID, (response) =>
        //{

        //});

        yield return new WaitWhile(() => isDone == false);
    }
}
