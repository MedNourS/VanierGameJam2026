using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private SceneAsset nextScene;
    [SerializeField] private SceneAsset previousScene;

    public void loadNextScene()
    {
        SceneManager.LoadScene(nextScene.name);
    }
}
