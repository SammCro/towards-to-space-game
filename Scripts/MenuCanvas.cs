using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class MenuCanvas : MonoBehaviour
{
    // Update is called once per frame
    [SerializeField] private GameObject canvaMap;
    [SerializeField] private GameObject gameController;
    void Update()
    {
        if (Input.anyKeyDown)
        {

            

            canvaMap.SetActive(true);
            gameController.GetComponent <BannerScript>().Destroyer();
            gameController.GetComponent<BannerScript>().GetBanner(AdPosition.BottomRight);
            gameController.GetComponent<InstNormal>().GetInterBut();
            gameObject.SetActive(false);
        }


    }
}
