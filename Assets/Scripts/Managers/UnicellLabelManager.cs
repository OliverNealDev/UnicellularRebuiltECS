using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UnicellLabelManager : MonoBehaviour
{
    public static UnicellLabelManager Instance;

    [SerializeField] private TextMeshProUGUI UnicellLabelPrefab;
    [SerializeField] private GameObject WorldSpaceCanvas;

    [SerializeField] private List<UnicellLabel> UnicellLabelList = new List<UnicellLabel>();

    private float LevelUpCheckerTimer;
    private float LevelUpCheckerTimerThreshold;

    private Camera cam;
    private bool isCameraFar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;

        LevelUpCheckerTimerThreshold = 0.2f;

        cam = Camera.main;
    }

    private void Update()
    {
        LevelUpCheckerTimer += Time.deltaTime;
        if (LevelUpCheckerTimer >= LevelUpCheckerTimerThreshold)
        {
            LevelUpCheckerTimer -= LevelUpCheckerTimerThreshold;
            LabelLogicTick();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isCameraFar)
        {
            for (int i = UnicellLabelList.Count - 1; i >= 0; i--)
            {
                UnicellLabel label = UnicellLabelList[i];

                if (label.targetUnicell != null && UnicellLabelList[i] != null)
                {
                    label.transform.position = label.targetUnicell.transform.position + new Vector3(0, 0.25f + label.targetUnicell.transform.localScale.x, 0);
                    //label.GetComponent<TextMeshProUGUI>().text = "Lv " + label.targetUnicell.Level;
                }
                else
                {
                    UnicellLabelList.Remove(label);
                    Destroy(label.gameObject);
                }
                //label.LabelText = label.targetUnicell.Level;
            }
        }
    }

    void LabelLogicTick()
    {
        if (cam.orthographicSize >= 50 && isCameraFar == false)
        {
            isCameraFar = true;

            for (int i = UnicellLabelList.Count - 1; i >= 0; i--)
            {
                UnicellLabelList[i].gameObject.SetActive(false);
            }
        }
        else if (cam.orthographicSize < 50 && isCameraFar == true)
        {
            isCameraFar = false;

            for (int i = UnicellLabelList.Count - 1; i >= 0; i--)
            {
                if (UnicellLabelList[i].targetUnicell != null)
                {
                    UnicellLabel label = UnicellLabelList[i];
                    label.gameObject.SetActive(true);
                    label.transform.position = UnicellLabelList[i].targetUnicell.transform.position + new Vector3(0, 0.25f + UnicellLabelList[i].targetUnicell.transform.localScale.x, 0);
                    if (label.targetUnicell.labelUpdateRequested)
                    {
                        label.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(label.targetUnicell.Damage + label.targetUnicell.MaxHealth).ToString();
                    }
                }
            }
        }

        for (int i = UnicellLabelList.Count - 1; i >= 0; i--)
        {
            UnicellLabel label = UnicellLabelList[i];
            if (label.targetUnicell.labelUpdateRequested)
            {
                label.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(label.targetUnicell.Damage + label.targetUnicell.MaxHealth).ToString();
            }
        }
    }

    public void InitialiseUnicellLabel(Unicell unicell)
    {
        UnicellLabel unicellLabel = Instantiate(UnicellLabelPrefab, unicell.transform.position + new Vector3(0, 0.25f + unicell.transform.localScale.x, 0), Quaternion.identity).GetComponent<UnicellLabel>();
        unicellLabel.targetUnicell = unicell;
        unicellLabel.transform.SetParent(WorldSpaceCanvas.transform);
        UnicellLabelList.Add(unicellLabel);
        unicellLabel.gameObject.SetActive(true);

        unicellLabel.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(unicellLabel.targetUnicell.Damage + unicellLabel.targetUnicell.MaxHealth).ToString() + "c";
    }

    public void RemoveUnicellLabel(Unicell unicell)
    {

    }
}
