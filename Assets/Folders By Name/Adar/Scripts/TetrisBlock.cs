﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;
    private float _previousTime;
    private float _fallTime = 0.7f;
    private const int Height = 12;
    private const int Width = 12;
    private static readonly Transform[,] Grid = new Transform[Width, Height];

    private void OnEnable()
    {
        GameEvents.OnSpeedUp += OnSpeedUp;
    }

    private void OnDisable()
    {
        GameEvents.OnSpeedUp -= OnSpeedUp;
    }

    private void OnSpeedUp()
    {
        if (_fallTime > 0.2f)
            _fallTime -= 0.1f;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if(!ValidMove())
                transform.position -= new Vector3(-1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove())
                transform.position -= new Vector3(1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //rotate !
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1), 90);
            if (!ValidMove())
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
        }


        if (Time.time - _previousTime > (Input.GetKey(KeyCode.DownArrow) ? _fallTime / 10 : _fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                CheckForLines();

                this.enabled = false;
                GameEvents.TetrisSet?.Invoke();
                // FindObjectOfType<SpawnTetromino>().NewTetromino();
                // MyEvents.GameOver();

            }
            _previousTime = Time.time;
        }
    }

    private void CheckForLines()
    {
        for (int i = Height-1; i >= 0; i--)
        {
            if(HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
                GameEvents.OnRowCleared?.Invoke();
            }
        }
    }

    private static bool HasLine(int i)
    {
        for(int j = 0; j< Width; j++)
        {
            if (Grid != null && Grid[j, i] == null)
                return false;
        }

        return true;
    }

    private static void DeleteLine(int i)
    {
        for (int j = 0; j < Width; j++)
        {
            Destroy(Grid[j, i].gameObject);
            Grid[j, i] = null;
        }
    }

    private static void RowDown(int i)
    {
        for (var y = i; y < Height; y++)
        {
            for (var j = 0; j < Width; j++)
            {
                if(Grid[j,y])
                {
                    Grid[j, y - 1] = Grid[j, y];
                    Grid[j, y] = null;
                    Grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }


    private void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            var roundedX = Mathf.RoundToInt(children.transform.position.x);
            var roundedY = Mathf.RoundToInt(children.transform.position.y);
            
            if (roundedY >= Height - 2)
            {
                Debug.Log("Game Over: Block reached the top!");
                GameEvents.GameOver?.Invoke();
            }

            Grid[roundedX, roundedY] = children;
        }
    }

    private bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            var roundedX = Mathf.RoundToInt(children.transform.position.x);
            var roundedY = Mathf.RoundToInt(children.transform.position.y);

            if(roundedX < 0 || roundedX >= Width || roundedY < 0 ||roundedY >= Height)
            {
                return false;
            }

            if (Grid[roundedX, roundedY])
                return false;
        }

        return true;
    }
}
