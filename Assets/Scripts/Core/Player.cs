using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField] Material _ownMaterial, _trailMaterial;
   
    public Material GetMaterial() { return _ownMaterial; }
    public Material GetTrailMaterial() { return _trailMaterial; }
}