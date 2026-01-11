using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private SceneAsset nextScene;

    public void loadNextScene()
    {
        SceneManager.LoadScene(nextScene.name);
    }


}
