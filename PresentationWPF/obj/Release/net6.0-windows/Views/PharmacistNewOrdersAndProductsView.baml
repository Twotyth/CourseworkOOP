   M S B A M L   `   `   `  ���� J  FPresentationWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:PresentationWPF.ViewsPresentationWPF.Views  G(clr-namespace:PresentationWPF.ViewModelsPresentationWPF.ViewModels  >    8PresentationWPF.Views.PharmacistNewOrdersAndProductsView   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     5local#clr-namespace:PresentationWPF.Views     ?
viewModels(clr-namespace:PresentationWPF.ViewModels       � 	Resources     btvc��%   &	        �� .��� $��20q� � RowDefinitions �� $��30���� $��* �.+��� $��Right=� �� Orientation$ 
Horizontal=�.+��� 
������ ���� *BackNavigateCommand+.����   BackArrowIcon#���  �� $��10 0 0 0q�
������ ���� * PersonalCabinetNavigateCommand+.����   ProfileIcon#���  � $��1��$��
30 10 30 0q�.+���   SimpleScrollViewer#���  $
��Auto=�.���� .+��  �� $
��Auto ��� .+���  �� Padding$
 0 10q� �� TextAlignment$ Center=�
�������$��24(�$ ��No orders to this pharmacy��y��� *OrdersEmpty+ �� 	Converter# [  �� $��0��#���  $
��Auto=�.���� ���� *OrdersToAccept+���� .R��� $ 
Horizontal=����� .R��� 
������ ���� *AcceptOrderCommand+.���� $	��5 0q� �� Padding$ 20 10q�  	Secondary#���  $��3q� �� CornerRadius$ 15��._��� $ 
Horizontal=�.+���   DeliveringIcon#���  �� $��	10 0  0 0q�$��18(����� *Id+� $��0 10 0 0q�$��1��.+�&   System.Windows.Controls.DataGrid  ���� *ViewingProductsToRegister+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn	   Header$
	 Name��
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 * 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *DisplayName+4  .System.Windows.Controls.DataGridTemplateColumn     CellTemplate �� .R��� ���� *DecreaseQuantityCommand+.����   MinusCircleIcon#���    $	 Quantity�� �� *
Quantity+   �� .R��� ���� *IncreaseQuantityCommand+.����   PlusCircleIcon#���     �� .R��� $	��0 4q����� *RegisterToPharmacyCommand+.���� $ 10  5q�$ 15��  Primary#���  ._���  	 	OnPrimary#��� 	 $��Register��