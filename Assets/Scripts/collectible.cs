using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
public class collectible : MonoBehaviour
{

    public TextMeshProUGUI coinText;

    private int _coins; 
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.transform.tag == "Player")
        {
            Destroy(gameObject);
            
            _coins += 1; 
            
            coinText.text = "Coins: " + _coins.ToString();
        }
        
    }

}
