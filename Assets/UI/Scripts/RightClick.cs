using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private UnityEvent onRightButtonClick;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnButtonClicked();
            //new ClickColor(eventData);
        }
    }


    private void OnButtonClicked()
    {
        onRightButtonClick.Invoke();
    }
}

public class ClickColor : Selectable
{
    public ClickColor(BaseEventData eventData) 
    {
        Press(eventData);
    }

    public void Press(BaseEventData eventData)
    {
        if (!IsActive() || !IsInteractable())
            return;

        DoStateTransition(SelectionState.Pressed, false);
        StartCoroutine(OnFinishSubmit());
    }

    private IEnumerator OnFinishSubmit()
    {
        var fadeTime = colors.fadeDuration;
        var elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        DoStateTransition(currentSelectionState, false);
    }
}
