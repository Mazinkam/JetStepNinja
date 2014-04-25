using UnityEngine;
using System.Collections;

public class PlatformTouch : MonoBehaviour {

    void OnTriggerStay(Collider Hit)
    {
        //print(Hit.name);
        if (Hit.rigidbody != null)
        {
            Debug.Log("touch");
        }
    }
}
