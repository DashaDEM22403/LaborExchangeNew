﻿#pragma checksum "..\..\..\..\..\Windows\JobVacancyWindow\SearchJobVacancyWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6ED3E04220C22F66D83A91FBF4C520BE12506073"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using LaborExchange.Windows.JobVacancyWindow;
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


namespace LaborExchange.Windows.JobVacancyWindow {
    
    
    /// <summary>
    /// SearchJobVacancyWindow
    /// </summary>
    public partial class SearchJobVacancyWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\..\..\Windows\JobVacancyWindow\SearchJobVacancyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SearchTextBox;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\..\..\Windows\JobVacancyWindow\SearchJobVacancyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SearchJobVacancyButton;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\..\Windows\JobVacancyWindow\SearchJobVacancyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid JobVacancyGridItem;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\..\Windows\JobVacancyWindow\SearchJobVacancyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid JobVacancyGrid;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\..\Windows\JobVacancyWindow\SearchJobVacancyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ChoiseApplicantButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.1.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/LaborExchange;component/windows/jobvacancywindow/searchjobvacancywindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Windows\JobVacancyWindow\SearchJobVacancyWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.1.0")]
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
            this.SearchJobVacancyButton = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\..\..\..\Windows\JobVacancyWindow\SearchJobVacancyWindow.xaml"
            this.SearchJobVacancyButton.Click += new System.Windows.RoutedEventHandler(this.SearchJobVacancyButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.JobVacancyGridItem = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.JobVacancyGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 5:
            this.ChoiseApplicantButton = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\..\..\..\Windows\JobVacancyWindow\SearchJobVacancyWindow.xaml"
            this.ChoiseApplicantButton.Click += new System.Windows.RoutedEventHandler(this.ChoiseJobVacancyButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

