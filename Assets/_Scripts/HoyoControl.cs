using System.Collections;
using Unity.Mathematics;
using UnityEngine;
public class HoyoControl : MonoBehaviour
{
    private int _diamondCurrent;
    private float _waitSecondsTransition = 0.5f;
    private float _waitSecondsTransitionComplet= 0.7f;
    private bool _isNext;

    private Color _defaultColor;
    public static HoyoControl instance;
    [SerializeField] private bool isDiamond;
    [SerializeField] private int diamondsNumber;
    [SerializeField] private Rigidbody2D rigidbody2DPlayer;
    [SerializeField] private SpriteRenderer[] spritesRendererHoyo;
    [SerializeField] private Animator animatorTransition;
    [SerializeField] private Animator animatorLevelComplet;
    [SerializeField] private GameObject effect;
    public int DiamondCurrent {get => _diamondCurrent; set => _diamondCurrent = value; }
    public bool IsNext => _isNext;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        ChangeColorStart();
    }

    private void ChangeColorStart()
    {
        if (IsNeedDiamond())
        {
            _defaultColor = spritesRendererHoyo[0].color;
            ChangeColorArray(Color.gray);
        }
    }
    
    private bool IsNeedDiamond()
    {
        return isDiamond;
    }

    private void ChangeColorArray(Color color)
    {
        for (int i = 0; i < spritesRendererHoyo.Length; i++)
        {
            ChangeColorSprite(i,color);
        }
    }

    private void ChangeColorSprite(int position, Color color)
    {
        spritesRendererHoyo[position].color = color;
    }

    private void Update()
    {
        ChangeColorDefault();
    }

    public void ChangeColorDefault()
    {
        if (IsCompletDiamond())
        {
            _diamondCurrent++;
            ChangeColorArray(_defaultColor);
        }
    }

    private bool IsCompletDiamond()
    {
        return _diamondCurrent == diamondsNumber;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (TagPlayer.IsPlayer(other))
        {
            ComprobationDiamond();
        }
    }

    private void ComprobationDiamond()
    {
        if (IsNeedDiamond() && _diamondCurrent >= diamondsNumber)
        {
            LoadCanvasWin();
        }
        else if (!IsNeedDiamond())
        {
            LoadCanvasWin();
        }
    }
    
    private void LoadCanvasWin()
    {
        StartCoroutine(WaitShowCanvasWin());
    }

    private IEnumerator WaitShowCanvasWin()
    {
        _isNext = true;
        AudioSourceManager.instance.PlayAudioHoyo();
        Instantiate(effect, transform.position, quaternion.identity);
        rigidbody2DPlayer.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(_waitSecondsTransition);
        AnimatorManager.AnimatorPlay(animatorTransition,Constans.OUT);
        yield return new WaitForSeconds(_waitSecondsTransitionComplet);
        AnimatorManager.AnimatorPlay(animatorLevelComplet,Constans.RUN);
    }
}
