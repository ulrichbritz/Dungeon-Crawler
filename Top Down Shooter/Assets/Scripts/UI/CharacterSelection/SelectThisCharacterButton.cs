using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class SelectThisCharacterButton : MonoBehaviour
    {
        [SerializeField] CharacterSelectionCanvas characterSelectCanvas;
        [SerializeField] Transform actualPlayerSpawnPoint;

        public void SelectThisCharacter(float seconds) 
        {
            characterSelectCanvas.currentSpawnedCharacter.transform.RotateAround(characterSelectCanvas.currentSpawnedCharacter.transform.position, characterSelectCanvas.currentSpawnedCharacter.transform.up, 180f);
            characterSelectCanvas.currentSpawnedCharacter.GetComponent<Animator>().SetTrigger("selected");

            StartCoroutine(CharacterSelected(seconds));  
        }

        IEnumerator CharacterSelected(float secondsToWait)
        {
            Instantiate(WorldSaveGameManager.instance.charactersList[0], actualPlayerSpawnPoint.position, Quaternion.identity);

            yield return new WaitForSeconds(secondsToWait);

            WorldSceneManager.instance.LoadScene(WorldSceneManager.instance.safeSpaceRoomIndex);
        }
    }
}

