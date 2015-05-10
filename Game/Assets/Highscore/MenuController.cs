using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuController : MonoBehaviour {

    string name = "";
    string score = "";
    List<Scores> highscore;

    public GUIText yourScoreText;

    void Start() {
        highscore = new List<Scores>();        
    }

    void FixedUpdate() {
        yourScoreText.text = "Your Score: " + HighScoreManager.lastScore;

        highscore = HighScoreManager._instance.GetHighScore();
    }

    void Update() {
        //if (Input.GetKeyDown(KeyCode.H)) {
        //    highscore = HighScoreManager._instance.GetHighScore();
        //}
    }

    void OnGUI() {
        //GUILayout.BeginHorizontal();
        //GUILayout.Label("Name :");
        //name = GUILayout.TextField(name);
        //GUILayout.EndHorizontal();

        //GUILayout.BeginHorizontal();
        //GUILayout.Label("Score :");
        //score = GUILayout.TextField(score);
        //GUILayout.EndHorizontal();

        //if (GUILayout.Button("Add Score")) {
        //    HighScoreManager._instance.SaveHighScore(name, System.Int32.Parse(score));
        //    highscore = HighScoreManager._instance.GetHighScore();
        //}

        //if (GUILayout.Button("Get LeaderBoard")) {
        //    highscore = HighScoreManager._instance.GetHighScore();
        //}

        //if (GUILayout.Button("Clear Leaderboard")) {
        //    HighScoreManager._instance.ClearLeaderBoard();
        //}

        GUILayout.Space(Screen.height / 4);

        GUILayout.BeginHorizontal();
        GUILayout.Space(Screen.width / 3);
        GUILayout.Label("Name", GUILayout.Width(Screen.width / 4));
        GUILayout.Label("Scores", GUILayout.Width(Screen.width / 4));
        GUILayout.EndHorizontal();

        GUILayout.Space(25);

        foreach (Scores _score in highscore) {
            GUILayout.BeginHorizontal();
            GUILayout.Space(Screen.width / 3);
            GUILayout.Label(_score.name, GUILayout.Width(Screen.width / 4));
            GUILayout.Label("" + _score.score, GUILayout.Width(Screen.width / 4));
            GUILayout.EndHorizontal();
        }

        if (GUI.Button(new Rect(Screen.width / 2 + 45, Screen.height - 70, 50, 30), "Retry")) {
            yourScoreText.text = "";
            this.enabled = false;
            Application.LoadLevel("Level1");
        }

        if (GUI.Button(new Rect(Screen.width / 2 - 85, Screen.height - 70, 50, 30), "Clear")) {
            HighScoreManager._instance.ClearLeaderBoard();
            highscore = HighScoreManager._instance.GetHighScore();
        }
    }
}