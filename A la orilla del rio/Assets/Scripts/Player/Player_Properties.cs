using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player_Properties : MonoBehaviour
{

    public static Player_Properties Instance { get; private set; }

    // Estados
    // Si esta caminando
    public bool isWalking;

    // Decide que sprite carga, si el zorro o el coyote
    public enum PlayableCharacters {FOX, COYOTE};

    public PlayableCharacters currentCharacter;

    // Decide si el jugador puede controlar al zorro
    public enum PlayerStates {AVAILABLE, LOCKED};

    public PlayerStates currentState;

    // Direccion del jugador
    public enum HorizontalDirection {LEFT, RIGHT};
    public enum VerticalDirection {UP, DOWN};

    public HorizontalDirection currentHDirection;
    public VerticalDirection currentVDirection;

    // Checkpoint
    public float checkpointPosX;
    public float checkpointPosY;

    // Inventory, Por ahora es una lista de integers para debuggear, puede ser de objetos o incluso una referencia a un objeto
    public List<int> inventory;


    // Referencia al  navmesh
    private NavMeshAgent nav;

    void Awake()
    {
        if (Instance != null) 
        { 
            Destroy(this.gameObject); 
        } 
        else 
        { 
            Instance = this; 
            DontDestroyOnLoad(this.gameObject);
        }  
    }

    // Start is called before the first frame update
    void Start()
    {
        currentCharacter = PlayableCharacters.FOX;
        currentHDirection = HorizontalDirection.LEFT;
        currentVDirection = VerticalDirection.DOWN;
        currentState = PlayerStates.AVAILABLE;

        checkpointPosX = 0.0f;
        checkpointPosX = 0.0f;


        isWalking = false;
        inventory = new List<int>();

        nav = this.gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        print(nav.steeringTarget);
        if (nav.velocity != Vector3.zero){
            isWalking = true;
        }else{
            isWalking = false;
        }
        //X
        if(nav.velocity[0] > 0) currentHDirection = HorizontalDirection.RIGHT;
        else if(nav.velocity[0] < 0) currentHDirection = HorizontalDirection.LEFT;
        //Y
        if(nav.velocity[1] > 0) currentVDirection = VerticalDirection.UP;
        else if(nav.velocity[1] < 0) currentVDirection = VerticalDirection.DOWN;
    }
}
