using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Needed to use the intefaces
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    //List of selection boxes
    public Image[] selectionBoxes;
    //List of prefabs
    public GameObject[] prefabs;

    void Start()
    {
        //Desactives the selection boxes
        foreach(var img in this.selectionBoxes)
        {
            img.gameObject.SetActive(false);
        }
        //Selects the first character as default
        this.Select(0);
    }

    //Receives an index and actives the selection box and storages the prefab of the character to spawn with that index, in PlayerStorage
    public void Select(int index)
    {
        //Desactives the selection boxes
        foreach(var img in this.selectionBoxes)
        {
            img.gameObject.SetActive(false);
        }
        //Actives the selection box
        this.selectionBoxes[index].gameObject.SetActive(true);
        //Storages the prefab
        CharacterStorage.characterPrefab = this.prefabs[index];
    }
}
