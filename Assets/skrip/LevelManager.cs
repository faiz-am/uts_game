using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadFinish()
    {
        SceneManager.LoadScene("Finish");
    }
}