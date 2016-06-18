using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FloatingMovement : MonoBehaviour 
{
    public void Initialize(Vector3 startPos, float direction)
    {
        this.startPos = startPos;
        this.endPos = startPos + new Vector3( direction * HorizontalAmplitude, 0f, 9f);

        currentAngle = startAngle;
        timeCounter = 0f;

        transform.position = startPos;
    }

    [SerializeField] private float VerticalAmplitude;
    [SerializeField] private float HorizontalAmplitude;
    [SerializeField] private float Speed;

    private Vector3 startPos;
    private Vector3 endPos;

    private float startAngle = 45;
    private float endAngle = 270f;
    private float currentAngle;

    private float timeCounter = 0f;

	private void LateUpdate () 
    {
        currentAngle = Mathf.Lerp(startAngle, endAngle, timeCounter) * Mathf.Deg2Rad;
        endPos.y = startPos.y + Mathf.Sin(currentAngle) * VerticalAmplitude;

        transform.position = Vector3.Lerp(startPos, endPos, timeCounter);
        transform.localScale = Vector3.Lerp(new Vector3(3f, 0.2f, 1f), Vector3.one, timeCounter * 7f);

        SetAlphaValue(transform, 1f - timeCounter);

        timeCounter += Time.deltaTime / Speed;

        if (timeCounter > 1f + Time.deltaTime)
        {
            timeCounter = 0f;
            gameObject.SetActive(false);
        }
	}

    private void SetAlphaValue(Transform alphaTr, float alphaValue)
    {
        SetAlphaToTr(alphaTr.GetComponent<TextMesh>(), alphaValue);

        for (int i = 0; i < alphaTr.childCount; i++)
        {
            SetAlphaValue(alphaTr.GetChild(i), alphaValue);
        }
    }

    private void SetAlphaToTr(TextMesh GraphicElement, float alphaValue)
    {
        if (GraphicElement == null)
            return;

        Color objectColor = GraphicElement.color;
        objectColor.a = alphaValue;
        GraphicElement.color = objectColor;
    }
}
