using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class SceneController : MonoBehaviour {

    void Start() {
        QuitOnConnectionErrors();
    }
    
    void Update() {
       // The tracking state must be FrameTrackingState.Tracking in order to access the Frame.
        if (Frame.TrackingState != FrameTrackingState.Tracking)
        {
            const int LOST_TRACKING_SLEEP_TIMEOUT = 15;
            Screen.sleepTimeout = LOST_TRACKING_SLEEP_TIMEOUT;
            return;
        }
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    /**
    * Checks the state of the ARCore Session
    */
    private void QuitOnConnectionErrors() {
       // Do not update if ARCore is not tracking.
        if (Session.ConnectionState == SessionConnectionState.DeviceNotSupported)
        {
            StartCoroutine(CodelabUtils.ToastAndExit(
                    "This device does not support ARCore.", 5));
        }
        else if (Session.ConnectionState == SessionConnectionState.UserRejectedNeededPermission)
        {
            StartCoroutine(CodelabUtils.ToastAndExit(
                    "Camera permission is needed to run this application.", 5));
        }
        else if (Session.ConnectionState == SessionConnectionState.ConnectToServiceFailed)
        {
            StartCoroutine(CodelabUtils.ToastAndExit(
                    "ARCore encountered a problem connecting.  Please start the app again.", 5));
        } 
    }
}
