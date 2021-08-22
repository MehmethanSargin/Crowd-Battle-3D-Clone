using DefaultNamespace;
using TMPro;
using UnityEngine;

public class EnemyCrowdController : MonoBehaviour
{
    public int EnemyCount => _enemyCount;
    
    [SerializeField] private Follower _enemyFollower;
    [SerializeField] private int _enemyCount;
    [SerializeField] private TextMeshProUGUI countText;
    
    private CrowdController _enemyCrowd;
    

    private int newEnemeyCount;
    private void Awake()
    {
        _enemyCrowd = new CrowdController(_enemyFollower, _enemyCount, transform);
    }

    private void Start()
    {
        _enemyCrowd.AddToCrowd(_enemyCount);
        transform.rotation = Quaternion.Euler(0,-180,0);
        countText.text = _enemyCrowd.CurrentCount + "";
    }

    public void Remove(int count)
    {
        newEnemeyCount = _enemyCount - count;
        InvokeRepeating(nameof(RemoveFromCrowd),.25f,.1f);
    }

    public void RemoveFromCrowd()
    {
        _enemyCrowd.RemoveFromCrowd();
        if (newEnemeyCount == _enemyCount)
        {
            CancelInvoke(nameof(RemoveFromCrowd));
            _enemyCount = newEnemeyCount;
        }
        countText.text = _enemyCrowd.CurrentCount + "";
        if (_enemyCrowd.CurrentCount ==1)
        {
            countText.transform.parent.gameObject.SetActive(false);
        }
    }
}
