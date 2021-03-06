﻿#region Copyright © 2017 Couchcoding

// File:    WinDebugReceiver.cs
// Package: Logbert
// Project: Logbert
// 
// The MIT License (MIT)
// 
// Copyright (c) 2017 Couchcoding
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

#endregion

using System;
using System.Collections.Generic;
using Com.Couchcoding.Logbert.Interfaces;
using System.Diagnostics;
using Com.Couchcoding.Logbert.Helper;
using Com.Couchcoding.Logbert.Logging;
using Com.Couchcoding.Logbert.Controls;

namespace Com.Couchcoding.Logbert.Receiver.WinDebugReceiver
{
  /// <summary>
  /// Implements a <see cref="ILogProvider"/> for the windows debugger service.
  /// </summary>
  public class WinDebugReceiver : ReceiverBase
  {
    #region Private Fields

    /// <summary>
    /// Counts the received messages;
    /// </summary>
    private int mLogNumber;

    /// <summary>
    /// Holds the process to capture debug logs from.
    /// </summary>
    private readonly Process mProcess;

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets the columns to display of the <see cref="ILogProvider"/>.
    /// </summary>
    public override Dictionary<int, string> Columns
    {
      get
      {
        return new Dictionary<int, string>
        {
          { 0, "Number"     },
          { 1, "Level"      },
          { 2, "Timestamp"  },
          { 3, "Process ID" },
          { 4, "Message"    }
        };
      }
    }

    /// <summary>
    /// Gets the description of the <see cref="ILogProvider"/>
    /// </summary>
    public override string Description
    {
      get
      {
        return string.Format(
            "{0} ({1})"
          , Name
          , mProcess == null ? "*" : mProcess.ProcessName);
      }
    }

    /// <summary>
    /// Gets the filename for export of the received <see cref="LogMessage"/>s.
    /// </summary>
    public override string ExportFileName
    {
      get
      {
        return Description;
      }
    }

    /// <summary>
    /// Gets the name of the <see cref="ILogProvider"/>.
    /// </summary>
    public override string Name
    {
      get
      {
        return "Windows Debug Receiver";
      }
    }

    /// <summary>
    /// Gets the settings <see cref="Control"/> of the <see cref="ILogProvider"/>.
    /// </summary>
    public override ILogSettingsCtrl Settings
    {
      get
      {
        return new WinDebugReceiverSettings();
      }
    }

    /// <summary>
    /// Determines whether this <see cref="ILogProvider"/> supports the logger tree window.
    /// </summary>
    public override bool HasLoggerTree
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    /// Determines whether this <see cref="ILogProvider"/> has a statistic window.
    /// </summary>
    public override bool HasStatisticView
    {
      get
      {
        // Currently no statistic window is supported.
        return false;
      }
    }

    /// <summary>
    /// Determines whether this <see cref="ILogProvider"/> supports reloading of the content, ot not.
    /// </summary>
    public override bool SupportsReload
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    /// Gets the supported <see cref="LogLevel"/>s of the <see cref="ILogProvider"/>.
    /// </summary>
    public override LogLevel SupportedLevels
    {
      get
      {
        return LogLevel.Debug;
      }
    }

    /// <summary>
    /// Get the <see cref="Control"/> to display details about a selected <see cref="LogMessage"/>.
    /// </summary>
    public override ILogPresenter DetailsControl
    {
      get
      {
        return new WinDebugDetailsControl();
      }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Handles the OnoutputDebugString event of the <see cref="DebugMonitor"/> instance.
    /// </summary>
    /// <param name="pid">The process ID of the sender application.</param>
    /// <param name="text">the message that is created by the sender application.</param>
    private void DebugMonitorOnOutputDebugString(int pid, string text)
    {
      if (mIsActive && (mProcess == null || Equals(mProcess.Id, pid)))
      {
        LogMessage newLogMsg = new LogMessageWinDebug(
            pid
          , text
          , ++mLogNumber);

        if (mLogHandler != null)
        {
          mLogHandler.HandleMessage(newLogMsg);
        }
      }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Intizializes the <see cref="ILogProvider"/>.
    /// </summary>
    /// <param name="logHandler">The <see cref="ILogHandler"/> that may handle incomming <see cref="LogMessage"/>s.</param>
    public override void Initialize(ILogHandler logHandler)
    {
      base.Initialize(logHandler);

      try
      {
        DebugMonitor.OnOutputDebugString += DebugMonitorOnOutputDebugString;
        DebugMonitor.Start();
      }
      catch (Exception ex)
      {
        Logger.Error(
            "Error while initializig the windows debugger receiver: {0}"
          , ex.Message);

        Shutdown();
      }
    }

    /// <summary>
    /// Shuts down the <see cref="ILogProvider"/> instance.
    /// </summary>
    public override void Shutdown()
    {
      DebugMonitor.OnOutputDebugString -= DebugMonitorOnOutputDebugString;

      try
      {
        DebugMonitor.Stop();
      }
      catch (Exception ex)
      {
        Logger.Error(
            "Error while initializig the windows debugger receiver: {0}"
          , ex.Message);
      }

      base.Shutdown();
    }

    /// <summary>
    /// Gets the header used for the CSV file export.
    /// </summary>
    /// <returns></returns>
    public override string GetCsvHeader()
    {
      return "\"Number\","
           + "\"Level\","
           + "\"Timestamp\","
           + "\"Message\""
           + Environment.NewLine;
    }

    /// <summary>
    /// Resets the <see cref="ILogProvider"/> instance.
    /// </summary>
    public override void Clear()
    {
      mLogNumber = 0;
    }

    /// <summary>
    /// Saves the current docking layout of the <see cref="ReceiverBase"/> instance.
    /// </summary>
    /// <param name="layout">The layout as string to save.</param>
    public override void SaveLayout(string layout)
    {
      Properties.Settings.Default.DockLayoutWinDebugReceiver = layout ?? string.Empty;
      Properties.Settings.Default.SaveSettings();
    }

    /// <summary>
    /// Loads the docking layout of the <see cref="ReceiverBase"/> instance.
    /// </summary>
    /// <returns>The restored layout, or <c>null</c> if none exists.</returns>
    public override string LoadLayout()
    {
      return Properties.Settings.Default.DockLayoutWinDebugReceiver;
    }
        /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString()
    {
      return Name;
    }
    
    #endregion
    
    #region Constructor

    /// <summary>
    /// Creates a new and empty instance of the <see cref="WinDebugReceiver"/> class.
    /// </summary>
    public WinDebugReceiver()
    {

    }

    /// <summary>
    /// Creates a new and configured instance of the <see cref="WinDebugReceiver"/> class.
    /// </summary>
    /// <param name="process">The <see cref="Process"/> of the process to capture.</param>
    public WinDebugReceiver(Process process)
    {
      mProcess = process;
    }

    #endregion
  }
}
