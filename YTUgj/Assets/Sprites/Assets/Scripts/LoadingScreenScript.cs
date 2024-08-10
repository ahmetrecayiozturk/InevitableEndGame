using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenScript : MonoBehaviour
{
    public string loadingSceneName = "LoadingScreen"; // LoadingScreen sahnesinin adý
    public float sceneDuration = 5f; // Sahnenin açýk kalma süresi (saniye cinsinden)

    void Start()
    {
        // Belirtilen süre sonunda SwitchScene fonksiyonunu çaðýr
        Invoke("SwitchScene", sceneDuration);
    }

    void SwitchScene()
    {
        // SceneManager ile LoadingScreen sahnesine geçiþ yap
        SceneManager.LoadScene(loadingSceneName);
    }
}
