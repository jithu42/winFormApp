﻿#pragma checksum "..\..\..\Views\reset_password.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1F480F9EFC1B7F4CC8D05D8B3F337CFD2EE1D110"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DevExpress.Xpf.DXBinding;
using HamburgerMenuApp.Core.Views;
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


namespace HamburgerMenuApp.Core.Views {
    
    
    /// <summary>
    /// reset_password
    /// </summary>
    public partial class reset_password : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\Views\reset_password.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox new_pass;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\Views\reset_password.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label2;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\Views\reset_password.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox curr_pass;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Views\reset_password.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_reset;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Views\reset_password.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label2_Copy2;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Views\reset_password.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_clear;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Views\reset_password.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox con_pass;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Views\reset_password.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label2_Copy;
        
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
            System.Uri resourceLocater = new System.Uri("/HamburgerMenuApp.Core;component/views/reset_password.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\reset_password.xaml"
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
            
            #line 8 "..\..\..\Views\reset_password.xaml"
            ((HamburgerMenuApp.Core.Views.reset_password)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.new_pass = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 3:
            this.label2 = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.curr_pass = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 5:
            this.btn_reset = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\..\Views\reset_password.xaml"
            this.btn_reset.Click += new System.Windows.RoutedEventHandler(this.Btn_reset_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.label2_Copy2 = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.btn_clear = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\..\Views\reset_password.xaml"
            this.btn_clear.Click += new System.Windows.RoutedEventHandler(this.Btn_clear_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.con_pass = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 9:
            this.label2_Copy = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
