using DefaultNamespace;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] private TextMeshProUGUI _countText;
    
    
    [Header("Movements")]
    [SerializeField] private float swipeValue;
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float maxPosX;
    
    [Header("Crowd")] 
    [SerializeField] private Follower follower;
    [SerializeField] private int _maxCrowdCount;
    
    private Camera cam;
    
    private float _dirX;
    private float _mousePosX;
    
    private CrowdController _crowdController;
    private bool _isMovable = true;
    private int _newCrowdCount;
    private float lastGateTime;

    private void Awake()
    {
        instance = this;
        _crowdController = new CrowdController(follower, _maxCrowdCount, transform);
    }

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (_isMovable)
        {
            SwerveMovement();
            Running();   
        }
        _crowdController.Follow();
    }

    #region Movements
    private void SwerveMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 playerPos = transform.position;
            _dirX = playerPos.x;
            _mousePosX = cam.ScreenToViewportPoint(Input.mousePosition).x;
        }
        if (Input.GetMouseButton(0))
        {
            float newMousePosX = cam.ScreenToViewportPoint(Input.mousePosition).x;
            float distanceX = newMousePosX - _mousePosX;
            float posX = _dirX + (distanceX * swipeValue);
            posX = Mathf.Clamp(posX, -maxPosX, maxPosX);
            Vector3 pos =transform.localPosition;
            pos.x = posX;
            transform.localPosition = pos;
        }
    }
   
    void Running()
    {
        transform.Translate(transform.forward * forwardSpeed * Time.deltaTime, Space.World);
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Gate gate) && Time.time -lastGateTime>.2f)
        {
            lastGateTime = Time.time;
          _crowdController.AddToCrowd(gate._gateType,gate._value);
        }
        else if (other.TryGetComponent(out EnemyCrowdController enemyCrowdController))
        {
            _isMovable = false;
            enemyCrowdController.Remove(_crowdController.CurrentCount);
            Remove(enemyCrowdController.EnemyCount);
        }
        _countText.text = _crowdController.CurrentCount + "";
    }
    
    
    private void Remove(int count)
    {
        _newCrowdCount = _crowdController.CurrentCount - count;
        InvokeRepeating(nameof(RemoveFromCrowd),.25f,.1f);
    }
    
    private void RemoveFromCrowd()
    {
        _crowdController.RemoveFromCrowd();
        _countText.text = _crowdController.CurrentCount + "";
        if (_crowdController.CurrentCount ==1)
        {
            CancelInvoke(nameof(RemoveFromCrowd));
            gameObject.SetActive(false);
            print("GameOver");
            Time.timeScale = 0;
            return;
        }
        if (_newCrowdCount == _crowdController.CurrentCount)
        {
            CancelInvoke(nameof(RemoveFromCrowd));
            _isMovable = true;
        }
    }
}
