using UnityEngine;

[CreateAssetMenu(fileName = "Location", menuName = "ScriptableObjects/Location")]
public class LocationSO : ScriptableObject
{   
    [TextArea] public string weatherDataURL;
    [TextArea] public string imageURL;
}
