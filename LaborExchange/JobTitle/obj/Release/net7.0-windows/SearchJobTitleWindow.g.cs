﻿#pragma checksum "..\..\..\SearchJobTitleWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2FF4B2AE4EFD4E6ADB82613A03FC4BF9F6E68C2C"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using LaborExchange.Windows.JobTitleWindow;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace LaborExchange.Windows.JobTitleWindow {
    
    
    /// <summary>
    /// SearchJobTitleWindow
    /// </summary>
    public partial class SearchJobTitleWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\SearchJobTitleWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SearchTextBox;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\SearchJobTitleWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SearchJobTitleButton;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\SearchJobTitleWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid JobTitleGridItem;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\SearchJobTitleWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid JobTitleGrid;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\SearchJobTitleWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ChoiseJobTitleButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.10.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/JobTitle;component/searchjobtitlewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\SearchJobTitleWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.10.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.SearchTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.SearchJobTitleButton = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\..\SearchJobTitleWindow.xaml"
            this.SearchJobTitleButton.Click += new System.Windows.RoutedEventHandler(this.SearchJobTitleButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.JobTitleGridItem = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.JobTitleGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 5:
            this.ChoiseJobTitleButton = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\..\SearchJobTitleWindow.xaml"
            this.ChoiseJobTitleButton.Click += new System.Windows.RoutedEventHandler(this.ChoiseJobTitleButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

