using UniRx;
using UnityEngine;

public class KeyInputEventProvider : MonoBehaviour
{

    public IReadOnlyReactiveProperty<bool> Space => _space;
    public IReadOnlyReactiveProperty<bool> UpArrow => _upArrow;
    public IReadOnlyReactiveProperty<bool> DownArrow => _downArrow;
    public IReadOnlyReactiveProperty<bool> LeftArrow => _leftArrow;
    public IReadOnlyReactiveProperty<bool> RightArrow => _rightArrow;
    public IReadOnlyReactiveProperty<bool> CrossArrows => _crossArrows;

    private readonly ReactiveProperty<bool> _space = new ReactiveProperty<bool>();
    private readonly ReactiveProperty<bool> _upArrow = new ReactiveProperty<bool>();
    private readonly ReactiveProperty<bool> _downArrow = new ReactiveProperty<bool>();
    private readonly ReactiveProperty<bool> _leftArrow = new ReactiveProperty<bool>();
    private readonly ReactiveProperty<bool> _rightArrow = new ReactiveProperty<bool>();
    private readonly ReactiveProperty<bool> _crossArrows = new ReactiveProperty<bool>();

    private void Start()
    {
        // Destroy時にDispose()する
        _space.AddTo(this);
        _upArrow.AddTo(this);
        _downArrow.AddTo(this);
        _leftArrow.AddTo(this);
        _rightArrow.AddTo(this);
        _crossArrows.AddTo(this);
    }

    public void Update()
    {
        _space.Value = Input.GetKeyDown(KeyCode.Space);
        _upArrow.Value = Input.GetKeyDown(KeyCode.UpArrow);
        _downArrow.Value = Input.GetKeyDown(KeyCode.DownArrow);
        _leftArrow.Value = Input.GetKeyDown(KeyCode.LeftArrow);
        _rightArrow.Value = Input.GetKeyDown(KeyCode.RightArrow);

        _crossArrows.Value = false;
        if (_upArrow.Value || _downArrow.Value || _leftArrow.Value || _rightArrow.Value)
        {
            _crossArrows.Value = true;
        }
    }
}
