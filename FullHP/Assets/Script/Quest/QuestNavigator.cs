using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestNavigator : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera uiCam;
    Transform findCam;
    Vector3 targetPos;
    RectTransform pointerRectTransform;


    public Sprite Arrow;
    public Sprite Cross;
    void Awake()
    {

        uiCam = FindObjectOfType<Camera>();
        pointerRectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
            Vector3 toPos = targetPos;
            Vector3 fromPosition = uiCam.transform.position;
            fromPosition.z = 0f;
            Vector3 dir = (toPos - fromPosition).normalized;
            float angleRad = Mathf.Atan2(dir.y, dir.x);
            float angleDegree = angleRad * Mathf.Rad2Deg;
            pointerRectTransform.localEulerAngles = new Vector3(0, 0, angleDegree);
        float borderSize = 200f;

            Vector3 targetPosScreen = uiCam.WorldToScreenPoint(targetPos);
            bool isOffScreen = targetPosScreen.x <= borderSize || targetPosScreen.x >= Screen.width - borderSize || targetPosScreen.y <= borderSize || targetPosScreen.y >= Screen.height - borderSize;
            if (isOffScreen)
            {
                Vector3 cappeedTargetScreenPosition = targetPosScreen;
                if (cappeedTargetScreenPosition.x <= borderSize) cappeedTargetScreenPosition.x = borderSize;
                if (cappeedTargetScreenPosition.x >= Screen.width) cappeedTargetScreenPosition.x = Screen.width - borderSize;
                if (cappeedTargetScreenPosition.y <= borderSize) cappeedTargetScreenPosition.y = borderSize;
                if (cappeedTargetScreenPosition.y >= Screen.height) cappeedTargetScreenPosition.y = Screen.height - borderSize;

                Vector3 pointerWorldPos = uiCam.ScreenToWorldPoint(cappeedTargetScreenPosition);
                pointerRectTransform.position = pointerWorldPos;
                pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
            gameObject.GetComponent<SpriteRenderer>().sprite = Arrow;

            }
            else
            {
                Vector3 pointerWorldPos = uiCam.ScreenToWorldPoint(targetPosScreen);
                pointerRectTransform.position = pointerWorldPos;
                pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
            pointerRectTransform.localEulerAngles = Vector3.zero;
            gameObject.GetComponent<SpriteRenderer>().sprite = Cross;
        }
        
        

    }

    public Vector3 GetPositionForQuest(Vector3 position)
    {
        targetPos = position;
        return targetPos;
    }

 
}
