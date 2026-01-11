using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Scene nextScene;

    public void loadNextScene()
    {
        SceneManager.LoadScene(nextScene.name);
    }


}
