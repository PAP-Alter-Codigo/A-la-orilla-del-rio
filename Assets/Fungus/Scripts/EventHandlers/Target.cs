using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Target : MonoBehaviour
{

    private Vector2 followSpot;
    public float speed = 1f;
    public bool inDialogue;
    public bool cutsceneInProgress;
    private NavMeshAgent agent;

    [SerializeField] private GameObject uiCanvas;

	private Vector3 destination;

    [Header("Start position")]
    [SerializeField] private Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        SetPlayerPositionToSpawnpoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (inDialogue == false)
        {
            //posicion del mouse
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButton(0))
            {
                //actualizar la posicion del target
                followSpot = new Vector2(mousePos.x, mousePos.y);
            }
			destination = new Vector3(followSpot.x, followSpot.y, transform.position.z);
            agent.SetDestination(destination);

        }

    }

    public void exitDialogue()
    {
        inDialogue = false;
        cutsceneInProgress = false;

        enableUI();
    }



    public void enterDialogue()
    {
        inDialogue = true;
        cutsceneInProgress = true;

        // // Disable all UI buttons
        disableUI();

    }

    public void disableUI()
    {
        uiCanvas.SetActive(false);
    }

    public void enableUI(){

        uiCanvas.SetActive(true);
    }


    public void SetDestinationTarget()
    {
        agent.SetDestination(transform.position);
    }

    public void dontMove()
    {
        print("before position: " + transform.position);
        agent.SetDestination(transform.position);
        print("middle position: " + transform.position);
        followSpot = transform.position;
        print("after position: " + transform.position);
    }

    public void setFollowSpot(Vector2 position)
    {
        followSpot = position;
    }

    private void SetPlayerPositionToSpawnpoint()
    {
        if (spawnPoint)
        {
            agent.SetDestination(spawnPoint.position);
        }

        followSpot = new Vector2(transform.position.x, transform.position.y);
    }

}
