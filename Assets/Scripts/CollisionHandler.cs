using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float deathDelay = 1f;
    [SerializeField] float VictoryDelay = 1f;


    void OnCollisionEnter(Collision other) 
    {
        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("On the Launch Pad");
                break;
            case "Finish":
                runVictorySequence();
                break;                   
            case "Fuel":
                Debug.Log("Refueling...");
                break;
            default: //if the player hits an untagged obstacle they will 'die' with a delay (default of 1) and will disable movement script
                runDeathSequence();
                break;

        }
    }

    void runVictorySequence()
    {
        //todo add SFX upon landing
        //todo add humping annimation upon landing
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", VictoryDelay);
    }
    void runDeathSequence()
    {
        //todo add SFX upon crash
        //todo add humping annimation upon crash
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", deathDelay);
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
