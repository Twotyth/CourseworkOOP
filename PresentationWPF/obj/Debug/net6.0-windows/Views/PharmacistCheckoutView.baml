   M S B A M L   `   `   `  ����J  FPresentationWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:PresentationWPF.ViewModelsPresentationWPF.ViewModels  5      2    ,PresentationWPF.Views.PharmacistCheckoutView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ?
viewModels(clr-namespace:PresentationWPF.ViewModels     5      .��5	      � $��20q�6	     � RowDefinitions  5
      �� 5      $��30��6   �� 5      .+�5      �� $��Right=�6    �� Orientation$ 
Horizontal=�6-   .+�5      �� 
������ ��5   	   �� *" OrdersAndProductsNavigateCommand+.��5   
   ��    DeliveringIcon#���   6   5   	   �� 5      $��10 0 0 0q�5   	   
������ ��5   	   �� * PersonalCabinetNavigateCommand+.��5   
   ��   ProfileIcon#���  6   5   	   5       � 5#      $��30 10 30 0 q�6   $��1��6     � ColumnDefinitions 5$      �� 5%   
   $��1* �6   �� 5&   
   $��2* �6   .+�5)      �   5*   
   �� 5+      $
��Auto �6   �� 5,      .+�5.   
   � $��30��6    5/      �� 50      $��5* �6   �� 51      $��2* �6   .+�55      �� -   5         ��Name$ 
SearchHint��57      $��0��56      $��Search here...��58        	HintLabel#���  59      5;      �� 5=      $��0��6     TextBoxBorder#���  6#   ._�5>      �� -   5        ��5?      �� *TitleSearched+ �� Mode$ OneWayToSource=�5@      5B      �� 5D      $��1��5E      ��5F      �� *SearchCommand+.��5H      ��   MagnifyingGlassIcon#���  6   5I      5J      �� 5L   
   $��0 10 0 0q�6   $��1��6#     	Secondary#���  60   $��0 0 0 3q�6Z   ._�5M      ��   SimpleScrollViewer#���  5N      $
��Auto=�5O      .��5Q      �� $��14(�6   $��SemiBold"�6)   ��6?   �� *ViewingStockItems+��5R      �� 5S      .R�5T      �� ��6   �� *AddToCheckCommand+.��5U      �� �� 5V      �6   �� *ProductInfo.FullName+$ OneTime=��� 6R   .�6V   ,6Y    �� 5W      �6   �� *
Quantity+�� 68   .�6<   ,6?    �� 5X      �6   �� *ProductInfo.Price+ �� StringFormat$ {0:C}��5Y      5Z      5[      5_      5`      5a      5b   	   � 5d      $��70 0 0 0q�6   $��1��6     5e   
   �� 5f      $��* �6   �� 5g      $
��Auto �6   �� 5h      $
��Auto �6   .+�5j   
   �� $	��0 3q�5k      #���  5l      ._�5n      &   System.Windows.Controls.DataGrid  $��SemiBold"�6   ��6+   �� *ViewingCheck+   Columns 5o      0  *System.Windows.Controls.DataGridTextColumn  5p      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6#      Header$
 Name��6-   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6;   �� *Name+4  .System.Windows.Controls.DataGridTemplateColumn  5r      
   CellTemplate
 5s      �� 5t      .R�5u      �� ��6   �� *DecreaseQuantityCommand+.��5v      ��   MinusCircleIcon#���  6   5w      5x      5z        5|      $ Count��6#   	 62   �� *
Quantity+  5~      
 5      �� 5�      .R�5�      �� ��6   �� *IncreaseQuantityCommand+.��5�      ��   PlusCircleIcon#���  6   5�      5�      5�        5�      $ Price��6#   	 62   �� *
TotalPrice+  5�      
 5�      �� 5�      .R�5�      �� ��6   �� *RemoveFromCheckCommand+.��5�      ��  	 
RemoveIcon#��� 	 6   5�      5�      5�      5�      5�      �� 5�   
   $��1��6   .+�5�      �� #���  6   $
��Auto=�6F   .��5�      �� $ 
Horizontal=�6   .+�5�      �� 
���  ����6+   �� *ErrorMessage+5�      5�      � 5�      $
��0 15q�6   $��1��6   .+�5�      �� $��22(�6   �� 5�      $��SemiBold"�6    
 Primary#��� 
 6*   $�Total:��6Q    �� 5�      $��SemiBold"�6   $��Italic$�6*   �6=   �� *
TotalPrice+$ {0:C}��5�      �� 5�      $��42��69   $��Right=�6E   ��6   �� *CheckoutCommand+.��5�      ��  �� Padding$
 15 8q�6    �� CornerRadius$ 15��6&   #��� 
 68   ._�5�      �� $��Center=�6   $ 
Horizontal=�68   .+�5�      �� $	��5 0q�6   $��SemiBold"�6+     	OnPrimary#���  6A   $��18(�6j   $��Checkout��6x   �� 5�        ConfirmIconInversed#���  6   5�      5�      5�      5�      5�      � 5�   
   $��2��6    5�      �� 5�      �� 5�      .+�5�      �� $��	0 0  20 0q�6   #���  6&   ._�5�      � .+�5�      �� -   5        $��ClientHintLabel��5�      $��Client ID...��6-   #���  6D   �� 5�      -   5        $��Bottom=�5�      ��5�      �� *
ClientId+5�      5�      5�      �� 5�      #���  6   $��1��6;   ._�5�      �� .+�5�      �� $��14(�6   #���  6$   $��Orders��6H   �� 5�      $ 
Horizontal=�6   .+�5�      �� $	��160��5�   :    �� SelectedItem 6   �� *SelectedOrder+��5�      �� *	OrdersIds+�� 5�      $	��5 0q�6B   
������ ��6   �� *DeselectOrderCommand+.��5�      �� $
��None=�6     RefreshIcon#���  6)   5�      5�      5�      5�      5�      5�   	   5�      5�      5�      