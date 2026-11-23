using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DisplayDebug : Singleton<DisplayDebug>
{
    [SerializeField] private List<GameObject> spawnedLogObjects = new List<GameObject>();
    
    const int MaxLogs = 100;

    [Header("UI References")]
    [SerializeField] private Transform content;
    [SerializeField] private GameObject displayObj;
    [SerializeField] private DebugPrefab debugPrefab;
    [SerializeField] private ScrollRect scrollRect;

    void OnEnable() => Application.logMessageReceived += HandleLog;
    void OnDisable() => Application.logMessageReceived -= HandleLog;

    protected override void Awake()
    {
        base.Awake();
    }

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

    void SpawnLogUI(string message, LogType type , Color background = default)
    {
        if (debugPrefab == null || content == null) return;

        // Instantiate Prefab ลงใน Container
        GameObject logObj = Instantiate(debugPrefab.gameObject, content);
        spawnedLogObjects.Add(logObj);

        var logText = logObj.GetComponent<DebugPrefab>();
        if (logText != null)
        {
            logText.Text.text = message;
            // เปลี่ยนสีข้อความตามประเภท Log
            logText.TextColor = type == LogType.Warning ? Color.yellow
                          : type == LogType.Log ? Color.white
                          : Color.red;
        }

        if (scrollRect != null)
        {
            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = 0f;
        }
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

    public void ActiveDisplayDebug(bool v)
    {
        displayObj.SetActive(v);
    }
}
