   M S B A M L   `   `   `  ���� J  FPresentationWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:PresentationWPF.ViewsPresentationWPF.Views  G(clr-namespace:PresentationWPF.ViewModelsPresentationWPF.ViewModels  -    'PresentationWPF.Views.NewPharmacistView   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     5local#clr-namespace:PresentationWPF.Views     ?
viewModels(clr-namespace:PresentationWPF.ViewModels       � 	Resources     Btvc��%   &	        �� .��� $��20q� � RowDefinitions �� $��30���� �� $
��Auto �.+���   
Background#���  $��Right=�y��� *	IsEditing+ �� 	Converter# [  ���� *CloseCommand+.����   DismissIcon#���  �� $��0��$
��Bold"�$��24(�$	��Add��$��Center=�$��Center=�y��� *
IsAdding+# [  �� $��0��$
��Bold"�$��24(�$
��Edit��$��Center=�$��Center=�y��� *	IsEditing+# [  �� $��Center=�$��1��$��	0 20 0  0q�.+���   TextBoxBorder#���  ._�� .+��� -   $��
SalaryHint��$��	Salary...��  	HintLabel#���  �� -   $	��180��$��Bottom=����� *Salary+� $��0 30 0 0q�y��� *
IsAdding+# [  .+��� .+��� #���  ._�� .+��� -   $��	LoginHint��$��Login...��#���  �� -   ���� *Login+�� $��	0 20 0  0q�#���  ._�� .+��� -   $��PasswordHint��$��Password...��#���  G� -   �� $��2��.+��� $
��Auto=�  SimpleScrollViewer#���  .���� $��Center=� �� Orientation$ 
Horizontal=�.+��� 
���  ��$��Center=����� *ErrorMessage+� $��0 5 0 0q�.+��� $��Center=�y��� *
IsAdding+# [  ���� *
AddCommand+.����   Primary#���   �� CornerRadius$ 15�� �� Padding$
 20 5q�._��� $ 
Horizontal=�.+��� $��Add New��$��20(�  	OnPrimary#���  �� $��10 0 0 0q�  ConfirmIconInversed#���  �� $��Center=�y��� *	IsEditing+# [  ���� *EditCommand+.���� #���  $ 15��$
 20 5q�._��� $ 
Horizontal=�.+��� $
��Save��$��20(�#���  �� $��10 0 0 0q�#���  