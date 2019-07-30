using UnityEngine;

[CreateAssetMenu(menuName = "Create Action", fileName = "Action")]
public class Action : ScriptableObject
{
    public string name;
    public Sprite actionImage;
    public TrafficManager.TrafficLampStatus trafficStatus;
    public bool[] actionEffectDirection = new bool[4];
    /*
     0 : Front of police
     1 : Right of police
     2 : Behind of Police
     3 : Left of police
     */
}
