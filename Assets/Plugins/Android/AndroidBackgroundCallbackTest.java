package com.test;

import android.util.Log;
import java.util.concurrent.ScheduledThreadPoolExecutor;

public class AndroidBackgroundCallbackTest {

    public interface BackgroundCallback {
        void onEvent(String var1);
    }

    public static void init(BackgroundCallback backgroundCallback) {
        Log.i("Unity", "[AndroidBackgroundCallbackTest] init");

        ScheduledThreadPoolExecutor threadPoolExecutor = new ScheduledThreadPoolExecutor(3);

        threadPoolExecutor.execute(() -> {
            try {
                Thread.sleep(1000);
            } catch (InterruptedException e) {
                throw new RuntimeException(e);
            }
            Log.i("Unity", "[AndroidBackgroundCallbackTest] calling callback");
            backgroundCallback.onEvent("some_value");
        });
    }
}