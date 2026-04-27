using UnityEngine;
using UnityEngine.UI;
public class SwitchColor : MonoBehaviour
{
    [Header("Target Image")]
    [SerializeField] private Image targetImage;

    [Header("Colors")]
    [SerializeField] private Color onColor = Color.green;
    [SerializeField] private Color offColor = Color.gray;

    private ToggleSwitch _toggleSwitch;

    private void Awake()
    {
        _toggleSwitch = GetComponent<ToggleSwitch>();
        if (_toggleSwitch == null)
            _toggleSwitch = GetComponentInParent<ToggleSwitch>();
    }

    private void Start()
    {
        // Provera stanja odmah na početku da bi boja bila ispravna čim se pokrene igra
        UpdateColorImmediately();
    }

    private void OnEnable()
    {
        if (_toggleSwitch != null)
        {
            // Prvo skinemo listener pa ga dodamo (da sprečimo dupliranje)
            _toggleSwitch.onToggleOn.RemoveListener(SetOnColor);
            _toggleSwitch.onToggleOff.RemoveListener(SetOffColor);

            _toggleSwitch.onToggleOn.AddListener(SetOnColor);
            _toggleSwitch.onToggleOff.AddListener(SetOffColor);
        }
    }

    private void OnDisable()
    {
        if (_toggleSwitch != null)
        {
            _toggleSwitch.onToggleOn.RemoveListener(SetOnColor);
            _toggleSwitch.onToggleOff.RemoveListener(SetOffColor);
        }
    }

    private void UpdateColorImmediately()
    {
        if (_toggleSwitch != null)
        {
            if (_toggleSwitch.CurrentValue) SetOnColor();
            else SetOffColor();
        }
    }

    public void SetOnColor()
    {
        if (targetImage != null)
            targetImage.color = onColor;
    }

    public void SetOffColor()
    {
        if (targetImage != null)
            targetImage.color = offColor;
    }

}
