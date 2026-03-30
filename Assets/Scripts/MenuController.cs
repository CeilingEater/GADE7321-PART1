using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuController : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public void RestartLevel() //when lives left are 0 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SettingMenu() //i realised this is redundant but ill remove it during part 2
    {
        SceneManager.LoadScene("Settings");
    }
    
    public void QuitGame()
    {
        Debug.Log("Game is exiting...");
        Application.Quit();
    }
}