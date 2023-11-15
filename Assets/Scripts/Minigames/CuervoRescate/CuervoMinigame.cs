using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CuervoMinigame: MonoBehaviour {
    [SerializeField]
    private GameObject cuervoRed;
    [SerializeField]
    private GameObject frontCanvas;
    [SerializeField]
    private RawImage backgroundImage;
    [SerializeField]
    private Image[] sideBars;
    [SerializeField]
    private Scrollbar scrollbar;
    [SerializeField]
    private float scrollSpeed, crowSpeedModifier;
    [SerializeField]
    private float tiem, sidebarsUntintSpeed, maxRopeForce, rotSpeed, rotoSpeed;
    [SerializeField, Range(0.1f, 5.5f)]
    private float tintStrength;
    [SerializeField]
    private float maxRotationAngle, rotationSpeedModifier, rotationSpeedDecreaseModifier;
    [SerializeField]
    private float cameraShakeVal = 1.0f;
    [SerializeField]
    private GameObject obstaclesParent;

    private List<SpriteRenderer> obstacleRenderers;
    private Vector3 oMousePos;
    private bool isAlive = true;
    private bool won = false;
    private float currTiem;
    private float yOffset;
    private float curRotationAngle;

    void Start() {
        currTiem = tiem;
        obstacleRenderers = new();
        for(int i=0; i<obstaclesParent.transform.childCount; i++) {
            obstacleRenderers.Add(obstaclesParent.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>());
        }
    }

    private void FixedUpdate() {
        if(!isAlive || won) return;
        currTiem -= Time.deltaTime;

        ColorBlock cb = scrollbar.colors;
        cb.disabledColor = Color.HSVToRGB((currTiem / tiem * 120.0f - 5.0f)/360.0f, 0.666f, 1.0f);
        scrollbar.colors = cb;

        scrollbar.size = currTiem / tiem;

        if(scrollbar.size <= 0.0f) {
            UnAlive();
        }

        float v = Mathf.Sin(Mathf.PI * (tiem - currTiem) / tiem * rotSpeed) * Mathf.Clamp(curRotationAngle, 0.0f, maxRotationAngle);
        cuervoRed.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, 0.0f, v), rotoSpeed * Time.deltaTime);

        curRotationAngle -= rotationSpeedDecreaseModifier * Time.deltaTime;
        if(curRotationAngle < 0.0f) curRotationAngle = 0.0f;
    }

    void Update() {
        if(!isAlive && !won) {
            if(cuervoRed.transform.position.y < -1.5) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            return;
        }
        if(won) {
            cuervoRed.transform.position = Vector3.MoveTowards(cuervoRed.transform.position, new(0, 18), 2.0f * Time.deltaTime);
            if(cuervoRed.transform.position.y >= 17.5f) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if(GetTouchDown()) {
            oMousePos = Input.mousePosition;
        }

        if(Input.GetMouseButton(0)) {
            float yDiff = oMousePos.y - Input.mousePosition.y;
            if(yDiff < 0) {
                yOffset += yDiff * scrollSpeed * Time.deltaTime;
                backgroundImage.uvRect = new(new(yOffset, 0), new(1, 1));

                curRotationAngle += -yDiff * rotationSpeedModifier * scrollSpeed * Time.deltaTime;

                Color tint = new(-yDiff * tintStrength / 255.0f, 0, 0, 1.0f);
                TintStage(tint);

                if(yDiff < -maxRopeForce) {
                    UnAlive();
                }

                cuervoRed.transform.position = Vector3.MoveTowards(cuervoRed.transform.position, cuervoRed.transform.position + new Vector3(0, -yDiff), Time.deltaTime * crowSpeedModifier);
                obstaclesParent.transform.position = Vector3.MoveTowards(obstaclesParent.transform.position, obstaclesParent.transform.position + new Vector3(0, yDiff), Time.deltaTime);
            }
        }else {
            Image sideBar = sideBars[0];
            if(sideBar.color.r > 0.0f) {
                Color tint = new(sideBar.color.r - sidebarsUntintSpeed * Time.deltaTime, 0, 0, 1.0f);
                TintStage(tint);
            }
        }

        if(cuervoRed.transform.position.y >= 15.64f) {
            won = true;
        }

        oMousePos = Input.mousePosition;
    }

    private bool GetTouchDown()=>Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0);

    private void TintStage(Color tint) {
        foreach(Image sideBar in sideBars) {
            sideBar.color = tint;
        }
        obstacleRenderers.ForEach(rndr => rndr.color = tint);
    }

    public void UnAlive() {
        if(!isAlive || won) return;
        StartCoroutine(ShakeCamera());
        Destroy(cuervoRed.transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>());
        isAlive = false;
        Rigidbody2D rb = cuervoRed.AddComponent<Rigidbody2D>();
        rb.AddForce(new(Random.value, 1), ForceMode2D.Impulse);
        
        Color tint = new(0.666f, 0, 0, 1.0f);
        TintStage(tint);
    }

    IEnumerator ShakeCamera() {
        for(int i = 0;i < Random.Range(7, 11);i++) {
            Camera.main.transform.localPosition += new Vector3(Random.value*cameraShakeVal, Random.value*cameraShakeVal, -10);
            frontCanvas.transform.localPosition += new Vector3(Random.value*cameraShakeVal, Random.value*cameraShakeVal);
            yield return new WaitForSecondsRealtime(Random.value*0.05f);
            frontCanvas.transform.localPosition = Vector3.zero;
            Camera.main.transform.localPosition = new Vector3(0, 0, -10);
        }
    }
}
