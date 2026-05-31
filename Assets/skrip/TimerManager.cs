using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public float timeLeft = 60f;
    public TMP_Text timerText;

    private bool isRunning = true;

    void Update()
    {
        if (!isRunning) return;

        timeLeft -= Time.deltaTime;

        // update UI
        timerText.text = Mathf.Ceil(timeLeft).ToString();

        if (timeLeft <= 0)
        {
            timeLeft = 0;
            isRunning = false;

            EndGame();
        }
    }

    void EndGame()
    {
        // bisa tambah efek dulu kalau mau
        SceneManager.LoadScene("MainMenu");
    }
}