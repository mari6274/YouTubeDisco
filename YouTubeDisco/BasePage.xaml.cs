﻿using Windows.UI.Xaml.Controls;
using YouTubeDisco.Config;

namespace YouTubeDisco
{
    public abstract partial class BasePage : Page
    {
        public Settings Settings { get; set; }

        public virtual void PostInjectionInitialize()
        {
        }
    }
}