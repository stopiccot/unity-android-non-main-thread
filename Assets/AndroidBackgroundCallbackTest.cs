using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AndroidBackgroundCallbackTest : MonoBehaviour
{
    internal class BackgroundCallbackProxy : AndroidJavaProxy
    {
        public BackgroundCallbackProxy() : base("com.test.AndroidBackgroundCallbackTest$BackgroundCallback") { }

        public void onEvent(string str)
        {
            Debug.Log($"[AndroidBackgroundCallbackTest] onEvent({str}) Thread: {Thread.CurrentThread.ManagedThreadId}");
            var value = PlayerPrefs.GetInt("some_key");
            Debug.Log($"[AndroidBackgroundCallbackTest] onEvent PlayerPrefs.GetInt {value}");
        }
    }
    
    private AndroidJavaClass pluginClass;
    private BackgroundCallbackProxy backgroundCallback;
    
    public void Test()
    {
        PlayerPrefs.SetInt("some_key", 123);
        Debug.Log($"[AndroidBackgroundCallbackTest] Test Thread: {Thread.CurrentThread.ManagedThreadId}");
        pluginClass = new AndroidJavaClass("com.test.AndroidBackgroundCallbackTest");
        backgroundCallback = new BackgroundCallbackProxy();
        pluginClass.CallStatic("init", backgroundCallback);
    }
}
