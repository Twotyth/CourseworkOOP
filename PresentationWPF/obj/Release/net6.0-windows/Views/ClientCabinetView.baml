   M S B A M L   `   `   `  ���� J  FPresentationWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:PresentationWPF.ViewsPresentationWPF.Views  O,clr-namespace:PresentationWPF.CustomControlsPresentationWPF.CustomControls  G(clr-namespace:PresentationWPF.ViewModelsPresentationWPF.ViewModels  -    'PresentationWPF.Views.ClientCabinetView   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     5local#clr-namespace:PresentationWPF.Views     ?custom,clr-namespace:PresentationWPF.CustomControls     ?
viewModels(clr-namespace:PresentationWPF.ViewModels     .��� .+�� .+�+   %PresentationWPF.Views.BaseViewControl  �   � RowDefinitions  �� $��50 ��� $��* �.+��� $��Right=� �� Orientation$ 
Horizontal=�$��	0 20 20 0q�.+��� $��30��$��0 0 10 0q����� *ExitCommand+.���� $��30��   
LogoutIcon#���   �� ���� *CloseCommand+.���� $��30��  BackArrowIcon#���  �� $��1��  SimpleScrollViewer#���  $
��Auto=�.���� .+��  � ColumnDefinitions �� ��   �� �� .+��� $��50 20q�$��77�� �� CornerRadius$ 15�����#D9D9D9._�� .+��� $��0 -35 0 -30q�$	��Top=�$��$��$��93(�$��SemiBold"�  
Background#���  6   0PresentationWPF.CustomControls.OutlinedTextBlock  $��Right=�$��Center=�   FontSize$ 39(�   
FontWeight$ Medium"�  Primary   Fill# �     Stroke# �     StrokeThickness$ 6Q�$��0 0 20 0q�	   Text	 �� *	Balance+
 �� StringFormat$
 {0:C}���� $��1��$��0��$��0 3 0 0q�$
��50 0q�  	Secondary#���  ._��� $��0 5 0 0q�.+��� $ 
Horizontal=�$��Center=�.+��� $��Center=�$��30��  DeliveringIcon#���  �� $��10 0 0 0q�$��
To deliver��$��Center=�$��24(�$��Italic$�$��Medium"�#���  �� #���  $	��150��$��0 10 0 0q�.���� ���� *OrdersToDeliver+���� .R��� .+��� ���� *ProductNameQuantity+���� .R�� .+��� $��16(�$
��Left=����� *Key+�� $��16(�$��Right=����� *Value+�� $��Right=�#���  $��0 10 0 0q����� *DeliveryPharmacy+�� $��2��$��0��$��1��$��	0 20 50 0q�.+��� #���  $��Italic$�$��Medium"�$��Center=�$��24(�$��
All orders���� $
��Auto=�#���  .���� ���� *	AllOrders+���� .R��� .+��� ���� *ProductNameQuantity+���� .R�� .+��� $��20(�$
��Left=����� *Key+�� $��20(�$��Right=����� *Value+�� #���  $��0 10 0 0q�$��Right=����� *DeliveryPharmacy+�� $��50 20q�#���  $��0 3 0 0q�._�� $��0 20 0 0q� �� $��* ��� $��* �  �� $
��Auto ��� $��* �.+��� $��24(�$��SemiBold"�$��Center=�$��Change username���� $��0��$��1��$��24(�$��SemiBold"�$��Center=�$��Change password��� $��0��$��0��.+��� .+�� $��0 50 0 0q� �� $	��60* ��� $
��220* �.+��� $��60��  ProfileAskIcon#���  �� -    ��Name$ LoginHintLabel��$��1��  	HintLabel#���  $��Your new login...��$��24(�$��15 0 0 3q��� $��1�� 	 TextBoxBorder#��� 	 $��	15 0 20 0q�._��� -   $��0 0 0 0q�$��Bottom=�$��24(�����  �� Path$ NewUsername� �� Mode$ TwoWay=��� $��1��$��0��.+��� #���  $
��Auto=�.���� $��14(�$��Center=�$��0 0 0 5q����� *UsernameMessage+���� *UsernameMessageBrush+�� $��Bottom=�$��Center=����� *ChangeUsernameCommand+.����  �� Padding$
 20 0q�#���  $��40��$ 14��._��� $��Center=�$ 
Horizontal=�.+��� $��Center=�$��Center=�$��24(�$��Medium"� 
 	OnPrimary#��� 
 $��Confirm���� $��10 0 0 0q�$��25��  ConfirmIconInversed#���  � $��0��$��1��.+��� .+�� $��0 50 0 0q� �� $	��60* ��� $
��220* �.+��� $��60��$	��Top=�  LockAskIcon#���  �� $��1��.+�� $��0 25 0 0q�.+��� -   $ PasswordHintLabel��#���  $��Your new password...��$��24(�$��15 0 0 8q��� #��� 	 $��	15 0 20 5q�._�G� -   $��0 0 0 0q�$��Bottom=�$��24(�� $��0 25 0 0q�.+��� -   $ RepeatPasswordHintLabel��#���  $��Repeat it...��$��24(�$��15 0 0 8q��� #��� 	 $��	15 0 20 5q�._�G� -   $��0 0 0 0q�$��Bottom=�$��24(��� $��1��$��1��.+��� #���  $
��Auto=�.���� $��14(�$��Center=�$��0 0 0 5q����� *PasswordMessage+���� *PasswordMessageBrush+�� $��Bottom=�$��Center=����� *ChangePasswordCommand+.���� $
 20 0q�#���  $��40��$ 14��._��� $��Center=�$ 
Horizontal=�.+��� $��Center=�$��Center=�$��24(�$��Medium"�#��� 
 $��Confirm���� $��10 0 0 0q�$��25��#���  