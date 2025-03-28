using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class ObjectC : MonoBehaviour, IObjectCColliderHandler
    {
        public GameObject barricade;
        public GameObject leafBarricade;
        public GameObject bush;
        public GameObject enemy;
        
        
        
        public void OnPlayerInteraction(Collider other)
        {
            leafBarricade.SetActive(true);
            bush.SetActive(false);
            if (enemy)
            {
                enemy.GetComponent<Enemy>().Instance.IsChasing = false;
            }
        }

        public void OnPlayerExitInteraction(Collider other)
        {
            leafBarricade.SetActive(false);
            bush.SetActive(true);
            if (enemy)
            {
                enemy.GetComponent<Enemy>().Instance.IsChasing = true;
            }
            
        }

        public void OnPlayerHide()
        {
            

        }
    }

