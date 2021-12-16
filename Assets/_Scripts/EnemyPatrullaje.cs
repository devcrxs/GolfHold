using UnityEngine;
using Random = UnityEngine.Random;
public class EnemyPatrullaje : MonoBehaviour
{
    private float _minDistance = 0.1f;
    
    [SerializeField] private Transform[] positions;
    [SerializeField] private bool isRandomPos = true;
    [SerializeField] private float speed;
    private int _index;

    private void Start()
    {
        RandomInitialPos();
    }

    private void RandomInitialPos()
    {
        if (isRandomPos)
        {
            _index = Random.Range(0, positions.Length);
        }
    }
    private void Update()
    {
        MoveEnemy();
    }
    private void MoveEnemy()
    {
       MoveTowards();
       ChangeDirecction();
    }

    private void MoveTowards()
    {
        transform.position = Vector2.MoveTowards(transform.position, 
            positions[_index].position, speed * Time.deltaTime);
    }

    private void ChangeDirecction()
    {
        if (IsArrivePosition())
        {
            if (IsLastPosition())
            {
                _index++;
            }
            else
            {
                _index = Constans.ZERO;
            }
        }
    }

    private bool IsArrivePosition()
    {
        return Vector2.Distance(transform.position, positions[_index].position) < _minDistance;
    }

    private bool IsLastPosition()
    {
        return positions[_index] != positions[positions.Length - 1];
    }
}
