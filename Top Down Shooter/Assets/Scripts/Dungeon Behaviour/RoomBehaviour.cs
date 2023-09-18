using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class RoomBehaviour : MonoBehaviour
    {
        public GameObject[] walls;  // 0 = up, 1 = down, 2 = right, 3 = left
        public GameObject[] entrances;


        public void UpdateRoom(bool[] status)
        {
            for (int i = 0; i < status.Length; i++)
            {
                entrances[i].SetActive(status[i]);
                walls[i].SetActive(!status[i]);
            }
        }
    }
}

