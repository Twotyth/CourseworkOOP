   M S B A M L   `   `   `  ���� J  FPresentationWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:PresentationWPF.ViewsPresentationWPF.Views  G(clr-namespace:PresentationWPF.ViewModelsPresentationWPF.ViewModels  *    $PresentationWPF.Views.NewProductView   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     5local#clr-namespace:PresentationWPF.Views     ?
viewModels(clr-namespace:PresentationWPF.ViewModels       � 	Resources     Btvc��%   &	        �� .��� .+�� $��20q� � RowDefinitions �� $��30���� .+���   
Background#���  $��Right=�y��� *	IsEditing+ �� 	Converter# [  ���� *CloseCommand+.����   DismissIcon#���  �� $��0��$
��Bold"�$��24(�$	��Add��$��Center=�$��Center=�y��� *
IsAdding+# [  �� $��0��$
��Bold"�$��24(�$
��Edit��$��Center=�$��Center=�y��� *	IsEditing+# [  � $��0 40 0 0q�$��1�� �� �� $
��Auto �.+��� $
��Auto=�  SimpleScrollViewer#���  .���� .+��  � ColumnDefinitions �� �� .+��� .+�� $��0 0 10 0q�.+��� -   $��NameHint��$��0 0 0 3q�$��Product name...��  	HintLabel#���  ��   TextBoxBorder#���  ._��� -   $��Bottom=����� *Name+� $��
 0 20 10 0q�.+��� -   $��ManufacturerHint��$��0 0 0 3q�$��Manufacturer...��#���  �� #���  ._��� -   $��Bottom=����� *Manufacturer+� $��	0 20 10 0q�.+��� -   $��	PriceHint��$��0 0  0 3q�#���  $��Price...���� #���  ._��� -   ���� *Price+�� $��Center=�$��1��.+��  �� $
��Auto ��� .+��� $��Center=�$��Center=�$��32��$��32������ *ChangeMedicalOptionCommand+  ��%   &	        �� .��� .+��� 
������  �� CornerRadius$ 8��$��3q�  	Secondary#���  �� $	��5 0q�  ConfirmIcon#���  y��� # [   �� Path$ 	IsMedical��� $��1��$��0 0 0 0q�$��Center=�$
��Wrap=�$��Center=�$��Is a medical product��$��20(�� $��0 20 0 0q�$	��100��.+��� -   $��DescriptionHint��$	��Top=�$��Description...��#���  �� #���  ._��� -    �� TextWrapping$
 Wrap=����� *Description+� $��0 0 0 0q�y��� *	IsMedical+# [   �� ��  �� �� �� �� .+��� $��2��$��40q�  Primary#���  $��3��� $��0 40 0 0q�$��2��$	��180��$��Center=�.+��� -	   $��MedicineHint��$��0 0 0 3q�$��Medicine...��#���  �� #���  ._��� -
   $��0 0 0 0q�$��Bottom=�$��18(����� *	Medicines+ �� SelectedItem �� *
Medicine+���� .R��� $��Bottom=����� *Name+� $��	0 40 10 0q�$��1��$��0��.+��� -   $��
DosageHint��$��	Dosage...��$��0 0 0 3q�#���  �� #���  ._��� -   ���� *Dosage+� $��0 40 0 0q�$��1��$��1��.+��� -   $��DosageFormHint��$��0 0 0 3q�$��Dosage form...��#���  �� $��Bottom=�#���  ._��� -   $��0 0 0 0q�$��Bottom=�$��18(����� *DosageForms+ �� *
DosageForm+� $��	0 40 10 0q�$��2��$��0��.+��� -   $��QuantityHint��$��Quantity...��$��0 0 0 3q�#���  �� #���  ._��� -   ���� *
Quantity+� $��0 40 0 0q�$��2��$��1��.+��� -   $��QuantityFormHint��$��0 0 0 3q�$��Quantity form...��#���  �� $��Bottom=�#���  ._��� -   $��0 0 0 0q�$��Bottom=�$��18(����� *QuantityForms+ �� *QuantityForm+� $��0 40 0 0q�$��3��$��0��.+��� -   $��ConsumptionFormHint��$��0 0 0 3q�$��Consumption form...��#���  �� $��Bottom=�#���  ._��� -   $��0 0 0 0q�$��Bottom=�$��18(����� *ConsumptionForms+ �� *ConsumptionForm+�� $��1��.+��� $
��Auto=�#���  .���� $��Center=� �� Orientation$ 
Horizontal=�.+��� 
���  ��$��Center=����� *ErrorMessage+� $��0 5 0 0q�.+��� $��Center=�y��� *
IsAdding+# [  ���� *
AddCommand+.���� #���  $ 15��	 �� Padding$
	 20 5q�._��� $ 
Horizontal=�.+��� $��Add New��$��20(� 	 	OnPrimary#��� 	 �� $��10 0 0 0q� 
 ConfirmIconInversed#��� 
 �� $��Center=�y��� *	IsEditing+# [  ���� *EditCommand+.���� #���  $ 15��$
	 20 5q�._��� $ 
Horizontal=�.+��� $
��Save��$��20(�#��� 	 �� $��10 0 0 0q�#��� 
 