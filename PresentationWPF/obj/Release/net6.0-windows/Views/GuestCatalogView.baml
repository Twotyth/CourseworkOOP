   M S B A M L   `   `   `  ���� J  FPresentationWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:PresentationWPF.ViewsPresentationWPF.Views  O,clr-namespace:PresentationWPF.CustomControlsPresentationWPF.CustomControls  G(clr-namespace:PresentationWPF.ViewModelsPresentationWPF.ViewModels  ,    &PresentationWPF.Views.GuestCatalogView   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     5views#clr-namespace:PresentationWPF.Views     ?custom,clr-namespace:PresentationWPF.CustomControls     ?
viewModels(clr-namespace:PresentationWPF.ViewModels     .��� .+�+   %PresentationWPF.Views.BaseViewControl  �   � RowDefinitions  �� $��50 ��� $��* � � ColumnDefinitions �� $��5* ��� $��4* ��� $��4* �.+��� $��2�� �� Orientation$ 
Horizontal=�$��Right=�$	��Top=�$��	0 20 20 0q�.+��� 
������ $��0 0 20 0q����� *CartNavigateCommand+.����    CartIcon#���   �� 
������ ���� *LoginOrPersonalCabinetCommand+.����   ProfileIcon#���  � $��0��$��1��$��0 20 0 0q� �� $��5* ��� $��2* ��� $��2* �.+��� -    ��Name$ SearchLabel��$��0��$��Search here...��  	HintLabel#���  �� $��0��  TextBoxBorder#���  ._��� -   ���� *TitleSearched+ �� Mode$ OneWayToSource=��� $��1������ *SearchCommand+.����   MagnifyingGlassIcon#���  �� $��21.43��$��Center=�$��2������ *FilterModalNavigateCommand+.����   
FilterIcon#���  �� $��3��$��0��$��1��  SimpleScrollViewer#���  $��
75 30 75 0q�$
��Auto=�$
��Auto=�.���� ���� *ViewingProducts+���� .R��� $
��0 10q�._�� $	��110�� �� $	��110 ��� $��* ��� $	��110 �.+��� $	��110��$	��110��
�������$
��Left=��� $��1��$
��10 0q�$��Stretch=�.+��� $��20(�$
��Bold"����� *Name+$ OneWay=��� $��90��$��Hidden=�.���� $
��Wrap=����� *Description+�� $��2��$��Center=�$��50��$��Stretch=����� *AddToCartCommand+ � 	Resources   Btvc��%   &	       �� .����   Primary#���   �� CornerRadius$ 10��._�� $��Stretch=� �� $��4* ��� $��3* �.+��� $��20(�$
��Bold"�
�������$��Center=�$��Center=����� *Price+ �� StringFormat$ {0:C}���� $��1��$��26.73��$��26.73�� 	 AddToCartIcon#��� 	 $��Center=�$��Center=�