   M S B A M L   `   `   `  ����F  BSolarSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:SolarSystem.Classes.UISolarSystem.Classes.UI  5      -    'SolarSystem.Classes.UI.SpaceObjChildren   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:SolarSystem.Classes.UI     5      -   5        $��./#Montserrat)�5      .��5
      � .+�5   
   �� ���	#B2FFFFFF  �� RadiusX$  5��5       �� RadiusY$ 5��5      � 5   
   $��0, 10q�6    � ColumnDefinitions 5      �� 5      �� 5       � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    .+�5      �� -   5         ��Name$ txtblockName��5      $��0��5      $��2��5      $	��SUN��5      $��24(�5      $
��Bold"�5       $��Center=�5!      $��40, 10q�5"      �� 5$      -   5         ��Name$ listboxOrbitObjects��5$      $��1��5%      $��2��5&       %� 
ScrollUnit$ Pixel=�5'      $��Disabled=�5(      $��Hidden=�5)      $��0, 10q�5*      $	��220��5+      5,      �� 5.      -   5         ��Name$ btnClose��5.      $��1��5/      $��Right=�50      $	��Top=�51      $��3, -7q�52         MaterialDesignToolButton#���   53      
������� �� IsCancel . 	 �� 	IsDefault	 . $��22��57      $��22��58      .��5:      �� $��	Close.png��6   5;      �� 5=      -   5        $ 	btnCreate��5=      $��2��5>      $��Create��5?      $��14(�5@      $��20, 10, 5, 10q�5A      $��Center=�5B      
���   �
�������5E      �� 5G      -   5        $ btnLoad��5G      $��2��5H      $��1��5I      $
��Load��5J      $��14(�5L      $��5, 10, 20, 10q�5M      $��Center=�5N      
���   �
���������5K      �� 
 �� ElementName$
 	btnCreate�� �� Path$ ActualWidth�5Q      5S      5T      5V      