using TMPro;
using UnityEngine;

    public class ObjectC : MonoBehaviour, IObjectCColliderHandler
    {
        public GameObject barricade;
        public GameObject leafBarricade;
        
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        
        
        
        void Start()
        {
           // barricade = GetComponent<GameObject>();
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    
        private void OnTriggerStay(Collider other)
        {
     
        }

        public void OnPlayerInteraction(Collider other)
        {
            if (Input.GetKey(KeyCode.F))
            {
                Debug.Log("Player interaction");
                leafBarricade .SetActive(true);
                barricade.SetActive(false);
            }
        }

        public void OnPlayerExitInteraction(Collider other)
        {
            if (Input.GetKey(KeyCode.F))
            {
                leafBarricade.SetActive(false);
                barricade.SetActive(true);
            }
            
        }

        public void OnPlayerHide(Collider other)
        {
            // Debug.Log(barricadeCollider.name);
        }
    }

