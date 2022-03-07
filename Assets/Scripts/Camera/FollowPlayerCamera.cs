using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    [SerializeField] private GameObject _playerPos;

    private void Update()
    {
        this.transform.position = new Vector3(
            _playerPos.transform.position.x,
            _playerPos.transform.position.y,
            transform.position.z
        );
    }
}