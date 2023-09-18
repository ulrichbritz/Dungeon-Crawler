using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class DungeonGenerator : MonoBehaviour
    {
        public class Cell
        {
            public bool visited = false;
            public bool[] status = new bool[4];
        }

        public Vector2 size;
        public int startPos = 0;
        public GameObject roomPrefab;
        public Vector2 offset;

        List<Cell> board;

        private void Start()
        {
            MazeGenerator();   
        }

        private void GenerateDungeon()
        {
            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    Cell currentCell = board[Mathf.FloorToInt(i + j * size.x)];

                    //if this cell has been visited, instantiate it
                    if(currentCell.visited == true)
                    {
                        var newRoom = Instantiate(roomPrefab, new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                        newRoom.UpdateRoom(currentCell.status);

                        newRoom.name += " " + i + "-" + j;
                    }
                }
            }
        }

        private void MazeGenerator()
        {
            board = new List<Cell>();

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    board.Add(new Cell());
                }
            }

            int currentCell = startPos;

            Stack<int> path = new Stack<int>();

            int k = 0;

            while(k < 1000)
            {
                k++;

                board[currentCell].visited = true;

                if(currentCell == board.Count - 1)
                {
                    break;
                }

                //check the cell's neighbours
                List<int> neighbours = CheckNeighbours(currentCell);

                if(neighbours.Count == 0)
                {
                    if(path.Count == 0)
                    {
                        break;
                    }
                    else
                    {
                        currentCell = path.Pop();
                    }
                }
                else
                {
                    //if we have neighbours
                    //add the current cell to path
                    path.Push(currentCell);

                    //choose neighbour at random
                    int newCell = neighbours[Random.Range(0, neighbours.Count)];

                    if(newCell > currentCell)
                    {
                        //down or right
                        if(newCell - 1 == currentCell)
                        {
                            //check if going right
                            board[currentCell].status[2] = true;
                            currentCell = newCell;
                            board[currentCell].status[3] = true;
                        }
                        else
                        {
                            board[currentCell].status[1] = true;
                            currentCell = newCell;
                            board[currentCell].status[0] = true;
                        }
                    }
                    else
                    {
                        //up or left
                        if (newCell + 1 == currentCell)
                        {
                            //check if going left
                            board[currentCell].status[3] = true;
                            currentCell = newCell;
                            board[currentCell].status[2] = true;
                        }
                        else
                        {
                            board[currentCell].status[0] = true;
                            currentCell = newCell;
                            board[currentCell].status[1] = true;
                        }
                    }
                }
            }

            GenerateDungeon();
        }

        private List<int> CheckNeighbours(int cellToCheck)
        {
            List<int> neighbours = new List<int>();

            //check up neighbour
            if(cellToCheck - size.x >= 0 && board[Mathf.FloorToInt(cellToCheck - size.x)].visited == false)
            {
                //if space and not visited, add its position
                neighbours.Add(Mathf.FloorToInt(cellToCheck - size.x));
            }

            //check down neighbour
            if (cellToCheck + size.x < board.Count && board[Mathf.FloorToInt(cellToCheck + size.x)].visited == false)
            {
                //if space and not visited, add its position
                neighbours.Add(Mathf.FloorToInt(cellToCheck + size.x));
            }

            //righ neighbour
            if ((cellToCheck + 1) % size.x != 0 && board[Mathf.FloorToInt(cellToCheck +1)].visited == false)
            {
                //if space and not visited, add its position
                neighbours.Add(Mathf.FloorToInt(cellToCheck + 1));
            }


            //left neighbour
            if (cellToCheck % size.x != 0 && board[Mathf.FloorToInt(cellToCheck - 1)].visited == false)
            {
                //if space and not visited, add its position
                neighbours.Add(Mathf.FloorToInt(cellToCheck - 1));
            }

            return neighbours;
        }
    }
}

