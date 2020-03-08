using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;
    public Text winText;
    public Text scoreText;
    public Text livesText;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioClip musicClipThree;
    public AudioSource musicSource;

    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;
    Animator anim;

    private int scoreValue = 0;
    private int lives;
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        winText.text = "";
        rd2d = GetComponent<Rigidbody2D>();
        scoreText.text = scoreValue.ToString();
        lives = 3;
        SetScoreText();
        SetLivesText();
        anim = GetComponent<Animator>();
        musicSource.clip = musicClipOne;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 2);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 2);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            anim.SetInteger("State", 2);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.SetInteger("State", 2);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            anim.SetInteger("State", 0);
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue = scoreValue + 1;
            Destroy(collision.collider.gameObject);
            SetScoreText();
        }
        if (scoreValue == 4)
        {
            transform.position = new Vector2(-6.0f, 30.0f);
            lives = 3;
            SetLivesText();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                anim.SetInteger("State", 1);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                anim.SetInteger("State", 1);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
         if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            SetLivesText();
        }
    }

            void SetScoreText()
    {
        scoreText.text = "Score: " + scoreValue.ToString();
        if (scoreValue >= 8)
        {
            winText.text = "You Win! Game Created by Kendrick Rivera.";
            musicSource.Stop();
            musicSource.clip = musicClipTwo;
            musicSource.Play();
        }
    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives == 0)
        {
            winText.text = "You Lose! Game Created by Kendrick Rivera.";
            musicSource.Stop();
            musicSource.clip = musicClipThree;
            musicSource.Play();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

}
