using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Birb : MonoBehaviour{
    [SerializeField]
    private GameObject pauseScreen, endScreen, winScreen, loadingScreen;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Sprite[] animFrames;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private float rotationSpeed = 4.0f;
    [SerializeField, Range(0.1f, 3.0f)]
    private float animationSpeed = 1.0f;
    [SerializeField, Range(0.1f, 20.0f)]
    private float jumpForce = 1.0f;
    [SerializeField]
    private float minZrot, maxZrot;
    [SerializeField]
    private float cameraShakeVal = 1.0f;
    [SerializeField]
    private string nextSceneName = "";

    public bool isAlive = true;

    private float animProgress = 0.0f;
    private int currSprite = 0;
    private float rotZ = 0.0f;

    // ===== Game progress stuff start =====
    [SerializeField]
    private float gameTime = 15.0f;
    [SerializeField]
    private Slider progressSlider;
    private float currTime;

    void FixedUpdate(){
        if(isAlive && currTime < gameTime) 
            currTime += Time.deltaTime;
        if(currTime >= gameTime) {
            isAlive = false;
            GetComponent<CapsuleCollider2D>().enabled = false;
        }
        progressSlider.value = currTime/gameTime;
    }
    // ===== Game progress stuff end =====

    void Start(){
        Time.timeScale = 0.0f;
        if(maxZrot <= minZrot) maxZrot = minZrot  + 1.0f;
        PhysicsMaterial2D pm = new PhysicsMaterial2D() {
            bounciness = 5
        };
        GetComponent<Rigidbody2D>().sharedMaterial = pm;
    }

    void Update(){
        if(IsTouchDown() && Time.timeScale == 0.0f) {
            if(!isAlive) {
                if(endScreen.activeInHierarchy) {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    return;
                }
                if(winScreen.activeInHierarchy && nextSceneName.Length > 0) {
                    loadingScreen.SetActive(true);
                    SceneManager.LoadScene(nextSceneName);
                    Time.timeScale = 1f;
                    return;
                }
            }
            Time.timeScale = 1.0f;
            pauseScreen.SetActive(false);
            rb.velocity = jumpForce * Vector2.up;
        }
        if(!isAlive) {
            if(currTime < gameTime) {
                transform.Rotate(Vector3.forward, rotationSpeed*rotationSpeed*Time.deltaTime);
                if(transform.position.y < -6.0f || transform.position.y > 6.0f) Time.timeScale = 0.0f;
            }else { 
                rb.velocity += new Vector2(0.1f, 0.25f);
                if(transform.position.y < -6.0f || transform.position.y > 6.0f) {
                    Time.timeScale = 0.0f;
                    winScreen.SetActive(true);
                }
            }
        }
        if(!isAlive || Time.timeScale == 0.0f) return;
        rotZ = Mathf.LerpAngle(rotZ, rotZ+rb.velocity.y, Time.deltaTime * rotationSpeed);
        rotZ = Mathf.Clamp(rotZ, minZrot, maxZrot);
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        if((animProgress += Time.deltaTime) >= animationSpeed) {
            animProgress = 0.0f;
            currSprite = (currSprite+1)%animFrames.Length;
            spriteRenderer.sprite = animFrames[currSprite];
        }
        if(IsTouchDown()) 
            rb.velocity = jumpForce * Vector2.up;
    }

    bool IsTouchDown()=>Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0);

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision == null) return;
        isAlive = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        endScreen.SetActive(true);
        StartCoroutine(ShakeCamera());
    }

    IEnumerator ShakeCamera() {
        for(int i = 0;i < Random.Range(7, 11);i++) {
            Camera.main.transform.localPosition += new Vector3(Random.value*cameraShakeVal, Random.value*cameraShakeVal, -10);
            yield return new WaitForSecondsRealtime(Random.value*0.05f);
            Camera.main.transform.localPosition = new Vector3(0, 0, -10);
        }
    }
}
