using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCassets : MonoBehaviour
{
    public NPCassets Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public Sprite Batholo;
    public Sprite Mordekai;
}
