using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void loadNextScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
