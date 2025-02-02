using System.Collections.Generic;
using Unity.Entities.UniversalDelegates;
using UnityEngine;

public class SelectSquareManager : MonoBehaviour
{
    public static SelectSquareManager Instance;

    [SerializeField] private List<SelectSquare> selectList = new List<SelectSquare>();

    public GameObject SelectSquare;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (selectList.Count > 0)
        {
            for (int i = selectList.Count - 1; i >= 0; i--)
            {
                SelectSquare SelectSquare = selectList[i];
                if (SelectSquare.targetUnicell != null)
                {
                    SelectSquare.transform.position = SelectSquare.targetUnicell.transform.position;

                    if (SelectSquare.transform.localScale.x != SelectSquare.targetUnicell.transform.localScale.x)
                    {
                        SelectSquare.transform.localScale = SelectSquare.targetUnicell.transform.localScale;
                    }

                    if ((UIManager.Instance.StatPanelTargetUnicell == SelectSquare.targetUnicell && UIManager.Instance.isStatsLocked) || CameraManager.Instance.followedUnicell == SelectSquare)
                    {
                        SelectSquare.GetComponent<SpriteRenderer>().color = Color.green;
                    }
                    else
                    {
                        SelectSquare.GetComponent<SpriteRenderer>().color = Color.yellow;
                    }

                    if (!SelectSquare.targetUnicell.isHovered && UIManager.Instance.StatPanelTargetUnicell != SelectSquare.targetUnicell)
                    {
                        selectList.RemoveAt(i);
                        Destroy(SelectSquare.gameObject);
                    }
                }
                else
                {
                    selectList.RemoveAt(i);
                    Destroy(SelectSquare.gameObject);
                }
            }
        }
    }

    public void InstantiateSelectSquare(Unicell unicell)
    {
        bool isUnicellOccupied = false;
        if (selectList.Count > 0)
        {
            for (int i = selectList.Count - 1; i >= 0; i--)
            {
                if (selectList[i].targetUnicell == unicell)
                {
                    isUnicellOccupied = true;
                }
            }
        }
        else
        {
            isUnicellOccupied = false;
        }

        if (!isUnicellOccupied)
        {
            SelectSquare newSelectSquareController = Instantiate(SelectSquare, unicell.transform.position, Quaternion.identity).GetComponent<SelectSquare>();
            selectList.Add(newSelectSquareController);
            newSelectSquareController.targetUnicell = unicell;

            if (newSelectSquareController.transform.localScale.x != newSelectSquareController.targetUnicell.transform.localScale.x)
            {
                newSelectSquareController.transform.localScale = newSelectSquareController.targetUnicell.transform.localScale;
            }

            if ((UIManager.Instance.StatPanelTargetUnicell == newSelectSquareController.targetUnicell && UIManager.Instance.isStatsLocked) || CameraManager.Instance.followedUnicell == newSelectSquareController)
            {
                newSelectSquareController.GetComponent<SpriteRenderer>().color = Color.green;
            }
            else
            {
                newSelectSquareController.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
        }
    }
}
