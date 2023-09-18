using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionCanvas : MonoBehaviour
{
    [SerializeField] Transform characterModelPoint;

    public List<GameObject> charactersToPickFrom;
    public int currentSelectedCharacter = 0;

    public GameObject currentSpawnedCharacter;

    private void Awake()
    {
        currentSpawnedCharacter = Instantiate(charactersToPickFrom[currentSelectedCharacter], characterModelPoint.position, Quaternion.identity);
        currentSpawnedCharacter.transform.RotateAround(currentSpawnedCharacter.transform.position, currentSpawnedCharacter.transform.up, 180f);
    }
}
