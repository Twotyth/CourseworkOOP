   M S B A M L   `   `   `  ���� J  FPresentationWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:PresentationWPF.ViewsPresentationWPF.Views  U QSystem.Runtime, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a7,clr-namespace:System;assembly=System.RuntimeSystem G(clr-namespace:PresentationWPF.ViewModelsPresentationWPF.ViewModels  &     PresentationWPF.Views.FilterView   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     5local#clr-namespace:PresentationWPF.Views     ?system,clr-namespace:System;assembly=System.Runtime     8vms(clr-namespace:PresentationWPF.ViewModels     $	��400��$	��400��.��� $��20q�  � RowDefinitions  �� $��30 ��� .+��� $��Right=����� *CloseCommand+.����    DismissIcon#���   � $��Center=�$
��Left=�.+��� $
��Left=����� *ResetFiltersCommand+.����  �� CornerRadius$ 10��
���<��._���  �� Padding$ 5q�  	OnPrimary#���  $��22(�$��Clear��$��Center=��� $��0��$��Filters��$��24(�$
��Bold"�$��Center=���   SimpleScrollViewer#���  $��1��$��0 40 0 0q�$
��Auto=�.���� .+�� .+��� .+��� -   ���� *)'ChangeManufacturerListVisibilityCommand+.��� 
������ .+���  �� Orientation$ 
Horizontal=�.+��� $��40��  ManufacturerFilterIcon#���  $
��Left=��� $��10 0 0 0q�$��Manufacturer��$��18(�$
��Left=�$��Center=��� $��30��  
ExpandIcon#���  $��Right=�$��Center=��� y��� *ManufacturerListVisibility+���� *AllManufacturers+���� .R�� ���� .R��� $��5q� �� Padding$ 5q�$��3q�  	Secondary#���  $ 8��$
��Left=�._��� ���� *AddToFilterCommand+.���� $��14(����� *Value+�� #���  $
��Auto=�.���� ���� *SelectedManufacturers+���� .R��� $ 
Horizontal=����� .R��� $��5q�$ 5q�$��3q�#���  $ 8��$
��Left=�._��� ���� *RemoveFromFilterCommand+.���� $��14(�$
��Left=����� *Value+� .+��� .+��� -   ���� *!ChangeTypeListVisibilityCommand+.��� .+��� 
������ $��0 10 0 0q�$ 
Horizontal=�.+���   TypeFilterIcon#���  $��40���� $��10 0 0 0q�$
��Type��$��18(�$
��Left=�$��Center=��� $��30��#���  $��Right=�$��Center=��� y��� *TypeListVisibility+���� *
AllTypes+���� .R�� ���� .R��� $��5q�$ 5q�$��3q�#���  $ 8��$
��Left=�._��� ���� *AddToFilterCommand+.���� $��14(����� *Value+�� #���  $
��Auto=�.���� ���� *SelectedTypes+���� .R��� $ 
Horizontal=����� .R��� $��5q�$ 5q�$��3q�#���  $ 8��$
��Left=�._��� ���� *RemoveFromFilterCommand+.���� $��14(�$
��Left=����� *Value+� .+��� .+��� -   ���� *%#ChangeCategoryListVisibilityCommand+.��� 
������ .+��� $��0 10 0 0q�$ 
Horizontal=�.+���   CategoryFilterIcon#���  $��40���� $��10 0 0 0q�$��Category��$��18(�$
��Left=�$��Center=��� $��30��#���  $��Right=�$��Center=��� y��� *CategoryListVisibility+���� *AllCategories+���� .R�� ���� .R��� $��5q�$ 5q�$��3q�#���  $ 8��$
��Left=�._��� ���� *AddToFilterCommand+.���� $��14(����� *Value+�� #���  $
��Auto=�.���� ���� *SelectedCategories+���� .R��� $ 
Horizontal=����� .R��� $��5q�$ 5q�$��3q�#���  $ 8��$
��Left=�._��� ���� *RemoveFromFilterCommand+.���� $��14(�$
��Left=����� *Value+