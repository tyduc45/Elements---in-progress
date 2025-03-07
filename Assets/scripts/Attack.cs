using UnityEngine;

public class Attack : MonoBehaviour
{
    PolygonCollider2D attackZone;        // get attack Area

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {

        }
    }
}
