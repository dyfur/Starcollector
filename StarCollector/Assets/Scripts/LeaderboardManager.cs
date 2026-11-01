
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    [Header("Leaderboard UI")]
    public TMP_InputField playerNameInput;
    public TextMeshProUGUI leaderboardText;
    public GameObject leaderboardPanel;

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

        if (playerNameInput == null)
        {
            Debug.LogError("PlayerNameInput is not connected!");
            return;
        }

        if (leaderboardText == null)
        {
            Debug.LogError("LeaderboardText is not connected!");
            return;
        }

        if (ScoreManager.Instance == null)
        {
            Debug.LogError("ScoreManager not found!");
            return;
        }

        // Get player name
        string playerName = playerNameInput.text.Trim();

        if (string.IsNullOrEmpty(playerName))
        {
            playerName = "Player";
        }

        // Limit name to 12 characters
        if (playerName.Length > 12)
        {
            playerName = playerName.Substring(0, 12);
        }

        // Get final score
        int playerScore = ScoreManager.Instance.score;

        // Create leaderboard entry
        ScoreEntry newEntry = new ScoreEntry
        {
            playerName = playerName,
            score = playerScore
        };

        leaderboard.entries.Add(newEntry);

        // Sort highest score first
        leaderboard.entries.Sort((a, b) =>
            b.score.CompareTo(a.score));

        // Keep only top 5
        if (leaderboard.entries.Count > 5)
        {
            leaderboard.entries.RemoveRange(
                5,
                leaderboard.entries.Count - 5
            );
        }

        // Save leaderboard
        SaveScores();

        // Show leaderboard panel
        if (leaderboardPanel != null)
        {
            leaderboardPanel.SetActive(true);
        }

        // Update displayed scores
        UpdateLeaderboard();

        // Prevent duplicate submission
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

        Debug.Log("Leaderboard saved successfully!");
    }

    private void LoadScores()
    {
        if (PlayerPrefs.HasKey(SaveKey))
        {
            string json = PlayerPrefs.GetString(SaveKey);

            ScoreList loadedList =
                JsonUtility.FromJson<ScoreList>(json);

            if (loadedList != null)
            {
                leaderboard = loadedList;

                if (leaderboard.entries == null)
                {
                    leaderboard.entries = new List<ScoreEntry>();
                }
            }

            Debug.Log("Leaderboard loaded!");
        }
        else
        {
            Debug.Log("No saved leaderboard found.");
        }
    }

    private void UpdateLeaderboard()
    {
        if (leaderboardText == null)
        {
            Debug.LogError("LeaderboardText is not connected!");
            return;
        }

        // Build leaderboard text
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
        Debug.Log("Leaderboard text: " + leaderboardText.text);
        Debug.Log("Leaderboard active: " +
                  leaderboardText.gameObject.activeInHierarchy);
    }

    // Use this ONCE to delete old test scores
    public void ClearLeaderboard()
    {
        PlayerPrefs.DeleteKey(SaveKey);

        leaderboard.entries.Clear();

        SaveScores();
        UpdateLeaderboard();

        Debug.Log("Old leaderboard scores deleted!");
    }
}
