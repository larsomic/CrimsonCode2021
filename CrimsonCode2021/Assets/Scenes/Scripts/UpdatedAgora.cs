using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using agora_gaming_rtc;
using agora_utilities;

namespace finalAgora
{
    class UpdatedAgora : MonoBehaviour
    {
        public string AppID;
        public string ChannelName;

        VideoSurface myView;
        VideoSurface remoteView;
        IRtcEngine mRtcEngine;

        void Awake()
        {
            SetupUI();
        }

        void Start()
        {
            SetupAgora();
        }

        void Join()
        {
            mRtcEngine.EnableVideo();
            mRtcEngine.EnableVideoObserver();
            mRtcEngine.EnableAudio(); // Proximity chat will basically go here
            myView.SetEnable(true);
            mRtcEngine.JoinChannel(ChannelName, "", 0); // Ok, so this should be modified later on, maybe dynamic video channels?
            Debug.Log("joined");
        }

        void Leave()
        {
            mRtcEngine.LeaveChannel();
            mRtcEngine.DisableVideo();
            mRtcEngine.DisableAudio();
            mRtcEngine.DisableVideoObserver();
        }

        void OnJoinChannelSuccessHandler(string channelName, uint uid, int elapsed)
        {
            Debug.LogFormat("Joined channel {0} successful, my uid = {1}", channelName, uid);
        }

        //Couldn't get room detection to work :(
        /*
        void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Entered trigger");
            GameObject go = GameObject.Find("MyView");
            myView = go.AddComponent<VideoSurface>();
            Join(other.gameObject.tag);
        }

        void OnTriggerExit2D(Collider2D other)
        {
            Debug.Log("left trigger");
            Leave();
        }
        */
        void OnLeaveChannelHandler(RtcStats stats)
        {
            myView.SetEnable(false);
            if (remoteView != null)
            {
                remoteView.SetEnable(false);
            }
        }

        void OnUserJoined(uint uid, int elapsed)
        {
            GameObject go = GameObject.Find("RemoteView");

            if (remoteView == null)
            {
                remoteView = go.AddComponent<VideoSurface>();
            }

            remoteView.SetForUser(uid);
            remoteView.SetEnable(true);
            remoteView.SetVideoSurfaceType(AgoraVideoSurfaceType.RawImage);
            remoteView.SetGameFps(30);
        }

        void OnUserOffline(uint uid, USER_OFFLINE_REASON reason)
        {
            remoteView.SetEnable(false);
        }

        void OnApplicationQuit()
        {
            if (mRtcEngine != null)
            {
                IRtcEngine.Destroy();
                mRtcEngine = null;
            }
        }

        void SetupUI()
        {
            GameObject go = GameObject.Find("MyView");
            myView = go.AddComponent<VideoSurface>();
            go = GameObject.Find("JoinButton");
            go?.GetComponent<Button>()?.onClick.AddListener(Join);
        }

        void SetupAgora()
        {
            mRtcEngine = IRtcEngine.GetEngine(AppID);

            mRtcEngine.OnUserJoined += OnUserJoined;
            mRtcEngine.OnUserOffline += OnUserOffline;
            mRtcEngine.OnJoinChannelSuccess += OnJoinChannelSuccessHandler;
            mRtcEngine.OnLeaveChannel += OnLeaveChannelHandler;
        }
    }
}