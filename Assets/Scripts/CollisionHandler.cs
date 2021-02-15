using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    
    void OnCollisionEnter(Collision other) 
    {
        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("On the Launch Pad");
                break;
            case "Finish":
                Debug.Log("On the Landing Pad");
                break;            
            case "Respawn":
                Debug.Log("Hit an obstacle");
                break;            
            case "Fuel":
                Debug.Log("Refueling...");
                break;
            default:
                Debug.Log("Flying - All systems good!");
                break;

        }
    }
}
