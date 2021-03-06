﻿#region Copyright © 2017 Couchcoding

// File:    Log4NetDirReceiverSettings.cs
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
using System.Drawing;
using System.Windows.Forms;

using Com.Couchcoding.Logbert.Interfaces;
using Com.Couchcoding.Logbert.Properties;
using System.IO;

using Com.Couchcoding.Logbert.Helper;

namespace Com.Couchcoding.Logbert.Receiver.Log4NetDirReceiver
{
  /// <summary>
  /// Implements the <see cref="ILogSettingsCtrl"/> control for the Log4Net file receiver.
  /// </summary>
  public partial class Log4NetDirReceiverSettings : UserControl, ILogSettingsCtrl
  {
    #region Private Methods

    /// <summary>
    /// Handles the Click event of the browse for file <see cref="Button"/>.
    /// </summary>
    private void TxtLogDirectoryButtonClick(object sender, EventArgs e)
    {
      using (FolderBrowserDialog bfd = new FolderBrowserDialog())
      {
        bfd.Description  = Resources.strLog4NetDirectoryReceiverSelectDirectoryToObserve;
        bfd.RootFolder   = Environment.SpecialFolder.Desktop;
        bfd.SelectedPath = txtLogDirectory.Text;

        if (bfd.ShowDialog(this) == DialogResult.OK)
        {
          txtLogDirectory.Text = bfd.SelectedPath;
        }
      }
    }

    /// <summary>
    /// Handles the Click event of the file pattern help <see cref="Button"/>.
    /// </summary>
    private void TxtLogFilePatternButtonClick(object sender, EventArgs e)
    {
      Control btnFilePattern = sender as Control;

      if (btnFilePattern != null)
      {
        mnuFilePattern.Show(
            btnFilePattern
          , new Point(btnFilePattern.Width, btnFilePattern.Top));
      }
    }

    /// <summary>
    /// Handles the Click event of a file pattern preset <see cref="ToolStripMenuItem"/>.
    /// </summary>
    private void MnuFilePatternClick(object sender, EventArgs e)
    {
      ToolStripMenuItem mnuCtrl = sender as ToolStripMenuItem;

      if (mnuCtrl?.Tag != null)
      {
        txtLogFilePattern.Text = mnuCtrl.Tag.ToString();
      }
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.UserControl.Load"/> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);

      if (ModifierKeys != Keys.Shift)
      {
        if (Directory.Exists(Settings.Default.PnlLog4NetDirectorySettingsDirectory))
        {
          txtLogDirectory.Text = Settings.Default.PnlLog4NetDirectorySettingsDirectory;
        }

        txtLogFilePattern.Text    = Settings.Default.PnlLog4NetDirectorySettingsPattern ;
        chkInitialReadAll.Checked = Settings.Default.PnlLog4NetDirectorySettingsReadAllExisting;
      }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Validates the entered settings.
    /// </summary>
    /// <returns>The <see cref="ValidationResult"/> of the validation.</returns>
    public ValidationResult ValidateSettings()
    {
      if (!Directory.Exists(txtLogDirectory.Text))
      {
        txtLogDirectory.SelectAll();
        txtLogDirectory.Select();

        return ValidationResult.Error(Resources.strLog4NetDirectodyReceiverDirectoryDoesNotExist);
      }

      if (string.IsNullOrEmpty(txtLogFilePattern.Text))
      {
        txtLogFilePattern.SelectAll();
        txtLogFilePattern.Select();

        return ValidationResult.Error(Resources.strLog4NetDirectodyReceiverInvalidFilePattern);
      }

      return ValidationResult.Success;
    }

    /// <summary>
    /// Creates and returns a fully configured <see cref="ILogProvider"/> instance.
    /// </summary>
    /// <returns>A fully configured <see cref="ILogProvider"/> instance.</returns>
    public ILogProvider GetConfiguredInstance()
    {
      if (ModifierKeys != Keys.Shift)
      {
        // Save the current settings as new default values.
        Settings.Default.PnlLog4NetDirectorySettingsDirectory       = txtLogDirectory.Text;
        Settings.Default.PnlLog4NetDirectorySettingsPattern         = txtLogFilePattern.Text;
        Settings.Default.PnlLog4NetDirectorySettingsReadAllExisting = chkInitialReadAll.Checked;

        Settings.Default.SaveSettings();
      }

      return new Log4NetDirReceiver(
          txtLogDirectory.Text
        , txtLogFilePattern.Text
        , chkInitialReadAll.Checked);
    }

    #endregion

    #region Constructor

    /// <summary>
    /// Creates a new instance of the <see cref="Log4NetDirReceiverSettings"/> <see cref="Control"/>.
    /// </summary>
    public Log4NetDirReceiverSettings()
    {
      InitializeComponent();

      ThemeManager.CurrentApplicationTheme.ApplyTo(mnuFilePattern);
    }

    #endregion
  }
}
