﻿#pragma checksum "..\..\..\..\Classes\UI\SpaceObjChildren.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "06A05C7B7436933E07861320285F8B10958E1042"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using SolarSystem.Classes.UI;
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


namespace SolarSystem.Classes.UI {
    
    
    /// <summary>
    /// SpaceObjChildren
    /// </summary>
    public partial class SpaceObjChildren : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\..\..\Classes\UI\SpaceObjChildren.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtblockName;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\Classes\UI\SpaceObjChildren.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listboxOrbitObjects;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\..\Classes\UI\SpaceObjChildren.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClose;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\Classes\UI\SpaceObjChildren.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCreate;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\..\Classes\UI\SpaceObjChildren.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnLoad;
        
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
            System.Uri resourceLocater = new System.Uri("/SolarSystem;component/classes/ui/spaceobjchildren.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Classes\UI\SpaceObjChildren.xaml"
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
            
            #line 9 "..\..\..\..\Classes\UI\SpaceObjChildren.xaml"
            ((SolarSystem.Classes.UI.SpaceObjChildren)(target)).Loaded += new System.Windows.RoutedEventHandler(this.SpaceObjChildren_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtblockName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.listboxOrbitObjects = ((System.Windows.Controls.ListBox)(target));
            return;
            case 4:
            this.btnClose = ((System.Windows.Controls.Button)(target));
            
            #line 57 "..\..\..\..\Classes\UI\SpaceObjChildren.xaml"
            this.btnClose.Click += new System.Windows.RoutedEventHandler(this.btnClose_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnCreate = ((System.Windows.Controls.Button)(target));
            
            #line 69 "..\..\..\..\Classes\UI\SpaceObjChildren.xaml"
            this.btnCreate.Click += new System.Windows.RoutedEventHandler(this.btnCreate_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnLoad = ((System.Windows.Controls.Button)(target));
            
            #line 81 "..\..\..\..\Classes\UI\SpaceObjChildren.xaml"
            this.btnLoad.Click += new System.Windows.RoutedEventHandler(this.btnLoad_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

