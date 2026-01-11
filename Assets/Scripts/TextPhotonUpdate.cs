using TMPro;
using UnityEngine;

public class TextPhotonUpdate : MonoBehaviour
{
    void Start()
    {
        PlayerEvents.Singleton.OnPhotonsChanged += UpdatePhotonText_OnPhotonsChanged;
    }

    // Update is called once per frame
    void UpdatePhotonText_OnPhotonsChanged(object sender, PlayerEvents.OnPhotonsChangedEventArgs args)
    {
        GetComponent<TextMeshProUGUI>().text = "<" + args.photonsLeft + " Photons Left>";
    }
}
