using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemRoot : MonoBehaviour
{
    protected ResService resSvc;
    protected AudioService audioSvc;

    public virtual void InitSystem()
    {
        resSvc = ResService.Instance;
        audioSvc = AudioService.Instance;
    }
}