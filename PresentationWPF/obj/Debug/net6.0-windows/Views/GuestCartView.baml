   M S B A M L   `   `   `  ����J  FPresentationWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:PresentationWPF.ViewsPresentationWPF.Views  5      G(clr-namespace:PresentationWPF.ViewModelsPresentationWPF.ViewModels  5      )    #PresentationWPF.Views.GuestCartView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5views#clr-namespace:PresentationWPF.Views     5      ?
viewModels(clr-namespace:PresentationWPF.ViewModels     5      .��5      � .+�5      +   %PresentationWPF.Views.BaseViewControl  � 5      $	�� 20q�6     � ColumnDefinitions  5      �� 5   
   $��2* �6   $	��285��6&   �� 5   
   $��1* �6    � RowDefinitions 5      �� 5   
   $��30 �6   �� 5   
   $��* �6   �� 5   
   $
��Auto �6   .+�5      �� $��2��5   	    �� Orientation$ 
Horizontal=�5   	   $��Right=�5   	   .+�5   
   �� 
������ ��5!      �� *BackCommand+.��5"      �� $��30��6      BackArrowIcon#���   6   5#      �� 5$   
   $��10 0 0 0q�5%      
������ ��5'      �� *ProfileNavigateCommand+.��5(      �� $��30��6     ProfileIcon#���  6   5)      5*   	   �� 5-      $��1��5.   	   $��0��6   $��
30 60 30 0q�6&   $	��0 3q�5/   	     	Secondary#���  50   	   ._�52   
   &   System.Windows.Controls.DataGrid     AutoGenerateColumns$ False��53         HeadersVisibility$ Column=�54      ��55      �� *	CartItems+   Columns 56      4  .System.Windows.Controls.DataGridTemplateColumn  57      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��6%      MinWidth$	 185Q�63      Width5  /System.Windows.Controls.DataGridLengthConverter$ 2* 6B   	   CellTemplate	 58      �� 59      .R�5:      �� $
��Left=�5<      $��14(�5=      $��SemiBold"�5>      ��5;      �� *Name+5>      5@      5B        5E      $
 Auto 6%   	 5F      �� 5G      .R�5H      �� $��Right=�6   ��67   �� *DecreaseQuantityCommand+.��5I      �� $��22��6     MinusCircleIcon#���  6(   5J      5K      5M        5O      $ Count��6%   $
 Auto 64   	 5P      �� 5Q      .R�5R      �� $��Center=�5S      $��14(�5T      $��SemiBold"�5U      ��5V      �� *
Quantity+5X      5Z        5\      $
 Auto 6%   	 5]      �� 5^      .R�5_      �� $
��Left=�6   ��66   �� *IncreaseQuantityCommand+.��5`      �� $��22��6     PlusCircleIcon#���  6(   5a      5b      5d        5f      $ Price��6%   	 5g      �� 5h      .R�5i      �� $��Center=�5j      $��14(�5k      $��SemiBold"�5l      ��5m      �� *Price+
 �� StringFormat$
 {0:C}��5o      5q        5t      $
 Auto 6%   	 5u      �� 5v      .R�5w      �� $
��Left=�6   ��66   �� *RemoveFromCartCommand+.��5x      �� $��16��6     
RemoveIcon#���  6(   5y      5z      5|      5~      5   	   � 5�      $��30 10 30 25q�6   $��2��6"   $��0��6/     5�   
   �� 5�      �� 5�      .+�5�   
   � $��Center=�6     5�      �� 5�      $	��100 �6   �� 5�      .+�5�      �� $��Total:��5�      $��22(�5�      $��Medium"�5�        Primary#���  5�      �� 5�      $��1��5�      $��40 0 0 0q�6   $��22(�5�      $��Italic$�5�      $��Medium"�5�      ��5�      �� *
TotalPrice+$
 {0:C}��5�      �� 5�   
   $��1��5�      $��0 0 0 0q�6   $��Right=�5�      ��5�      �� *CheckoutCommand+.��5�      �� $	��155��6   $��42��6    �� CornerRadius$ 15��6+   #���  6=   ._�5�      �� $ 
Horizontal=�6   $��Center=�62   .+�5�      �� $��Medium"�5�      $��18(�5�        	OnPrimary#���  5�      $��Center=�5�      $��Center=�5�      $��Checkout��5�      �� 5�      $��10 0 0 0q�6   $��25��6(     ConfirmIconInversed#���  64   5�      5�      5�      5�   	   � 5�      $��	0 60 30 0q�6   $��2��6    $��1��61   $��1��6>   .+�5�   
   �� .+�5�      � $��0 50 0 0q�6     5�      �� 5�      $	��60* �6!   �� 5�      $
��220* �6!   .+�5�      �� $��32��6    	 PrescriptionIcon#��� 	 6    �� 5�      -   5         ��Name$ PrescriptionHintLabel��5�      $��1��6    
 	HintLabel#��� 
 5�      $��Prescription ID...��5�      $��18(�5�      $��0 0 0 5q�5�      �� 5�      $��1��5�        TextBoxBorder#���  6   $��0 0 20 0q�5�      ._�5�      �� -   5        $��0 0 0 0q�5�      $��Bottom=�5�      $��24(�5�      ��5�      ��  �� Path$ PrescriptionId� �� Mode$ TwoWay=�5�      5�      5�      � 5�      $��0 30 0 0q�6     5�      �� 5�      $	��60* �6!   �� 5�      $
��220* �6!   .+�5�      �� $��32��6     GeoIcon#���  6    �� 5�      $��1��5�      #���  6   $��0 0 20 0q�5�      ._�5�      �� $��0 0 0 0q�5�      $��Bottom=�5�      $��18(�5�      ��5�      �� *PharmacyAddresses+ �� SelectedItem 5�      �� *SelectedDeliveryPharmacy+��5�      �� 5�      .R�5�      �� ��6    �� *	Address+5�      5�      5�      5�      �� 5�      $��10 20q�6     SimpleScrollViewer#���  6(   $
��Auto=�6U   .��5�      �� $ 
Horizontal=�6   .+�5�      �� ��6   �� *	Message+��63   �� *MessageBrush+5�      5�      5�      �� 5�   
   $��0 0 0 25q�6   $
��Wrap=�6&   $��15(�6;   $��Bottom=�6I   �� 5�      .�6   .,Log in to account to get 1% from your order.5�      �� 5�      �� 6   .�6   Or6     �� 5�      #���  6@    �� Command 6   �� *SignUpNavigateCommand+.�6g   Sign up right now5�      5�      5�   	   5     5     5     