using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject canvasMap;



    public void SetMap()
    {
        canvasMap.SetActive(true);
    }
}
