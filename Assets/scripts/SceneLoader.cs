using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private string sceneNameToLoad; // Name of the scene to load

    public void LoadSceneOnClick()
    {
        SceneManager.LoadScene(sceneNameToLoad);
    }
}
