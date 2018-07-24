using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Adapted from https://gist.github.com/mminer/975374

//TODO: Pool objects

public class PopUpSystem : MonoBehaviour {

    public GameObject notificationButtonPrefab;

    public Transform notificationPanelTransform; 


    public Color errorColor = Color.red;
    public Color warningColor = Color.yellow;
    public Color logColor = Color.black;

    Dictionary<LogType, Color> logTypeColors = new Dictionary<LogType, Color>()
    {
        { LogType.Assert, Color.white },
        { LogType.Error, Color.red },
        { LogType.Exception, Color.red },
        { LogType.Log, Color.white },
        { LogType.Warning, Color.yellow },
    };

    struct Log
    {
        public string message;
        public string stackTrace;
        public LogType type;
    }
    List<Log> logs = new List<Log>();

    void OnEnable ()
    {
        Application.RegisterLogCallback(HandleLog);
    }

    void OnDisable ()
    {
        Application.RegisterLogCallback(null);
    }



    /// <summary>
    /// Records a log from the log callback.
    /// </summary>
    /// <param name="message">Message.</param>
    /// <param name="stackTrace">Trace of where the message came from.</param>
    /// <param name="type">Type of message (error, exception, warning, assert).</param>
    void HandleLog (string message, string stackTrace, LogType type)
    {
        logs.Add(new Log() {
            message = message,
            stackTrace = stackTrace,
            type = type,
        });

        if(notificationPanelTransform != null && notificationButtonPrefab != null){
            GameObject notif = GameObject.Instantiate(notificationButtonPrefab);
            notif.transform.SetParent(notificationPanelTransform);
            notif.transform.Find("Text").gameObject.GetComponent<Text>().text = message;
            Image logo = notif.transform.Find("Image").gameObject.GetComponent<Image>();
            switch(type){
                case LogType.Error:
                case LogType.Exception:
                    logo.color = errorColor;
                    break;
                case LogType.Warning:
                    logo.color = warningColor;
                    break;
                case LogType.Log:
                    logo.color = logColor;
                    break;
                default:
                    logo.color = Color.black;
                    break;
            }

        }

    }

    int cpt = 0;
    void Update(){
        
        if(cpt == 0){
            Debug.LogWarning("My warning");
        }
        if(cpt == 90){
            Debug.LogError("My error");
        }

        if(cpt == 120){
            cpt = -1;
            Debug.Log("My Log");
        }
        cpt++;
        
    }
}
