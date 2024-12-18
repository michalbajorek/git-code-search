﻿using System;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace GitCodeSearch.Utilities;

public static class TaskExtensions
{
    public static void WaitAndDispatch(this Task task)
    {
        var frame = new DispatcherFrame();
        Exception? exception = null;
        task.ContinueWith(task =>
        {
            exception = task.Exception;
            frame.Continue = false;
        });

        Dispatcher.PushFrame(frame);

        if (exception != null)
        {
            ExceptionDispatchInfo.Throw(exception);
        }
    }
}
