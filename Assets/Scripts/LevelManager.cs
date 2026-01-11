using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Scene nextScene;

    public void loadNextScene()
    {
        SceneManager.LoadScene(nextScene.name);
    }


}
