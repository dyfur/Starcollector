
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    [Header("Leaderboard UI")]
    public TMP_InputField playerNameInput;
    public TextMeshProUGUI leaderboardText;

    private const string SaveKey = "Leaderboard";

    private bool scoreSubmitted = false;

    [Serializable]
    public class ScoreEntry
    {
        public string playerName;
        public int score;
    }

    [Serializable]
    public class ScoreList
    {
        public List<ScoreEntry> entries = new List<ScoreEntry>();
    }

    private ScoreList leaderboard = new ScoreList();

    private void Start()
    {
        LoadScores();
        UpdateLeaderboard();
    }

    public void SubmitCurrentScore()
    {
        Debug.Log("Submit button clicked!");

        if (scoreSubmitted)
        {
            Debug.Log("Score already submitted!");
            return;
        }

        if (playerNameInput == null || leaderboardText == null)
        {
            Debug.LogError("Leaderboard UI is not connected!");
            return;
        }

        if (ScoreManager.Instance == null)
        {
            Debug.LogError("ScoreManager not found!");
            return;
        }

        string playerName = playerNameInput.text.Trim();

        if (string.IsNullOrEmpty(playerName))
        {
            playerName = "Player";
        }

        int playerScore = ScoreManager.Instance.score;

        ScoreEntry newEntry = new ScoreEntry
        {
            playerName = playerName,
            score = playerScore
        };

        leaderboard.entries.Add(newEntry);

        leaderboard.entries.Sort((a, b) =>
            b.score.CompareTo(a.score));

        if (leaderboard.entries.Count > 5)
        {
            leaderboard.entries.RemoveRange(
                5,
                leaderboard.entries.Count - 5
            );
        }

        SaveScores();
        UpdateLeaderboard();

        scoreSubmitted = true;

        playerNameInput.interactable = false;

        Debug.Log("Score saved: " + playerName +
                  " - " + playerScore);
    }

    private void SaveScores()
    {
        string json = JsonUtility.ToJson(leaderboard);

        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
    }

    private void LoadScores()
    {
        if (PlayerPrefs.HasKey(SaveKey))
        {
            string json = PlayerPrefs.GetString(SaveKey);

            ScoreList loadedList =
                JsonUtility.FromJson<ScoreList>(json);

            if (loadedList != null)
                leaderboard = loadedList;
        }
    }

    private void UpdateLeaderboard()
    {
        if (leaderboardText == null)
        {
            Debug.LogError("LeaderboardText is not connected!");
            return;
        }

        leaderboardText.gameObject.SetActive(true);

        string displayText = "HIGH SCORES\n\n";

        for (int i = 0; i < leaderboard.entries.Count; i++)
        {
            ScoreEntry entry = leaderboard.entries[i];

            displayText +=
                (i + 1) + ". " +
                entry.playerName + " - " +
                entry.score + " points\n";
        }

        if (leaderboard.entries.Count == 0)
        {
            displayText += "No scores yet!";
        }

        leaderboardText.text = displayText;

        Debug.Log("Leaderboard updated!");
    }
}
