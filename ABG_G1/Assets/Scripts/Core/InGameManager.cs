using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : Singleton<InGameManager>
{
    [SerializeField] public GameObject player;
    protected override void Initialize()
    {
        base.Initialize();

    }
    void Start()
    {
        
    }
    void Update()
    {
    }
}
