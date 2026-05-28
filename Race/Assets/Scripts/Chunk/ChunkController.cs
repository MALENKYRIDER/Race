using UnityEngine;

public class ChunkController : MonoBehaviour
{
    public float MoveSpeed = 8f;
    public float MinX;
    public float MinY;
    
    public float Direction;
    
    private void Update()
    {
        float dir = 0;
        
        if (Input.GetKey(KeyCode.D))
        {
            dir = -1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            dir = 1;
        }
        
        Vector3 position = transform.position;
        float previousX = position.x;
        position.x += dir * MoveSpeed * Time.deltaTime;
        position.x = Mathf.Clamp(position.x, MinX, MinY);
        transform.position = position;
        Direction = Mathf.Abs(transform.position.x - previousX) > 0.0001f ? dir : 0;
    }
    
    // public float LaneOffset = 1.5f;
    // public float MoveSpeed = 8f;
    // public int Maxlane = 1;
    //
    // public float Direction;
    //
    // private int _currentLane = 0;
    // private float _startX;
    //
    // private void Start()
    // {
    //     _startX = transform.position.x;
    // }
    //
    // private void Update()
    // {
    //     float dir = 0;
    //     
    //     if (Input.GetKeyDown(KeyCode.D))
    //     {
    //         dir = -1;
    //         _currentLane = Mathf.Max(_currentLane - 1, -Maxlane);
    //     }
    //
    //     if (Input.GetKeyDown(KeyCode.A))
    //     {
    //         dir = -1;
    //         _currentLane = Mathf.Min(_currentLane + 1, Maxlane);
    //     }
    //     
    //     
    //     float targetX = _startX + _currentLane * LaneOffset;
    //     
    //     Vector3 currentPosition = transform.position;
    //     currentPosition.x = Mathf.MoveTowards(currentPosition.x, targetX, MoveSpeed * Time.deltaTime);
    //     float previousX = transform.position.x;
    //     transform.position = currentPosition;
    //     Direction = Mathf.Abs(transform.position.x - previousX) > 0.0001f ? _currentLane : 0;
    // }
}