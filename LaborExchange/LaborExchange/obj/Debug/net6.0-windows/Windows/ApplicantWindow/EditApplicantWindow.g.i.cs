﻿#pragma checksum "..\..\..\..\..\Windows\ApplicantWindow\EditApplicantWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "63C0A125BA3BBE0604B08148753C4D168D92ACF0"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using LaborExchange.Windows.ApplicantWindow;
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


namespace LaborExchange.Windows.ApplicantWindow {
    
    
    /// <summary>
    /// EditApplicantWindow
    /// </summary>
    public partial class EditApplicantWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 29 "..\..\..\..\..\Windows\ApplicantWindow\EditApplicantWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PassportSeriesTextBox;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\..\Windows\ApplicantWindow\EditApplicantWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SpecialityTextBox;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\..\..\Windows\ApplicantWindow\EditApplicantWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox InstitutionTextBox;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\..\..\..\Windows\ApplicantWindow\EditApplicantWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ApplicantPhoto;
        
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
            System.Uri resourceLocater = new System.Uri("/LaborExchange;component/windows/applicantwindow/editapplicantwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Windows\ApplicantWindow\EditApplicantWindow.xaml"
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
            this.PassportSeriesTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.SpecialityTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            
            #line 55 "..\..\..\..\..\Windows\ApplicantWindow\EditApplicantWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SearchSpecialityButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.InstitutionTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            
            #line 62 "..\..\..\..\..\Windows\ApplicantWindow\EditApplicantWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SearchInstitutionButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 92 "..\..\..\..\..\Windows\ApplicantWindow\EditApplicantWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveApplicantButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ApplicantPhoto = ((System.Windows.Controls.Image)(target));
            return;
            case 8:
            
            #line 99 "..\..\..\..\..\Windows\ApplicantWindow\EditApplicantWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddApplicantPhotoButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

