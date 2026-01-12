using System;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{

    public EventHandler<OnPhotonsChangedEventArgs> OnPhotonsChanged;
    public class OnPhotonsChangedEventArgs : EventArgs
    {
        public int photonsLeft;
    }



    public static PlayerEvents Singleton {get; private set;}

    void Awake()
    {
        if (Singleton != null) Debug.Log("BIIIIGGGGG ERRRROOOORRR");
        Singleton = this;
    }


}
