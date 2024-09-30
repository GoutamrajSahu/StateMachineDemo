using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerGridMovement : MonoBehaviour
{
    //State 1 => For NormalMovement
    //State 2 => For DirectionChange

    public static PlayerControllerGridMovement instance;
    //[SerializeField] public bool isChangingDirection;
    //[SerializeField] public bool isMoving;
    [SerializeField] public float changingDirectionTime = 0.2f;
    [SerializeField] public float timeToMove = 3f;
    [SerializeField] List<Vector3> inputDirectionsBufferStorage;
    [SerializeField] public Vector3 currentDirection;
    [SerializeField] public PlayerStateManager player;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        player = GetComponent<PlayerStateManager>();
        int i = Random.Range(0, 3);
        if(i == 0)
        {
            AddInputDirectionBuffer(Vector3.up);
        }
        if(i == 1)
        {
            AddInputDirectionBuffer(Vector3.left);
        }
        if (i == 2)
        {
            AddInputDirectionBuffer(Vector3.down);
        }
        if (i == 3)
        {
            AddInputDirectionBuffer(Vector3.right);
        }
        player.SwitchState(player.changingDirectionState);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            AddInputDirectionBuffer(Vector3.up);
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            AddInputDirectionBuffer(Vector3.left);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            AddInputDirectionBuffer(Vector3.down);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            AddInputDirectionBuffer(Vector3.right);
        }
    }

    public void AddInputDirectionBuffer(Vector3 direction)
    {
        inputDirectionsBufferStorage.Add(direction);
    }
    public Vector3 GetAndDeleteNextBufferDirection()
    {
        if(inputDirectionsBufferStorage.Count == 0)
        {
            return Vector3.zero;
        }
        else
        {
            Vector3 tmp = inputDirectionsBufferStorage[0];
            inputDirectionsBufferStorage.RemoveAt(0);
            return tmp;
        }
    }
    public int GetInputDirectionsBufferStorageLength()
    {
        return inputDirectionsBufferStorage.Count;
    }
}
