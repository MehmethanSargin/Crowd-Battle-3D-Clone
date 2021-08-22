using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _player;
    private Vector3 offset;
    
    void Start()
    {
        offset =  transform.position -_player.transform.position ;
    }

    
    void LateUpdate()
    {
        transform.position = _player.position + offset;
    }
}
