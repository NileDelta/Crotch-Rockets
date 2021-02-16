using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] float deathDelay = 1f;
    [SerializeField] float VictoryDelay = 1f;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip victorySFX;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem victoryParticles;
    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
        crashParticles.Stop();
        victoryParticles.Stop();        
    }

    void Update() 
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }
    void OnCollisionEnter(Collision other) 
    {
        
        if (isTransitioning || collisionDisabled){return;}
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
                runCrashSequence();
                break;

        }
    }

    void runVictorySequence()
    {
        isTransitioning = true;
        audioSource.PlayOneShot(victorySFX);
        victoryParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", VictoryDelay);
    }
    void runCrashSequence()
    {
        audioSource.PlayOneShot(crashSFX);
        crashParticles.Play();
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
