using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int width;
    private int height;
    private float cellSize;
    private List<List<int>> gridList;
    private TextMesh[,] debugTextArray;
    public Grid(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        debugTextArray = new TextMesh[width, height];
        gridList = new List<List<int>>();
        for(int x = 0; x < height; x++)
        {
            List<int> row = new(); 
            for(int y = 0; y < width; y++)
            {
                row.Add(0);
            }
            gridList.Add(row);
        }
        
        for(int x = 0; x < gridList.Count; x++)
        {
            for(int y = 0; y < gridList[x].Count; y++)
            {
                debugTextArray[x, y] = UtilsClass.CreateWorldText(gridList[x][0].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 30, Color.white, TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y+1), Color.blue, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x+1, y), Color.blue, 100f); 
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.blue, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.blue, 100f);

        // CameraHandler.instance.cam.transform.position = new Vector3();

        //CameraHandler.instance.SetCameraPosition(GetWorldPosition(width/2, height/2) + new Vector3(0,0,-10));
    }

    private Vector3 GetWorldPosition(float x, float y)
    {
        return new Vector3(x, y) * cellSize;
    }

    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt(worldPosition.x / cellSize);
        y = Mathf.FloorToInt(worldPosition.y / cellSize);
    }

    public void SetValue(int x, int y, int value)
    {
        if(x >= 0 && y >= 0 && x < width && y < height) 
        {
            gridList[x][y] = value;
            debugTextArray[x, y].text = value.ToString();
        }
    }

    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    public int GetValue(int x, int y)
    {
        if(x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridList[x][y];
        }
        else
        {
            return -1;
        }
    }
    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition,out x,out y);
        return GetValue(x, y);
    }
    
    public void GetXYAndCallFloadFill(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        FloadFill(x, y, 1, 0);
    }


    public Vector3 GetGridPosition(int x, int y)
    {
        Vector3 position = new Vector3((x * cellSize)-cellSize/2, (y * cellSize)-cellSize/2);

        return position;
    }

    private void FloadFill(int x, int y, int fillValue, int oldValue)
    {
        if (GetValue(x,y) == 0)
        {
            SetValue(x, y, fillValue);
            FloadFill(x + 1, y, fillValue, oldValue);
            FloadFill(x - 1, y, fillValue, oldValue);
            FloadFill(x, y + 1, fillValue, oldValue);
            FloadFill(x, y - 1, fillValue, oldValue);
        }
    }
}
