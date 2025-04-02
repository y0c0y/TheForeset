using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class ObjectC : MonoBehaviour, IObjectCColliderHandler
{
    
    public GameObject bush;
    private GameObject _leafBarricade;
    private Enemy _enemy;
    private Transform _player;
    
    public AudioSource bushAudio;
    public AudioClip bushInClip;
    public AudioClip bushOutClip;
    
    private void Start()
    {
        _enemy = Enemy.Instance;
        _player = PlayerController.Instance.transform;
        FindLeafBarricade(_player);
    }

    private void FindLeafBarricade(Transform player)
    {
        if (!player) return;
        var playerLook = player.Find("PlayerLook");
        
        if (playerLook == null) return;
        var leafBarricade = playerLook.Find("LeafBarricade");

        if (leafBarricade == null) return;
        _leafBarricade =  leafBarricade.gameObject;
    }

    private void ChangeAudio(bool hide)
    {
        var tmpClip = hide ? bushInClip : bushOutClip;
        bushAudio.PlayOneShot(tmpClip);
    }
        
    public void OnPlayerInteraction(Collider other)
    {
        OnPlayerHide(true);
    }

    public void OnPlayerExitInteraction(Collider other)
    {
        OnPlayerHide(false);
    }
    
    private void OnPlayerHide(bool hide)
    {
        ChangeAudio(hide);
        _leafBarricade.SetActive(hide);
        bush.SetActive(!hide);
            
        if (!_enemy) return;
            
        if (hide)
        {
            Debug.Log("Hide in Bush");
            _enemy.OnIsChasing(false);
        }
        else
        {
            _enemy.OnIsChasing(true);
        }
    }
}