   M S B A M L   `   `   `  ���� J  FPresentationWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:PresentationWPF.ViewModelsPresentationWPF.ViewModels  2    ,PresentationWPF.Views.PharmacistCheckoutView   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ?
viewModels(clr-namespace:PresentationWPF.ViewModels     .��� $��20q�  � RowDefinitions  �� $��30���� .+��� $��Right=� �� Orientation$ 
Horizontal=�.+��� 
������ ���� *" OrdersAndProductsNavigateCommand+.����    DeliveringIcon#���   �� $��10 0 0 0q�
������ ���� * PersonalCabinetNavigateCommand+.����   ProfileIcon#���  � $��30 10 30 0 q�$��1�� � ColumnDefinitions �� $��1* ��� $��2* �.+��   �� $
��Auto ��� .+�� $��30�� �� $��5* ��� $��2* �.+��� -    ��Name$ 
SearchHint��$��0��$��Search here...��  	HintLabel#���  �� $��0��  TextBoxBorder#���  ._��� -   ���� *TitleSearched+ �� Mode$ OneWayToSource=��� $��1������ *SearchCommand+.����   MagnifyingGlassIcon#���  �� $��0 10 0 0q�$��1��  	Secondary#���  $��0 0 0 3q�._���   SimpleScrollViewer#���  $
��Auto=�.���� $��14(�$��SemiBold"����� *ViewingStockItems+���� .R��� ���� *AddToCheckCommand+.���� �� ��� *ProductInfo.FullName+$ OneTime=��� .�, �� ��� *
Quantity+�� .�, �� ��� *ProductInfo.Price+ �� StringFormat$ {0:C}��� $��70 0 0 0q�$��1��  �� $��* ��� $
��Auto ��� $
��Auto �.+��� $	��0 3q�#���  ._�&   System.Windows.Controls.DataGrid  $��SemiBold"����� *ViewingCheck+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ *    Header$
 Name��1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 �� *Name+4  .System.Windows.Controls.DataGridTemplateColumn  
   CellTemplate
 �� .R��� ���� *DecreaseQuantityCommand+.����   MinusCircleIcon#���    $ Count��	 �� *
Quantity+  
 �� .R��� ���� *IncreaseQuantityCommand+.����   PlusCircleIcon#���    $ Price��	 �� *
TotalPrice+  
 �� .R��� ���� *RemoveFromCheckCommand+.����  	 
RemoveIcon#��� 	 �� $��1��.+��� #���  $
��Auto=�.���� $ 
Horizontal=�.+��� 
���  ������ *ErrorMessage+� $
��0 15q�$��1��.+��� $��22(��� $��SemiBold"� 
 Primary#��� 
 $�Total:�� �� $��SemiBold"�$��Italic$���� *
TotalPrice+$ {0:C}���� $��42��$��Right=����� *CheckoutCommand+.����  �� Padding$
 15 8q� �� CornerRadius$ 15��#��� 
 ._��� $��Center=�$ 
Horizontal=�.+��� $	��5 0q�$��SemiBold"�  	OnPrimary#���  $��18(�$��Checkout����   ConfirmIconInversed#���  � $��2�� �� �� .+��� $��	0 0  20 0q�#���  ._�� .+��� -   $��ClientHintLabel��$��Client ID...��#���  �� -   $��Bottom=����� *
ClientId+�� #���  $��1��._��� .+��� $��14(�#���  $��Orders���� $ 
Horizontal=�.+��� $	��160�� �� SelectedItem �� *SelectedOrder+���� *	OrdersIds+�� $	��5 0q�
������ ���� *DeselectOrderCommand+.���� $
��None=�  RefreshIcon#���  