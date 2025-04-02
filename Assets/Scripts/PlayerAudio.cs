using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class PlayerAudio : MonoBehaviour
{
    
    [System.Serializable]
    public struct AudioSettings
    {
        public float moveVolumeDB;
        public float heartbeatVolumeDB;
    }
    
    [Header("Audio Source")]
    public AudioSource moveAudio;
    public AudioSource heartbeatAudio;
    
    [Header("Audio Clips")]
    public AudioClip walkClip;
    public AudioClip runClip;
    public AudioClip heartbeatClip;
    
    [Header("Audio Mixer")]
    public AudioMixer audioMixer;
    
    [SerializeField] private string moveVolumeParam = "MoveVolume";
    [SerializeField] private string heartbeatVolumeParam = "HeartbeatVolume";
    
    private MoveMode _currentMoveMode;
    private PlayerController _player;

    private readonly Dictionary<MoveMode, AudioSettings> _modeAudioSettings = new Dictionary<MoveMode, AudioSettings>
    {
        { MoveMode.Idle, new AudioSettings { moveVolumeDB = -80f, heartbeatVolumeDB = 0f } },
        { MoveMode.Move, new AudioSettings { moveVolumeDB = 10f, heartbeatVolumeDB = -10f } },
        { MoveMode.Running, new AudioSettings { moveVolumeDB = 0f, heartbeatVolumeDB = -5f } },
        { MoveMode.Slow, new AudioSettings { moveVolumeDB = -50f, heartbeatVolumeDB = 0f } },
    };
    
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
        ApplyAudioSettings(_currentMoveMode);
    }

    private void Update()
    {
        if (_player.moveMode == _currentMoveMode) return;
        _currentMoveMode = _player.moveMode;
        ApplyAudioSettings(_currentMoveMode);
        SwitchFootstepClip(_currentMoveMode);
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

    private void ApplyAudioSettings(MoveMode mode)
    {
        if (!_modeAudioSettings.TryGetValue(mode, out var settings)) return;
        audioMixer.SetFloat(moveVolumeParam, settings.moveVolumeDB);
        audioMixer.SetFloat(heartbeatVolumeParam, settings.heartbeatVolumeDB);
        
        // Debug.Log(mode.ToString());
    }
    
   
}