using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterScript : MonoBehaviour
{

    // External Variables
    [SerializeField] private int speed;
    [SerializeField] private bool isSheCanMove;
    [SerializeField] private GameObject[] forDestroy;
    [SerializeField] private bool isAlive;
    [SerializeField] private float health;
    [SerializeField] private ParticleSystem[] jumpEffects;
    [SerializeField] private float LeftRightSpeed;
    [SerializeField] private float flyspeed;
    [SerializeField] private ParticleSystem flying;
    [SerializeField] private ParticleSystem deathEffect;
    [SerializeField] private GameObject canvasUI;
    [SerializeField] private GameObject Rocket;
    [SerializeField] private GameObject RocketBackground;
    [SerializeField] private List<GameObject> healths;
    [SerializeField] private GameObject playerUI;
    [SerializeField] GameObject deathCanvas;
    [SerializeField] private ParticleSystem deathEffectCharacter;
    [SerializeField] private AudioSource sourceOfAudio;

    [SerializeField] private AudioClip[] AudioClips;

    // Components


    // Internal Variables

    private Rigidbody character;
    private Animator characterAnimator;
    private bool isFlying;
    private bool isRunning;
    private bool isDoubleJump;
    private bool isGrounded;
    private List<GameObject> obstacles;
    private Touch phoneTouch;
    private bool canJump;
    private float xLine;
    private float initialDistance;
    private float initialY;
    private bool isItFirst;

    void Start()
    {


        ReviverLocation();





        isItFirst = true;

        initialY = gameObject.transform.position.y;

        


        xLine = GameObject.FindGameObjectWithTag("FlyLine").transform.position.z;

        initialDistance = xLine + 15f;

        obstacles = GameObject.FindGameObjectsWithTag("obstacle").ToList();

        for (int i = 0; i < obstacles.Count; i++)
        {
            obstacles[i].SetActive(false);
        }

        canJump = true;
        isRunning = true;
        isAlive = true;
        isSheCanMove = false;
        character = GetComponent<Rigidbody>();
        characterAnimator = GetComponent<Animator>();
        Invoke("MoveChecker", 3.5f);
    }

    // Update is called once per frame
    void Update()
    {
        FallChecker();

        if (Rocket != null)
        {
            Rocket.GetComponent<Image>().fillAmount = gameObject.transform.position.z / initialDistance;
        }

        if (isRunning)
        {
            Running();
        }

        if (isFlying)
        {
            Flying();
        }

    }


    void Flying()
    {
        characterAnimator.SetTrigger("IsFly");
        float touchMovement = 0;

        var characterVelocity = character.velocity;
        characterVelocity.z = 1 * flyspeed - characterVelocity.z;
        character.velocity = characterVelocity;

        if (Input.touchCount > 0)
        {
            phoneTouch = Input.GetTouch(0);

            if (phoneTouch.phase == TouchPhase.Moved)
            {
                touchMovement = -1 * phoneTouch.deltaPosition.x / 10;
            }


        }

        

        float translation = touchMovement * -1 * Time.deltaTime * LeftRightSpeed;

        // Move translation along the object's z-axis
        var transformPosition = gameObject.transform.position;
        transformPosition.x += translation;

        gameObject.transform.position = transformPosition;



    }

    void CollSound()
    {
        sourceOfAudio.PlayOneShot(AudioClips[1]);
    }


    void FallChecker()
    {
        if (gameObject.transform.position.y <= initialY - 5)
        {
            DeadChecker();

        }
    }

    void Running()
    {
        ObstacleChecker();

        if (isSheCanMove && isAlive)
        {


            var characterVelocity = character.velocity;
            characterVelocity.z = 1 * speed - characterVelocity.z;
            character.velocity = characterVelocity;


            GroundCheck();

            if (Input.anyKeyDown && canJump)
            {
                if (isGrounded)
                {
                    sourceOfAudio.PlayOneShot(AudioClips[4]);

                    jumpEffects[0].Play();

                    characterAnimator.SetTrigger("Jump");
                    character.AddForce(Vector3.up * 5, ForceMode.Impulse);
                    isDoubleJump = true;

                }
                else
                {
                    if (isDoubleJump)
                    {

                        sourceOfAudio.PlayOneShot(AudioClips[4]);

                        jumpEffects[0].Play();

                        characterAnimator.SetTrigger("DoubleJump");
                        character.AddForce(Vector3.up * 7, ForceMode.Impulse);
                        isDoubleJump = false;
                    }
                }
            }
        }
    }

    void MoveChecker()
    {
        sourceOfAudio.PlayOneShot(AudioClips[0]);
        isSheCanMove = true;
    }

    public void GroundCheck()
    {
        RaycastHit hit;
        float distance = 0.3f;
        Vector3 samet = Vector3.down;


        if (Physics.Raycast(transform.position, samet, out hit, distance))
        {
            isGrounded = true;
        }

        else
        {
            isGrounded = false;
        }

    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("finish"))
        {
            sourceOfAudio.PlayOneShot(AudioClips[3]);

            Rocket.SetActive(false);
            RocketBackground.SetActive(false);

            flying.Play();
            Rigidbody samet = GetComponent<Rigidbody>();
            samet.constraints = RigidbodyConstraints.FreezePositionY;
            samet.constraints = RigidbodyConstraints.FreezeRotation;
            isRunning = false;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<CapsuleCollider>().direction = 2;
            isFlying = true;
        }

        if (coll.gameObject.CompareTag("finishLine"))
        {

            canJump = false;
        }

        if (coll.gameObject.CompareTag("flyObstacle"))
        {
            CollSound();

            health -= 50;
            deathEffect.transform.position = coll.gameObject.transform.position;
            Destroy(coll.gameObject);

            HealthDisabler();


            if (health <= 0)
            {
                DeadChecker();
            }

            deathEffect.Play();

        }

        if (coll.gameObject.CompareTag("obstacle2"))
        {
            CollSound();

            health -= 50;

            HealthDisabler();

            Destroy(coll.gameObject);

            if (health <= 0)
            {
                DeadChecker();
            }
            else
            {
                characterAnimator.SetTrigger("Dodge");
            }
        }

        if (coll.gameObject.CompareTag("finishline2"))
        {
            sourceOfAudio.PlayOneShot(AudioClips[5]);

            if (SceneManager.GetActiveScene().buildIndex != 14)
            {
                string level = "Level" + (SceneManager.GetActiveScene().buildIndex + 1);
                PlayerPrefs.SetInt(level, 1);
            }

            playerUI.SetActive(false);
            canvasUI.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("obstacle"))
        {
            CollSound();

            health -= 50;

            HealthDisabler();


            if (health <= 0)
            {
                DeadChecker();
            }
            else
            {
                characterAnimator.SetTrigger("Dodge");
            }
        }
    }

    void ObstacleChecker()
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            if (obstacles[i].transform.position.z - gameObject.transform.position.z <= 15)
            {
                obstacles[i].SetActive(true);
                Destroy(obstacles[i], 10f);
                obstacles.Remove(obstacles[i]);
            }
        }
    }

    public void ChangeScene(int indexOfScene)
    {
        SceneManager.LoadScene(indexOfScene);
    }

    public void ActiveScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void HealthDisabler()
    {
        healths.Last().SetActive(false);
        healths.Remove(healths.Last());

    }

    void DeadChecker()
    {
        if (isItFirst)
        {
            isItFirst = false;
            speed = 0;
            sourceOfAudio.PlayOneShot(AudioClips[2]);

            deathEffectCharacter.Play();


            isAlive = false;
            for (int i = 0; i < forDestroy.Length; i++)
            {
                Destroy(forDestroy[i]);
            }

            playerUI.SetActive(false);
            deathCanvas.SetActive(true);
        }
    }

    void Revive(Vector3 reviveLoc)
    {
        gameObject.transform.position = reviveLoc;
    }

    void ReviverLocation()
    {
        if (PlayerPrefs.HasKey("revive"))
        {

            int reviveScene= PlayerPrefs.GetInt("revive");

            switch (reviveScene)
            {
                case 11:
                    Revive(new Vector3(2.22000003f, 32.0299988f, 143.720001f));
                    break;
                case 12:
                    Revive(new Vector3(2.30999994f, 125.699997f, 397.640015f));
                    break;
                case 13:
                    Revive(new Vector3(2.11567688f, 133.006256f, 618.23999f));
                    break;
                case 14:
                    Revive(new Vector3(2.11567688f, 133.006256f, 627.72998f));
                    break;
                case 15:
                    Revive(new Vector3(2.11567688f, 136.500443f, 429.709991f));
                    break;
                case 16:
                    Revive(new Vector3(2.22000003f, 32.0299988f, 143.720001f));
                    break;
                case 17:
                    Revive(new Vector3(2.30999994f, 125.699997f, 397.640015f));
                    break;
                case 18:
                    Revive(new Vector3(2.11567688f, 133.006256f, 618.23999f));
                    break;
                case 19:
                    Revive(new Vector3(2.11567688f, 133.006256f, 627.72998f));
                    break;
                case 110:
                    Revive(new Vector3(2.11567688f, 136.500443f, 429.709991f));
                    break;
                case 111:
                    Revive(new Vector3(2.22000003f, 32.0299988f, 143.720001f));
                    break;
                case 112:
                    Revive(new Vector3(2.30999994f, 125.699997f, 397.640015f));
                    break;
                case 113:
                    Revive(new Vector3(2.11567688f, 133.006256f, 618.23999f));
                    break;
                case 114:
                    Revive(new Vector3(2.11567688f, 133.006256f, 627.72998f));
                    break;
                case 115:
                    Revive(new Vector3(2.11567688f, 136.500443f, 429.709991f));
                    break;


            }

                
            PlayerPrefs.DeleteKey("revive");

        }

    }
}
