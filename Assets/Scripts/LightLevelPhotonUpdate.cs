using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightLevelPhotonUpdate : MonoBehaviour
{
    [SerializeField] private float photonStrength;
    [SerializeField] private float minLigth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerEvents.Singleton.OnPhotonsChanged += UpdateLightLevel_OnPhotonsChanged;
    }

    // Update is called once per frame
    void UpdateLightLevel_OnPhotonsChanged(object sender, PlayerEvents.OnPhotonsChangedEventArgs args)
    {
        GetComponent<Light2D>().intensity = args.photonsLeft * photonStrength + minLigth;
    }
}
