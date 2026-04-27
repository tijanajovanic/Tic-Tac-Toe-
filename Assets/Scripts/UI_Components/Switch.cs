using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleSwitch : MonoBehaviour, IPointerClickHandler
    {
    [Header("Slider setup")]
    [SerializeField, Range(0, 1f)] protected float sliderValue;
    public bool CurrentValue { get; private set; }

    private Slider _slider;

    [Header("Animation")]
    [SerializeField, Range(0, 1f)] private float animationDuration = 0.5f;
    [SerializeField] private AnimationCurve slideEase = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Coroutine _animateSliderCoroutine;

    [Header("Events")]
    public UnityEvent onToggleOn;
    public UnityEvent onToggleOff;

    protected void OnValidate()
    {
        if (_slider == null) _slider = GetComponent<Slider>();
        if (_slider != null) _slider.value = sliderValue;
    }

    protected virtual void Awake()
    {
        SetupSliderComponent();
        CurrentValue = (sliderValue >= 0.465f); // Ako je sliderValue u inspektoru postavljen na 1, biće true
        if (_slider != null) _slider.value = sliderValue;
    } 

    private void SetupSliderComponent()
    {
        _slider = GetComponent<Slider>();
        if (_slider == null) return;

        _slider.interactable = false;
        _slider.transition = Selectable.Transition.None;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Toggle();
    }

    private void Toggle()
    {
        SetStateAndStartAnimation(!CurrentValue);
    }

    private void SetStateAndStartAnimation(bool state)
    {
        CurrentValue = state;

        if (CurrentValue) onToggleOn?.Invoke();
        else onToggleOff?.Invoke();

        if (_animateSliderCoroutine != null) StopCoroutine(_animateSliderCoroutine);
        _animateSliderCoroutine = StartCoroutine(AnimateSlider());
    }
    public void SetState(bool state)
    {
        CurrentValue = state;
        if (_slider != null)
            _slider.value = state ? 1 : 0.465f;
    }

    private IEnumerator AnimateSlider()
    {
        float startValue = _slider.value;
        float endValue = CurrentValue ? 1 : 0.465f;
        float time = 0;

        while (time < animationDuration)
        {
            time += Time.unscaledDeltaTime;
            _slider.value = Mathf.Lerp(startValue, endValue, slideEase.Evaluate(time / animationDuration));
            yield return null;
        }
        _slider.value = endValue;
    }
}
