using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class SpawnTetromino : MonoBehaviour
// {
//     // private 
//     // [SerializeField] private GameObject ComponentFather;
//     public GameObject[] Tetrominoes;
//
//     // Start is called before the first frame update
//     void Start()
//     {
//         NewTetromino();
//     }
//
//     public void NewTetromino()
//     {
//         // instantiate a new tetromino at the 
//         Instantiate(Tetrominoes[Random.Range(0, Tetrominoes.Length)], transform.position, Quaternion.identity);
//     }
// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTetromino : MonoBehaviour
{
    // Array of tetromino prefabs. The order should correspond to your Products enum.
    public GameObject[] Tetrominoes;

    // Reference to the ProductDeque. You can assign this in the inspector.
    public ProductDeque productDeque;

    // Flag to indicate if we are waiting for a product to spawn a tetromino.
    private bool waitingForTetromino = true;

    void OnEnable()
    {
        GameEvents.TetrisSet += OnTetrisSet;
        // Subscribe to the event when a product is collected.
    }

    void OnDisable()
    {
        // Unsubscribe from the event when the object is disabled.
        GameEvents.TetrisSet -= OnTetrisSet;
    }


    private void Update()
    {
        // Check if the deque has products and we are waiting for a tetromino.
        if (waitingForTetromino && productDeque != null && productDeque.Count > 0)
        {
            NewTetromino();
        }
    }

    void Start()
    {
        NewTetromino();
    }

    public void NewTetromino()
    {
        if (productDeque != null && productDeque.Count > 0)
        {
            // Get the next product from the deque.
            Products nextProduct = productDeque.PopFront();

            // Convert the product enum to an index (assuming your Tetrominoes array is in the same order).
            int index = (int)nextProduct;
            if (index >= 0 && index < Tetrominoes.Length)
            {
                Instantiate(Tetrominoes[index], transform.position, Quaternion.identity);
                Debug.Log($"Spawned Tetromino: {Tetrominoes[index].name}");
                waitingForTetromino = false;
            }
            else
            {
                Debug.LogError("Product index out of range for Tetrominoes array.");
            }
        }
        else
        {
            Debug.Log("No product available in the deque. Waiting for product.");
            // SetWaitingForTetromino
            // waitingForTetromino = false;
        }
    }

    public void OnTetrisSet()
    {
        waitingForTetromino = true;
    }
}