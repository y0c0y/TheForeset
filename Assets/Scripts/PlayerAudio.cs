using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class PlayerAudio : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource moveAudio;
    public AudioSource heartbeatAudio;
    
    [Header("Audio Clips")]
    public AudioClip walkClip;
    public AudioClip runClip;
    public AudioClip heartbeatClip;
    
    [Header("Audio Mixer")]
    public AudioMixer audioMixer;
    
    [Header("Audio Mixer Snapshot")]
    public AudioMixerSnapshot idleSnapshot;
    public AudioMixerSnapshot walkSnapshot;
    public AudioMixerSnapshot runSnapshot;
    public AudioMixerSnapshot slowSnapshot;
    public float transitionTime = 0.5f;
    
    // [SerializeField] private string moveVolumeParam = "MoveVolume";
    // [SerializeField] private string heartbeatVolumeParam = "HeartbeatVolume";
    
    private MoveMode _currentMoveMode;
    private PlayerController _player;

    public void ApplySnapshot(MoveMode mode)
    {
        switch (mode)
        {
            case MoveMode.Idle:
                idleSnapshot.TransitionTo(transitionTime);
                break;
            case MoveMode.Move:
                walkSnapshot.TransitionTo(transitionTime);
                break;
            case MoveMode.Running:
                runSnapshot.TransitionTo(transitionTime);
                break;
            case MoveMode.Slow:
                slowSnapshot.TransitionTo(transitionTime);
                break;
            default:
                break;
        }
    }
    
    private void Awake()
    {
        if (!moveAudio || !heartbeatAudio || !audioMixer)
        {
            Debug.LogError("오디오 소스나 AudioMixer 참조가 누락되었습니다!");
        }
    }
    private void Start()
    {
        _player = PlayerController.Instance;
        if (_player == null)
        {
            Debug.LogError("플레이어 싱글톤이 초기화되지 않았습니다!");
        }

        _currentMoveMode = _player.moveMode;
        ApplySnapshot(_currentMoveMode);
    }

    private void Update()
    {
        if (_player.isDead)
        {
            heartbeatAudio.Stop();
            moveAudio.Stop();
            // moveAudio.Stop();
        }
        if (_player.moveMode == _currentMoveMode) return;
        _currentMoveMode = _player.moveMode;
        SwitchFootstepClip(_currentMoveMode);
        ApplySnapshot(_currentMoveMode);
    }
    
    private void SwitchFootstepClip(MoveMode mode)
    {
        switch (mode)
        {
            case MoveMode.Idle:
                moveAudio.clip = null;
                moveAudio.Stop();
                break;
            case MoveMode.Slow:
            case MoveMode.Move:
                moveAudio.clip = walkClip;
                if (!moveAudio.isPlaying) moveAudio.Play();
                break;
            case MoveMode.Running:
                moveAudio.clip = runClip;
                if (!moveAudio.isPlaying) moveAudio.Play();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
        }
    }
}