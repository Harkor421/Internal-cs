﻿#pragma checksum "..\..\CPUUnpark.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "6402BDDDEEA73A84D2DEC8296ABCDB2A0DEA69D0E5A7F425D1248301E252BECD"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Micropt;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace Micropt {
    
    
    /// <summary>
    /// CPUUnpark
    /// </summary>
    public partial class CPUUnpark : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\CPUUnpark.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lstRegData;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\CPUUnpark.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCpuStatus;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\CPUUnpark.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnParkAll;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\CPUUnpark.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnUnparkAll;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\CPUUnpark.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblStatusText;
        
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
            System.Uri resourceLocater = new System.Uri("/Micropt;component/cpuunpark.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\CPUUnpark.xaml"
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
            this.lstRegData = ((System.Windows.Controls.ListView)(target));
            return;
            case 2:
            this.btnCpuStatus = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\CPUUnpark.xaml"
            this.btnCpuStatus.Click += new System.Windows.RoutedEventHandler(this.btnCpuStatus_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnParkAll = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\CPUUnpark.xaml"
            this.btnParkAll.Click += new System.Windows.RoutedEventHandler(this.btnParkAll_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnUnparkAll = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\CPUUnpark.xaml"
            this.btnUnparkAll.Click += new System.Windows.RoutedEventHandler(this.btnUnparkAll_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.lblStatusText = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

