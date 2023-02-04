using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float timeDelay = 1f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip successSound;
    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] ParticleSystem successParticle;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisonDisable = false;

    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        DebugKeys();
    }

    void DebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            int nextscene = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextscene == SceneManager.sceneCountInBuildSettings)
            {
                nextscene = 0;
            }
            SceneManager.LoadScene(nextscene);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            //Toggles variable
            collisonDisable = !collisonDisable;
        }
    }

    void OnCollisionEnter(Collision other) {
        if(isTransitioning || collisonDisable) return;

        switch(other.gameObject.tag){
            case "Friendly":
                Debug.Log("You're okay!");
                break;
            case "Finish":
                Victory();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence(){
        isTransitioning = true;
        deathParticle.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(deathSound);
        GetComponent<Movement>().enabled = false;
        DelayScene("ReloadScene", timeDelay);
    }

    void Victory(){
        isTransitioning = true;
        successParticle.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        GetComponent<Movement>().enabled = false;
        DelayScene("NextScene", timeDelay);
    }

    void DelayScene(string method, float time){
        Invoke(method, time);
    }

    void ReloadScene(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void NextScene(){
        int sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(sceneIndex < SceneManager.sceneCountInBuildSettings){
            SceneManager.LoadScene(sceneIndex);
        }else{
            SceneManager.LoadScene(0);
        }
    }
}
