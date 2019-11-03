using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField]
    private float animationSpeed = 0.25f;

    internal bool Visible { get; set; } = false;
    internal float AnimationSpeed { get; set; }

    RectTransform ScrollPanel;
    Vector2 targetPosition;
    Vector2 initialPosition;

    private void Awake()
    {
        ScrollPanel = GetComponent<RectTransform>();
        initialPosition = ScrollPanel.anchoredPosition;
        targetPosition = new Vector2(0, initialPosition.y);

        AnimationSpeed = animationSpeed;
    }

    public void Show()
    {
        if (Visible)
            return;

        Visible = true;
    }

    public void Hide()
    {
        if (!Visible)
            return;

        Visible = false;
    }

    private void Update()
    {
        if (Visible && ScrollPanel.anchoredPosition.x > 0)
            ScrollPanel.anchoredPosition = new Vector2(Mathf.Lerp(ScrollPanel.anchoredPosition.x, targetPosition.x, AnimationSpeed), initialPosition.y);
        else if (!Visible && ScrollPanel.anchoredPosition.x < initialPosition.x)
            ScrollPanel.anchoredPosition = new Vector2(Mathf.Lerp(ScrollPanel.anchoredPosition.x, initialPosition.x, AnimationSpeed), initialPosition.y);
    }
}
