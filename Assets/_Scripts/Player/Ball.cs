using UnityEngine;
using UnityEngine.EventSystems;
public class Ball : MonoBehaviour
{
    private float _distance;
    private float pushForce = 4;
    private bool _isDragg;

    private Rigidbody2D _rb;
    private Camera _camera;
    private CheckColl _checkColl;

    //Vectores
    private Vector2 _force;
    private Vector2 _startPoint;
    private Vector2 _endPoint;
    private Vector2 _direction;

    [SerializeField] private Trajectory trajectory;

    private void Start()
    {
        ReferenceComponents();
        VelocityZero();
    }

    private void ReferenceComponents()
    {
        _rb = GetComponent<Rigidbody2D>();
        _checkColl = GetComponent<CheckColl>();
        _camera = Camera.main;
    }
    private void VelocityZero()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Constans.ZERO;
    }

    private void FixedUpdate()
    {
        Draggin();
    }

    private void Update()
    {
        TouchComprobation();
        StopRoute();
    }

    private void TouchComprobation()
    {
        if (IsTouch())
        {
            if (IsTouchDown())
            {
                _isDragg = true;
                DragStart();
                
            }
            if (IsTouchUp())
            {
                _isDragg = false;
                trajectory.HideTrajectory();
                DragEnd();
            }
        }
    }

    private bool IsTouch()
    {
        if (Input.touchCount > Constans.ZERO)
        {
            Touch touch = Input.GetTouch(Constans.ZERO);
            return  GameManager.instance.availableAttempts > Constans.ZERO && !EventSystem.current.IsPointerOverGameObject(touch.fingerId); 
        }

        return false;
    }

    private bool IsTouchDown()
    {
        return Input.GetTouch(Constans.ZERO).phase == TouchPhase.Began;
    }
    
    private void DragStart()
    {
        VelocityZero();
        Touch touch = Input.GetTouch(0);
        _startPoint = _camera.ScreenToWorldPoint(touch.position);
        trajectory.ShowTrajectory();
    }

    private bool IsTouchUp()
    {
        return Input.GetTouch(0).phase == TouchPhase.Ended && _isDragg;
    }
    
    private void DragEnd()
    {
        if (_checkColl.CanDrag)
        {
            PushBall();
            AudioSourceManager.instance.PlayAudioShoot();
            trajectory.HideTrajectory();
        }
    }
    
    private void PushBall()
    { 
        VelocityZero();
        _rb.AddForce(_force,ForceMode2D.Impulse);
        GameManager.instance.availableAttempts -= 1;
    }
    

    private void Draggin()
    {
        if (_isDragg)
        {
            Drag();
        }
    }
    
    private void Drag()
    {
        Touch touch = Input.GetTouch(Constans.ZERO);
        _endPoint = _camera.ScreenToWorldPoint(touch.position); //Posicion del touch en pantalla
        _distance = Vector2.Distance(_startPoint, _endPoint); // Distancia entre los dos vectores, magnitud
        _direction = (_startPoint - _endPoint).normalized; // Direcction hacia donde va la bola
        _force = pushForce * _distance * _direction; // Fuerza Aplicada
        trajectory.CircleUpdate(transform.position,_force);
    }

    private void StopRoute()
    {
        if (IsMovingUp())
        {
            VelocityZero();
        }
    }

    private bool IsMovingUp()
    {
        return _rb.velocity.y > Constans.ZERO && _isDragg && _checkColl.CanDrag;
    }
}
