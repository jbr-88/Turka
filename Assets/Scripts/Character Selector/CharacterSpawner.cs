using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    void Start()
    {
        //Instantiate the prefab in CharacterStorage in spawner's position, with spawner's rotation
        Instantiate(CharacterStorage.characterPrefab, this.transform.position, this.transform.rotation);
        //Destroys the spawner
        Destroy(this.gameObject);
    }
}
