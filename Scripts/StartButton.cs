using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void OnStartBtn()
    {
        SceneManager.LoadScene("Town1");
    }
}
