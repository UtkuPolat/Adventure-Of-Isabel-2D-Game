using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterControl : MonoBehaviour
{
    public Sprite[] idleAnimation;
    public Sprite[] walkAnimation;
    public Sprite[] jumpAnimation;
    public Text healthText;
    public Text CoinsText;
    public Image BlackBackground;
    int health=100;
    
    int idleAnimationCounter = 0;
    int walkAnimationCounter = 0;
    int coinscount = 0;

    SpriteRenderer spriteRenderer;
    Rigidbody2D physc;
    Vector3 vector;
    Vector3 camFirstPos;
    Vector3 camLastPos;
    float horizontal = 0;
    float idleAnimationTime = 0;
    float walkAnimationTime = 0;
    float blackbackgroundcount = 0;
    bool jumpFlag = true;
    float backtomenu = 0;

    GameObject mainCamera;
    void Start()
    {
        Time.timeScale = 1;
        spriteRenderer = GetComponent<SpriteRenderer>();
        physc = GetComponent<Rigidbody2D>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        
        if (SceneManager.GetActiveScene().buildIndex > PlayerPrefs.GetInt("whichlevel"))
        {
            PlayerPrefs.SetInt("whichlevel", SceneManager.GetActiveScene().buildIndex);
        }
        camFirstPos = mainCamera.transform.position - transform.position;
        healthText.text = "Health:   "+health;
        CoinsText.text = " Coins:30 - " + coinscount;
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            if (jumpFlag) 
            {
                physc.AddForce(new Vector2(0,750));
                jumpFlag = false;
            }
            
        }
    }
    
    private void LateUpdate() 
    {
        CameraControl();
    }
    private void FixedUpdate() 
    {
        Animation();
        CharacterMovement();
        if (health <= 0)
        {
            Time.timeScale = 0.4f;
            healthText.enabled = false;
            blackbackgroundcount += 0.03f;
            BlackBackground.color = new Color(0,0,0, blackbackgroundcount);
            backtomenu += Time.deltaTime;
            if (backtomenu > 1)
            {
                SceneManager.LoadScene("mainmenu");
            }
        }
    }

    void CharacterMovement() 
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vector = new Vector3(horizontal*10, physc.velocity.y, 0); //D tuşuna basılınca y ekseninde 1*10 olarak hareket ediyor
        physc.velocity = vector; 
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        jumpFlag = true;
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "bullet")
        {
            health--;
            healthText.text = "Health:    "+health;
        }
        if (other.gameObject.tag == "enemy")
        {
            health -= 10;
            healthText.text = "Health:    "+health;
        }
        if (other.gameObject.tag == "saw")
        {
            health -= 10;
            healthText.text = "Health:    "+health;
        }
        if (other.gameObject.tag == "levelfinish")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
        if (other.gameObject.tag == "givehealth")
        {
            health += 10;
            healthText.text = "Health:    " + health;
            other.GetComponent<BoxCollider2D>().enabled = false;
            other.GetComponent<give_a_health>().enabled = true;
            Destroy(other.gameObject,3);
        }

        if (other.gameObject.tag == "coins")
        {
            coinscount++;
            CoinsText.text = "Coins:30 - " + coinscount;
            //Debug.Log(coinscount++);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "water")
        {
            health = 0;
        }
        if (other.gameObject.tag == "dead")
        {
            health = 0;
        }
        if (other.gameObject.tag == "Finish")
        {
            health = 0;
        }
    }



    void Animation() 
    {
        if (jumpFlag) 
        {
            if (horizontal == 0) 
            {
                idleAnimationTime += Time.deltaTime;
                if (idleAnimationTime > 0.05f)
                {
                    spriteRenderer.sprite = idleAnimation[idleAnimationCounter++];
                    if (idleAnimationCounter == idleAnimation.Length) 
                    {
                        idleAnimationCounter = 0;
                    }
                    idleAnimationTime = 0;
                }
            }
            else if (horizontal > 0)
            {
            walkAnimationTime += Time.deltaTime;
            if (walkAnimationTime > 0.01f)
            {
                spriteRenderer.sprite = walkAnimation[walkAnimationCounter++];
                if (walkAnimationCounter == walkAnimation.Length) 
                {
                    walkAnimationCounter = 0;
                }
                walkAnimationTime = 0;
            }
            transform.localScale = new Vector3(1,1,1);
            }
            else if (horizontal < 0)
            {
                walkAnimationTime += Time.deltaTime;
                if (walkAnimationTime > 0.01f)
                {
                    spriteRenderer.sprite = walkAnimation[walkAnimationCounter++];
                    if (walkAnimationCounter == walkAnimation.Length) 
                    {
                     walkAnimationCounter = 0;
                    }
                    walkAnimationTime = 0;
                }
                transform.localScale = new Vector3(-1,1,1);
            }
        }
        else 
        {
            if (physc.velocity.y > 0)
            {
                spriteRenderer.sprite = jumpAnimation[0];
            }
            else
            {
                spriteRenderer.sprite = jumpAnimation[1];
            }
            if (horizontal > 0) 
            {
                transform.localScale = new Vector3(1,1,1);
            }
            else if (horizontal < 0)
            {
                transform.localScale = new Vector3(-1,1,1);
            }
        }
    }
    void CameraControl () 
    {
        camLastPos = camFirstPos + transform.position;
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, camLastPos, 0.01f);
    }
}
