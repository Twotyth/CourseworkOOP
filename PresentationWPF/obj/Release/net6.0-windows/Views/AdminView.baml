   M S B A M L   `   `   `  ���� J  FPresentationWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:PresentationWPF.ViewsPresentationWPF.Views  G(clr-namespace:PresentationWPF.ViewModelsPresentationWPF.ViewModels  %    PresentationWPF.Views.AdminView   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     5views#clr-namespace:PresentationWPF.Views     ?
viewModels(clr-namespace:PresentationWPF.ViewModels       � 	Resources     btvc��%   &	        �� .��� .+�+   %PresentationWPF.Views.BaseViewControl  � $��20q� � RowDefinitions �� $��30 ��� .+��� $��Right=� �� Orientation$ 
Horizontal=�.+��� ���� *NewEntityNavigationCommand+.����  �� Padding$
 20 0q� �� CornerRadius$ 15��  Primary#���  ._��� $ 
Horizontal=�.+��� $��20��  AddToCartIcon#���  �� $��10 0 0 0q�$	��New��$��SemiBold"�  	OnPrimary#���  $��16(�$��Center=��� $��10 0 0 0q�
������ ���� * PersonalCabinetNavigateCommand+.���� $��30��  ProfileIcon#���  �� $��1��$��0 3 0 0q�  	Secondary#���  $��
30 20 30 0q�._��� 
������ $��0q�   �� 
TargetType �� Property �� Value �� 
TargetType	 ��Name
 �� Triggers P� Property P� Value �� 
TargetName��%g  '��       �� " ��.���  � ��$ 20(���  � ��
 �������  � �� �� " ��.R�� .+��� $��Border��#���  $
��10 0q�._��� $	 ContentSite��$��Center=�$��Center=�$��Header��$
��True��$	��0,2q�
 P�  �@��
IsSelected$
 True��. ��� $ Border�� � ��$ 0 0 0 3q���  � ��$ SemiBold"��� # �   � ��.@��� $��Pharmacists��.��&   System.Windows.Controls.DataGrid  ���� *PharmacistViewModels+ �� UpdateSourceTrigger$ PropertyChanged=�   Columns 4  .System.Windows.Controls.DataGridTemplateColumn     CellTemplate �� .R��� $	��5 0q����� *ShowEditCommand+.����   EditIcon#���  0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Id��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Id+  $ Salary�� �� *Salary+   �� .R��� 
������ ���� *DeleteCommand+.����   
RemoveIcon#���  �� $��Products��.��  ���� *ProductViewModels+    �� .R��� $	��5 0q����� *ShowEditCommand+.���� #���     ��  � 	Resources   	NotOnSale �� Triggers �� Binding �� Value��%�   &	       �� " �� ��  �� *
IsOnSale+ True.a���  � y�$ 	Collapsed=���  �� *
IsOnSale+	 False.a���  � y�$ Visible=�.R��� $	��5 0q�$��22������ *ChangeWithdrawCommand+.��� .+���  	 WithdrawIcon#��� 	 y��� *
IsOnSale+ �� 	Converter# [  �� #��[  
 ReturnToSaleIcon#��� 
   $ Id�� �� *Id+  $
 Name�� �� *
FullName+  $ Manufacturer�� �� *Manufacturer+  $ Medicine�� �� *MedicineInfo.MedicineId+ �� FallbackValue$ Not Medical��   �� .R��� 
������ ���� *DeleteCommand+.���� #���  �� $��
Pharmacies��.��  ���� *PharmacyViewModels+$ 	LostFocus=�    �� .R��� $	��5 0q����� *ShowEditCommand+.���� #���    $ Id�� �� *Id+  $ Address�� �� *	Address+  $ 
Pharmacist�� �� *WorkingPharmacistId+$ Is not serviced��   �� .R��� 
������ ���� *DeleteCommand+.���� #���  �� $��Clients��.��  ���� *ClientViewModels+   $ Id�� �� *Id+  $ Balance�� �� *	Balance+�� $��Orders��.��  ���� *OrderViewModels+    �� .R��� $	��5 0q����� *CancelOrderCommand+.���� $��Center=�  CancelOrderIcon#���    $ Id�� �� *Id+  $ 	Client Id�� �� *
ClientId+  $ Status�� �� *Status+  $ Delivery pharmacy ID�� �� *DeliveryPharmacyId+