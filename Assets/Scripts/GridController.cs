using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public static GridController instance;
    Grid grid;
    [SerializeField] PlayerControllerFreeMovement playerController_FreeMovement;
    [SerializeField] PlayerControllerGridMovement playerController_GridMovement;
    [SerializeField] public int numberOfColumns;
    [SerializeField] public int numberOfRows;
    [SerializeField] public int cellSize;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        grid = new(numberOfColumns, numberOfRows, cellSize);
        playerController_GridMovement.transform.position = grid.GetGridPosition(2,3);
        CameraHandler.instance.SetCameraPosition(playerController_GridMovement.transform.position + new Vector3(0, 0, -10));
    }

    private void Update()
    {
        grid.SetValue(playerController_GridMovement.transform.position, 1);

        if (Input.GetMouseButtonDown(0))
        {
            grid.SetValue(UtilsClass.GetMouseWorldPosition(), 1);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPosition()));
            grid.GetXYAndCallFloadFill(UtilsClass.GetMouseWorldPosition());
        }
    }
}
