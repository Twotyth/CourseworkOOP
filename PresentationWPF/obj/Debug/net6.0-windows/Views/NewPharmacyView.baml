   M S B A M L   `   `   `  ����J  FPresentationWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:PresentationWPF.ViewsPresentationWPF.Views  5      G(clr-namespace:PresentationWPF.ViewModelsPresentationWPF.ViewModels  5      +    %PresentationWPF.Views.NewPharmacyView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:PresentationWPF.Views     5      ?
viewModels(clr-namespace:PresentationWPF.ViewModels     5        � 	Resources  5
         Btvc��%   &	        �� 5      6#   5      .��5      � $��20q�6	    � RowDefinitions 5      �� 5      $��30��6   �� 5      �� 5      $
��Auto �6   .+�5      ��   
Background#���  5      $��Right=�5      y�5      �� *	IsEditing+ �� 	Converter# [  ��5      �� *CloseCommand+.��5      ��   DismissIcon#���  6   5      �� 5      $��0��5      $
��Bold"�5      $��24(�5       $	��Add��5!      $��Center=�5"      $��Center=�5#      y�5      �� *
IsAdding+# [  5#      �� 5&      $��0��5(      $
��Bold"�5)      $��24(�5*      $
��Edit��5+      $��Center=�5,      $��Center=�5-      y�5'      �� *	IsEditing+# [  5-      �� 50      $��Center=�6   $��1��6.   $��	0 20 0  0q�6;   .+�51      ��   TextBoxBorder#���  6   ._�52   
   � .+�53      �� -   5        $��AddressHint��53      $��
Address...��6%     	HintLabel#���  6:   �� 54      -   5        $	��180��55      $��Bottom=�56   &   ��6   �� *	Address+57      5:      5;   	   �� 5=      $��0 30 0 0q�6   #���  6!   ._�5>   	   � .+�5?   
   �� -   5        $��PharmacistHint��5?      $��Pharmacist...��6&   #���  6>   �� 5@   
   -   5        ��5A      �� *PharmacistsIds+ �� SelectedItem 5B      �� *WorkingPharmacistId+5C      5E   
   5F   	   5G      �� 5I      $��2��6   .+�5J      �� $
��Auto=�6     SimpleScrollViewer#���  6:   .��5K   
   �� $��Center=�6    �� Orientation$ 
Horizontal=�62   .+�5L      �� 
���  ��$��Center=�6E   ��6'   �� *ErrorMessage+6E   5M      5N   	   � 5P      $��0 5 0 0q�6   .+�5Q   
   �� $��Center=�5T      y�5R      �� *
IsAdding+# [  ��5S      �� *
AddCommand+.��5U      ��   Primary#���  5V       �� CornerRadius$ 15��5W       �� Padding$
 20 5q�6   ._�5X      �� $ 
Horizontal=�6   .+�5Y      �� $��Add New��6   $��20(�6)     	OnPrimary#���  67   �� 5Z      $��10 0 0 0q�6     ConfirmIconInversed#���  6(   5[      5\      5]      �� 5`   
   $��Center=�5c      y�5a      �� *	IsEditing+# [  ��5b      �� *EditCommand+.��5d      �� #���  5e      $ 15��5f      $
 20 5q�6   ._�5g      �� $ 
Horizontal=�6   .+�5h      �� $
��Save��6   $��20(�6&   #���  64   �� 5i      $��10 0 0 0q�6   #���  6(   5j      5k      5l      5n   	   5o      5q      5r      