   M S B A M L   `   `   `  ����J  FPresentationWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:PresentationWPF.ViewsPresentationWPF.Views  5      G(clr-namespace:PresentationWPF.ViewModelsPresentationWPF.ViewModels  5      -    'PresentationWPF.Views.NewPharmacistView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:PresentationWPF.Views     5      ?
viewModels(clr-namespace:PresentationWPF.ViewModels     5        � 	Resources  5
         Btvc��%   &	        �� 5      6#   5      .��5      � $��20q�6	    � RowDefinitions 5      �� 5      $��30��6   �� 5      �� 5      $
��Auto �6   .+�5      ��   
Background#���  5      $��Right=�5      y�5      �� *	IsEditing+ �� 	Converter# [  ��5      �� *CloseCommand+.��5      ��   DismissIcon#���  6   5      �� 5      $��0��5      $
��Bold"�5       $��24(�5!      $	��Add��5"      $��Center=�5#      $��Center=�5$      y�5      �� *
IsAdding+# [  5$      �� 5'      $��0��5)      $
��Bold"�5*      $��24(�5+      $
��Edit��5,      $��Center=�5-      $��Center=�5.      y�5(      �� *	IsEditing+# [  5.      �� 51      $��Center=�6   $��1��6.   $��	0 20 0  0q�6;   .+�52      ��   TextBoxBorder#���  6   ._�53   
   � .+�54      �� -   5        $��
SalaryHint��54      $��	Salary...��6$     	HintLabel#���  68   �� 55      -   5        $	��180��56      $��Bottom=�57   %   ��6   �� *Salary+58      5;      5<   	   � 5>      $��0 30 0 0q�5@   	   y�5?   	   �� *
IsAdding+# [  .+�5B   
   �� .+�5D      �� #���  6   ._�5E      � .+�5F      �� -   5        $��	LoginHint��5F      $��Login...��6%   #���  68   �� 5G      -   5        ��5H      �� *Login+5I      5L      5M      �� 5O      $��	0 20 0  0q�6   #���  6&   ._�5P      � .+�5Q      �� -   5        $��PasswordHint��5Q      $��Password...��6*   #���  6@   G� 5S      -   5        5T      5W      5X      5\      5^   	   5`      �� 5b      $��2��6   .+�5c      �� $
��Auto=�6     SimpleScrollViewer#���  6:   .��5d   
   �� $��Center=�6    �� Orientation$ 
Horizontal=�62   .+�5e      �� 
���  ��$��Center=�6E   ��6'   �� *ErrorMessage+6E   5f      5g   	   � 5i      $��0 5 0 0q�6   .+�5j   
   �� $��Center=�5m      y�5k      �� *
IsAdding+# [  ��5l      �� *
AddCommand+.��5n      ��   Primary#���  5o       �� CornerRadius$ 15��5p       �� Padding$
 20 5q�6   ._�5q      �� $ 
Horizontal=�6   .+�5r      �� $��Add New��6   $��20(�6)     	OnPrimary#���  67   �� 5s      $��10 0 0 0q�6     ConfirmIconInversed#���  6(   5t      5u      5v      �� 5y   
   $��Center=�5|      y�5z      �� *	IsEditing+# [  ��5{      �� *EditCommand+.��5}      �� #���  5~      $ 15��5      $
 20 5q�6   ._�5�      �� $ 
Horizontal=�6   .+�5�      �� $
��Save��6   $��20(�6&   #���  64   �� 5�      $��10 0 0 0q�6   #���  6(   5�      5�      5�      5�   	   5�      5�      5�      