   M S B A M L   `   `   `  ����J  FPresentationWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:PresentationWPF.ViewsPresentationWPF.Views  5      G(clr-namespace:PresentationWPF.ViewModelsPresentationWPF.ViewModels  5      %    PresentationWPF.Views.AdminView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5views#clr-namespace:PresentationWPF.Views     5      ?
viewModels(clr-namespace:PresentationWPF.ViewModels     5        � 	Resources  5         btvc��%   &	        �� 5      6#   5      .��5      � .+�5      +   %PresentationWPF.Views.BaseViewControl  � 5      $��20q�6    � RowDefinitions 5      �� 5   
   $��30 �6   �� 5   
   .+�5      �� $��Right=�6    �� Orientation$ 
Horizontal=�6/   .+�5   
   �� ��6   �� *NewEntityNavigationCommand+.��5      ��  �� Padding$
 20 0q�6    �� CornerRadius$ 15��6"     Primary#���  64   ._�5      �� $ 
Horizontal=�6   .+�5      �� $��20��6     AddToCartIcon#���  6"   �� 5      $��10 0 0 0q�5      $	��New��6#   $��SemiBold"�6.     	OnPrimary#���  6D   $��16(�5      $��Center=�5      5       5!      5"      �� 5$   
   $��10 0 0 0q�6   
������ ��6<   �� * PersonalCabinetNavigateCommand+.��5%      �� $��30��6     ProfileIcon#���  6   5&      5'   	   �� 5)      $��1��5*   	   $��0 3 0 0q�5+   	     	Secondary#���  5,   	   $��
30 20 30 0q�5-   	   ._�5/   
   �� 
������ $��0q�6.     50       �� 
TargetType �� Property �� Value �� 
TargetType	 ��Name
 �� Triggers P� Property P� Value �� 
TargetName��%�  '��       �� 51      " ��6   .�52      ��  � ��$ 20(�6+   �� 53       � ��
 �����6-   �� 54       � �� 55      �� 56      " ��6$   .R�57      � .+�58      �� $��Border��59      #���  5:      $
��10 0q�5;      ._�5=      �� $	 ContentSite��5>      $��Center=�5?      $��Center=�5@      $��Header��5A      $
��True��5B      $	��0,2q�5C      5E      5F      
 5G      P� 5H       �@��
IsSelected$
 True��66   . �5I      �� $ Border��6!    � ��$ 0 0 0 3q�6P   �� 5J       � ��$ SemiBold"�67   �� 5K      # �  67    � ��5L      5N      5P      5Q      5R      .@�5S      �� $��Pharmacists��6   .��5T      &   System.Windows.Controls.DataGrid  ��5U      �� *PharmacistViewModels+ �� UpdateSourceTrigger$ PropertyChanged=�   Columns 5W      4  .System.Windows.Controls.DataGridTemplateColumn  5X         CellTemplate 5Y      �� 5Z      .R�5[      �� $	��5 0q�6   ��6-   �� *ShowEditCommand+.��5\      ��   EditIcon#���  6    5]      5^      5`      0  *System.Windows.Controls.DataGridTextColumn  5b      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *Id+  5c      $ Salary��6%    65   �� *Salary+  5e       5f      �� 5g      .R�5h      �� 
������ ��5j      �� *DeleteCommand+.��5l      ��   
RemoveIcon#���  6    5m      5n      5p      5r      5s      �� 5u      $��Products��6   .��5v        ��5w      �� *ProductViewModels+ 5x        5y       5z      �� 5{      .R�5|      �� $	��5 0q�6   ��6-   �� *ShowEditCommand+.��5}      �� #���  6    5~      5      5�        5�       5�      �� 5�       � 	Resources 5�        	NotOnSale �� Triggers �� Binding �� Value��%  &	       �� 5�      " ��62    5�      �� 5�       6*   �� *
IsOnSale+ True6G   .a�5�       ��  � y�$ 	Collapsed=�6=   5�      �� 5�       6*   �� *
IsOnSale+	 False6G   .a�5�       ��  � y�$ Visible=�6=   5�      5�      5�      .R�5�      �� $	��5 0q�6   $��22��6-   ��69   �� *ChangeWithdrawCommand+.��5�      � .+�5�      ��  	 WithdrawIcon#��� 	 5�      y�5�      �� *
IsOnSale+ �� 	Converter# [  5�      �� 5�      #��[ 5�       
 ReturnToSaleIcon#��� 
 5�      5�      5�      5�      5�        5�      $ Id��6%    61   �� *Id+  5�      $
 Name��6%    63   �� *
FullName+  5�      $ Manufacturer��6%    6;   �� *Manufacturer+  5�      $ Medicine��6%    67   �� *MedicineInfo.MedicineId+ �� FallbackValue$ Not Medical��  5�       5�      �� 5�      .R�5�      �� 
������ ��5�      �� *DeleteCommand+.��5�      �� #���  6    5�      5�      5�      5�      5�      �� 5�      $��
Pharmacies��6   .��5�        ��6   �� *PharmacyViewModels+$ 	LostFocus=� 5�        5�       5�      �� 5�      .R�5�      �� $	��5 0q�6   ��6-   �� *ShowEditCommand+.��5�      �� #���  6    5�      5�      5�        5�      $ Id��6%    61   �� *Id+  5�      $ Address��6%    66   �� *	Address+  5�      $ 
Pharmacist��6%    69   �� *WorkingPharmacistId+$ Is not serviced��  5�       5�      �� 5�      .R�5�      �� 
������ ��5�      �� *DeleteCommand+.��5�      �� #���  6    5�      5�      5�      5�      5�      �� 5�      $��Clients��6   .��5�        ��6   �� *ClientViewModels+ 5�        5�      $ Id��6%    61   �� *Id+  5�      $ Balance��6%    66   �� *	Balance+5�      5�      �� 5�      $��Orders��6   .��5�        ��6   �� *OrderViewModels+ 5�        5�       5�      �� 5�      .R�5�      �� $	��5 0q�6   ��6-   �� *CancelOrderCommand+.��5�      �� $��Center=�6      CancelOrderIcon#���  6;   5�      5�      5�        5�      $ Id��6%    61   �� *Id+  5�      $ 	Client Id��6%    68   �� *
ClientId+  5�      $ Status��6%    65   �� *Status+  5�      $ Delivery pharmacy ID��6%    6C   �� *DeliveryPharmacyId+5�      5�      5�      5�   	   5�      5�      5�      