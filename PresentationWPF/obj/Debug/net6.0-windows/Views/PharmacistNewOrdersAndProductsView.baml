   M S B A M L   `   `   `  ����J  FPresentationWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:PresentationWPF.ViewsPresentationWPF.Views  5      G(clr-namespace:PresentationWPF.ViewModelsPresentationWPF.ViewModels  5      >    8PresentationWPF.Views.PharmacistNewOrdersAndProductsView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:PresentationWPF.Views     5      ?
viewModels(clr-namespace:PresentationWPF.ViewModels     5        � 	Resources  5
         btvc��%   &	        �� 5      6#   5      .��5      � $��20q�6	    � RowDefinitions 5      �� 5      $��30��6   �� 5      $��* �6   .+�5      �� $��Right=�6    �� Orientation$ 
Horizontal=�6-   .+�5      �� 
������ ��6(   �� *BackNavigateCommand+.��5   
   ��   BackArrowIcon#���  6   5   	   �� 5      $��10 0 0 0q�6   
������ ��6!   �� * PersonalCabinetNavigateCommand+.��5   
   ��   ProfileIcon#���  6   5   	   5      � 5      $��1��6   $��
30 10 30 0q�6   .+�5      ��   SimpleScrollViewer#���  6   $
��Auto=�6B   .��5    
   �� .+�5!      �  5"      �� 5#      $
��Auto �6   �� 5$      .+�5'      ��  �� Padding$
 0 10q�6    �� TextAlignment$ Center=�5(      
�������$��24(�5*      $ ��No orders to this pharmacy��5-      y�5,      �� *OrdersEmpty+ �� 	Converter# [  5-      �� 50      $��0��51      #���  6   $
��Auto=�52      .��54      �� ��6   �� *OrdersToAccept+��55      �� 56      .R�57      �� $ 
Horizontal=�6!   58      ��5;      �� 5<      .R�5=      �� 
������ ��66   �� *AcceptOrderCommand+.��5>      �� $	��5 0q�6    �� Padding$ 20 10q�6,     	Secondary#���  6<   $��3q�6f    �� CornerRadius$ 15��6z   ._�5?      �� $ 
Horizontal=�6%   .+�5@      ��   DeliveringIcon#���  6"   �� 5A      $��	10 0  0 0q�5B      $��18(�5C      ��6+   �� *Id+5E      5F      5G      5H      5L      5M      � 5P      $��0 10 0 0q�6   $��1��6%   .+�5Q      &   System.Windows.Controls.DataGrid  ��6   �� *ViewingProductsToRegister+   Columns 5R      0  *System.Windows.Controls.DataGridTextColumn  5S      ,  &System.Windows.Controls.DataGridColumn	   Header$
	 Name��6'   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 * 65   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6?   �� *DisplayName+4  .System.Windows.Controls.DataGridTemplateColumn  5U         CellTemplate 5V      �� 5W      .R�5X      �� ��6!   �� *DecreaseQuantityCommand+.��5Y      ��   MinusCircleIcon#���  6"   5Z      5[      5]        5_      $	 Quantity��6'    69   �� *
Quantity+  5a       5b      �� 5c      .R�5d      �� ��6!   �� *IncreaseQuantityCommand+.��5e      ��   PlusCircleIcon#���  6"   5f      5g      5i        5k       5l      �� 5m      .R�5n      �� $	��0 4q�6!   ��6.   �� *RegisterToPharmacyCommand+.��5o      �� $ 10  5q�6#   $ 15��63     Primary#���  6E   ._�5p      ��  	 	OnPrimary#��� 	 6(   $��Register��6Q   5q      5r      5s      5u      5x      5y      5{      5|      5}   	   5~      5�      5�      