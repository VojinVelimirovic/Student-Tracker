﻿#pragma checksum "..\..\..\ExtraWindows\AddSubjectWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "9DD69BFA9FB9458304CF0ACB6A3A525636A7493F34663803472CCE79318E1A71"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using StudentTracker.ExtraWindows;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace StudentTracker.ExtraWindows {
    
    
    /// <summary>
    /// AddSubjectWindow
    /// </summary>
    public partial class AddSubjectWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 33 "..\..\..\ExtraWindows\AddSubjectWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameBoxSubject;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\ExtraWindows\AddSubjectWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox SemComboBox;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\ExtraWindows\AddSubjectWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddSubject;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Student Tracker;component/extrawindows/addsubjectwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ExtraWindows\AddSubjectWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.NameBoxSubject = ((System.Windows.Controls.TextBox)(target));
            
            #line 40 "..\..\..\ExtraWindows\AddSubjectWindow.xaml"
            this.NameBoxSubject.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.NameBoxSubject_TextChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.SemComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 57 "..\..\..\ExtraWindows\AddSubjectWindow.xaml"
            this.SemComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.SemComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.AddSubject = ((System.Windows.Controls.Button)(target));
            
            #line 64 "..\..\..\ExtraWindows\AddSubjectWindow.xaml"
            this.AddSubject.Click += new System.Windows.RoutedEventHandler(this.AddSubject_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

