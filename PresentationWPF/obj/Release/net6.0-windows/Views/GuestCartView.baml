   M S B A M L   `   `   `  ���� J  FPresentationWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:PresentationWPF.ViewsPresentationWPF.Views  G(clr-namespace:PresentationWPF.ViewModelsPresentationWPF.ViewModels  )    #PresentationWPF.Views.GuestCartView   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     5views#clr-namespace:PresentationWPF.Views     ?
viewModels(clr-namespace:PresentationWPF.ViewModels     .��� .+�+   %PresentationWPF.Views.BaseViewControl  � $	�� 20q�  � ColumnDefinitions  �� $��2* �$	��285���� $��1* � � RowDefinitions �� $��30 ��� $��* ��� $
��Auto �.+��� $��2�� �� Orientation$ 
Horizontal=�$��Right=�.+��� 
������ ���� *BackCommand+.���� $��30��   BackArrowIcon#���   �� $��10 0 0 0q�
������ ���� *ProfileNavigateCommand+.���� $��30��  ProfileIcon#���  �� $��1��$��0��$��
30 60 30 0q�$	��0 3q�  	Secondary#���  ._�&   System.Windows.Controls.DataGrid     AutoGenerateColumns$ False��   HeadersVisibility$ Column=����� *	CartItems+   Columns 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��   MinWidth$	 185Q�   Width5  /System.Windows.Controls.DataGridLengthConverter$ 2* 	   CellTemplate	 �� .R��� $
��Left=�$��14(�$��SemiBold"����� *Name+  $
 Auto 	 �� .R��� $��Right=����� *DecreaseQuantityCommand+.���� $��22��  MinusCircleIcon#���    $ Count��$
 Auto 	 �� .R��� $��Center=�$��14(�$��SemiBold"����� *
Quantity+  $
 Auto 	 �� .R��� $
��Left=����� *IncreaseQuantityCommand+.���� $��22��  PlusCircleIcon#���    $ Price��	 �� .R��� $��Center=�$��14(�$��SemiBold"����� *Price+
 �� StringFormat$
 {0:C}��  $
 Auto 	 �� .R��� $
��Left=����� *RemoveFromCartCommand+.���� $��16��  
RemoveIcon#���  � $��30 10 30 25q�$��2��$��0��  �� �� .+�� $��Center=�  �� $	��100 ��� .+��� $��Total:��$��22(�$��Medium"�  Primary#���  �� $��1��$��40 0 0 0q�$��22(�$��Italic$�$��Medium"����� *
TotalPrice+$
 {0:C}���� $��1��$��0 0 0 0q�$��Right=����� *CheckoutCommand+.���� $	��155��$��42�� �� CornerRadius$ 15��#���  ._��� $ 
Horizontal=�$��Center=�.+��� $��Medium"�$��18(�  	OnPrimary#���  $��Center=�$��Center=�$��Checkout���� $��10 0 0 0q�$��25��  ConfirmIconInversed#���  � $��	0 60 30 0q�$��2��$��1��$��1��.+��� .+�� $��0 50 0 0q�  �� $	��60* ��� $
��220* �.+��� $��32�� 	 PrescriptionIcon#��� 	 �� -    ��Name$ PrescriptionHintLabel��$��1�� 
 	HintLabel#��� 
 $��Prescription ID...��$��18(�$��0 0 0 5q��� $��1��  TextBoxBorder#���  $��0 0 20 0q�._��� -   $��0 0 0 0q�$��Bottom=�$��24(�����  �� Path$ PrescriptionId� �� Mode$ TwoWay=�� $��0 30 0 0q�  �� $	��60* ��� $
��220* �.+��� $��32��  GeoIcon#���  �� $��1��#���  $��0 0 20 0q�._��� $��0 0 0 0q�$��Bottom=�$��18(����� *PharmacyAddresses+ �� SelectedItem �� *SelectedDeliveryPharmacy+���� .R��� ���� *	Address+�� $��10 20q�  SimpleScrollViewer#���  $
��Auto=�.���� $ 
Horizontal=�.+��� ���� *	Message+���� *MessageBrush+�� $��0 0 0 25q�$
��Wrap=�$��15(�$��Bottom=��� .�.,Log in to account to get 1% from your order.�� �� .�Or �� #���   �� Command �� *SignUpNavigateCommand+.�Sign up right now