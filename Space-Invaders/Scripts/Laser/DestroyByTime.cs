using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        Destroy(this.gameObject, 3f);
    }

}
