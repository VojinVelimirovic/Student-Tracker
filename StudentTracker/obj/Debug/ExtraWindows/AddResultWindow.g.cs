﻿#pragma checksum "..\..\..\ExtraWindows\AddResultWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "95859776DB78DC2586F2ABB18E8051F8D918BD26211D5589EDB4984566E99F87"
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
    /// AddResultWindow
    /// </summary>
    public partial class AddResultWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 36 "..\..\..\ExtraWindows\AddResultWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox SemComboBox;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\ExtraWindows\AddResultWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox SubComboBox;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\..\ExtraWindows\AddResultWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameBox;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\..\ExtraWindows\AddResultWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ScoredBox;
        
        #line default
        #line hidden
        
        
        #line 116 "..\..\..\ExtraWindows\AddResultWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TotalBox;
        
        #line default
        #line hidden
        
        
        #line 128 "..\..\..\ExtraWindows\AddResultWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker datePicker;
        
        #line default
        #line hidden
        
        
        #line 140 "..\..\..\ExtraWindows\AddResultWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddResultButton;
        
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
            System.Uri resourceLocater = new System.Uri("/StudentTracker;component/extrawindows/addresultwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ExtraWindows\AddResultWindow.xaml"
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
            this.SemComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 43 "..\..\..\ExtraWindows\AddResultWindow.xaml"
            this.SemComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.SemComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.SubComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 61 "..\..\..\ExtraWindows\AddResultWindow.xaml"
            this.SubComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.SubComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.NameBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 103 "..\..\..\ExtraWindows\AddResultWindow.xaml"
            this.NameBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.NameBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ScoredBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 113 "..\..\..\ExtraWindows\AddResultWindow.xaml"
            this.ScoredBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.ScoredBox_PreviewTextInput);
            
            #line default
            #line hidden
            
            #line 114 "..\..\..\ExtraWindows\AddResultWindow.xaml"
            this.ScoredBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.ScoredBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.TotalBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 124 "..\..\..\ExtraWindows\AddResultWindow.xaml"
            this.TotalBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TotalBox_PreviewTextInput);
            
            #line default
            #line hidden
            
            #line 125 "..\..\..\ExtraWindows\AddResultWindow.xaml"
            this.TotalBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TotalBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.datePicker = ((System.Windows.Controls.DatePicker)(target));
            
            #line 138 "..\..\..\ExtraWindows\AddResultWindow.xaml"
            this.datePicker.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.datePicker_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.AddResultButton = ((System.Windows.Controls.Button)(target));
            
            #line 145 "..\..\..\ExtraWindows\AddResultWindow.xaml"
            this.AddResultButton.Click += new System.Windows.RoutedEventHandler(this.AddResultButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

