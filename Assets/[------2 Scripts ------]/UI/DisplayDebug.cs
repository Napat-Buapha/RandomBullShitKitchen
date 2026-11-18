using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DisplayDebug : MonoBehaviour
{
    readonly List<GameObject> spawnedLogObjects = new List<GameObject>();
    
    bool visible;
    const int MaxLogs = 100;

    [Header("UI References")]
    [SerializeField] private Transform countain;
    [SerializeField] private GameObject debugPrefab;
    [SerializeField] private ScrollRect scrollRect;

    void OnEnable()  => Application.logMessageReceived += HandleLog;
    void OnDisable() => Application.logMessageReceived -= HandleLog;

    void HandleLog(string message, string stackTrace, LogType type)
    {
        if (type == LogType.Error || type == LogType.Exception)
            message += "\n" + stackTrace;
        
        if (spawnedLogObjects.Count > MaxLogs)
        {
            spawnedLogObjects.RemoveAt(0);
            if (spawnedLogObjects.Count > 0)
            {
                Destroy(spawnedLogObjects[0]);
                spawnedLogObjects.RemoveAt(0);
            }
        }

        // สร้าง Prefab ออกมาแสดงผลทาง UI ทันที
        SpawnLogUI(message, type);
    }

    void SpawnLogUI(string message, LogType type)
    {
        if (debugPrefab == null || countain == null) return;

        // Instantiate Prefab ลงใน Container
        GameObject logObj = Instantiate(debugPrefab, countain);
        spawnedLogObjects.Add(logObj);

        Text logText = logObj.GetComponentInChildren<Text>();
        if (logText != null)
        {
            logText.text = message;
            // เปลี่ยนสีข้อความตามประเภท Log
            logText.color = type == LogType.Warning ? Color.yellow
                          : type == LogType.Log ? Color.white
                          : Color.red;
        }

        if (scrollRect != null)
        {
            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = 0f;
        }
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.P) || (Input.touchCount == 3 && Input.GetTouch(2).phase == TouchPhase.Began))
        {
            visible = !visible;
            if (countain != null)
            {
                countain.gameObject.SetActive(visible);
            }
        }
        */
    }

    public void ClearLogs()
    {
        spawnedLogObjects.Clear();
        foreach (var obj in spawnedLogObjects)
        {
            if (obj != null) Destroy(obj);
        }
        spawnedLogObjects.Clear();
    }
}
