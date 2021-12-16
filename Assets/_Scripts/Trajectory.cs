using UnityEngine;
public class Trajectory : MonoBehaviour
{
    private float _timeStamp;
    private float _colorAlpha = 0.7f;
    private float subtractColorAlpha = 0.05f;
    
    private Transform[] _circleList;
    private Vector2 _positionCircle;
    
    [SerializeField] private int circleNumber;
    [SerializeField] private float spacing;
    [SerializeField] [Range(0.01f,0.3f)] private float _circleMinScale;
    [SerializeField] [Range(0.3f,1f)] private float _circleMaxScale; 
    [SerializeField] private Rigidbody2D playerRigidbody2D;
    
    [Header("GameObjects")]
    [SerializeField] private GameObject circleParent;
    [SerializeField] private GameObject prefabCircle;

    private void Start()
    {
        HideTrajectory();
        InstantiateCircles();
        ReduceColorAlpha();
    }

    private void InstantiateCircles()
    {
        _circleList = new Transform[circleNumber];
        prefabCircle.transform.localScale = Vector2.one * _circleMaxScale;
        var factorScale = _circleMaxScale / circleNumber;
        for (int i = 0; i < circleNumber; i++)
        {
            _circleList[i] = Instantiate(prefabCircle, null).transform;
            _circleList[i].parent = circleParent.transform;

            _circleList[i].localScale = Vector2.one * _circleMaxScale;
            if (_circleMaxScale > _circleMinScale)
            {
                _circleMaxScale -= factorScale;
            }
        }
    }

    private void ReduceColorAlpha()
    {
        for (int i = 0; i < circleNumber; i++)
        {
            var color = _circleList[i].gameObject.GetComponent<SpriteRenderer>().color;
            color.a = _colorAlpha;
            _colorAlpha -= subtractColorAlpha;
            _circleList[i].gameObject.GetComponent<SpriteRenderer>().color = color;
        }
    }

    public void CircleUpdate (Vector3 playerPos, Vector2 forceApplied)
    {
        _timeStamp = spacing;
        for (int i = 0; i < circleNumber; i++)
        {
            var positionX = playerPos.x + forceApplied.x * _timeStamp;
            
            var positionY = playerPos.y + forceApplied.y * _timeStamp - Physics2D.gravity.magnitude * 
                playerRigidbody2D.gravityScale * _timeStamp * _timeStamp / 2f;
            
            _positionCircle.x = positionX;
            _positionCircle.y = positionY;

            _circleList[i].position = _positionCircle;
            _timeStamp += spacing;
        }
    }

    public void ShowTrajectory()
    {
        IsActiveGameObject(circleParent,true);
    }

    public void HideTrajectory()
    {
        IsActiveGameObject(circleParent,false);
    }

    private void IsActiveGameObject(GameObject go, bool value)
    {
        go.SetActive(value);
    }
}
