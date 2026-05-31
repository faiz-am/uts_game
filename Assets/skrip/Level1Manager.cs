using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Manager : MonoBehaviour
{
    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }
}