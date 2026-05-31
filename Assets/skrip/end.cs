using UnityEngine;
using UnityEngine.SceneManagement;

public class end : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}