   M S B A M L   `   `   `  ���� J  FPresentationWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:PresentationWPF.CustomControlsPresentationWPF.CustomControls  W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   Logo  �� 
TargetType �� Property �� Value	 �� X1	 �� Y1	 �� X2	 �� Y2  Primary J� Effect3   -System.Windows.Media.Effects.DropShadowEffect    	Direction	    Opacity
    
BlurRadius �� Triggers P� Property P� Value �� 
TargetType  TextBoxBorder  	Secondary  	HintLabel �� Color �� Opacity*   $PresentationWPF.CustomControls.Modal � 	Resources  Key   BooleanToVisibilityConverter �� 	Converter � UseLayoutRounding J� OpacityMask !� Visual �� ElementName ��Name  
Background �� BasedOn  NormalBrush 	 NormalBorderBrush ��Name �� Triggers 
 DefaultedBorderBrush �� 
TargetName  MouseOverBrush  PressedBrush  PressedBorderBrush  DisabledBackgroundBrush  DisabledBorderBrush  DisabledForegroundBrush  HorizontalSimpleThumbStyle �Name ��Name �� RadiusX �� RadiusY ! SimpleScrollRepeatButtonStyle  SimpleHorizontalScrollBar  � RowDefinitions! ]�Name" ]� Thumb# ]� IncreaseRepeatButton$ ��Name% ]� DecreaseRepeatButton& �� Angle  SimpleVerticalScrollBar  SimpleScrollViewer' � ColumnDefinitions( �� CanContentScroll) ��Name,* �� %ComputedHorizontalScrollBarVisibility+ �� Orientation, �� ViewportWidth- �� ViewportSize. � Minimum/ �� ScrollableWidth0 � Maximum1 � Value2 �� Path3 �� Mode4 �� RelativeSource*5 �� #ComputedVerticalScrollBarVisibility6 �� ViewportHeight7 �� ScrollableHeight  ComboBoxNormalBorderBrush ! ComboBoxNormalBackgroundBrush # ComboBoxDisabledForegroundBrush # ComboBoxDisabledBackgroundBrush  ComboBoxDisabledBorderBrush   ComboBoxToggleButtonTemplate8 �� CornerRadius9 J� RenderTransformOrigin  RoundedArrowIcon: �� 	ClickMode; d� 	IsChecked< J� IsHitTestVisible= �� SelectionBoxItem> �� SelectionBoxItemTemplate? �� 
IsReadOnly@ �� 
IsReadOnlyA � AllowsTransparencyB �� IsDropDownOpenC J� SnapsToDevicePixelsD � ActualWidthE �� MaxDropDownHeight-  'System.Windows.Input.KeyboardNavigationF   DirectionalNavigation&   System.Windows.Controls.DataGrid=  7System.Windows.Controls.Primitives.DataGridColumnHeader*  $System.Windows.Controls.DataGridCell)  #System.Windows.Controls.DataGridRow�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     8d2http://schemas.microsoft.com/expression/blend/2008  Bmc;http://schemas.openxmlformats.org/markup-compatibility/2006  7custom,clr-namespace:PresentationWPF.CustomControls  %  &	        '�� S    '�� �    'G� �    &	 *    &	 �    '  �    7  '�� �    &	 �    &	 
    &	 �
    &	 c    &	 �    &	     &	 +    &	 ;    &	 K    &	 [    &	 k    7  '�� Y    7  7  '  v    '  �    '      �� "  ��.���  � �� �� .R�� .+��� $ 0��$ 0��$	 170��$ 0��$��	35 0 0 0 q�$	��150��$��4��#���  $��Bottom=�$
��Left=��� $��Bottom=�$
��Left=�$��57 0 0 0q�$��KhaiHealthCo.��$��Be Vietnam Pro)�
���   �$��24(�A� $��33��$��33��$��Bottom=�$
��Left=�$��20 0 0 0q�
�������    $	 270Q�$
	 0.25Q�$
 4Q��� $��29��$��8�����#53A616$��Bottom=�$
��Left=�$��
32.5 0 0 2q��� $��8��$��29�����#53A616$��Bottom=�$
��Left=�$��	22 0 0 12q� P�  �@J�IsMouseOver$
 True��. ���  �@�Cursor$
 Hand���� "  ��.���  � �� �� " ��.R��� ��  �@�FocusVisualStyle Z�  P�  �@J�IsMouseOver$
 True��. ���  �@�Cursor$
 Hand���� "  ��.���  � ��
 ���� ��  � ��$ 0q���  � ��$ 20(� P�  �@J�IsMouseOver$
 True��. ���  �@�Cursor$ IBeam���� "  G�.���  � ��
 ���� ��  � ��$ 0q���  � ��$ 20(� P�  �@J�IsMouseOver$
 True��. ���  �@�Cursor$ IBeam���� "  ��.���  � ��$ 0 0 0 3q���  � ��# �  ��  �@��CornerRadius$	 2.5���� "  ��.���  � �� �� $ Black��$	 0.4Q���  � ��
 ���� ��  � ��$ 20(���  � ��$ 0 0 0 0q���  � ��$ 0 0 0 0q���  � ��$ Italic$���  � ��$ UltraExpanded&���  � ��$ Bottom=���  � ��$ Stretch=��� "   .���  � �� �� "   ����  BooleanToVisibilityConverter .R�� #��z��y��� *IsOpen+8   .+��� #��z��#��z��#��z��#��z��$
 True��._��  !�  �� $ border��.+��� $ border��#���  �� #��z���� "  �� Z� .���  � ��# �  ��  � ��# � 	 ��  � �� �� " ��.R�� .+��� $ Border��#��z��#��z��#��z���� $ ContentPresenter��$��Center=�$��Center=�#��z��#��z��#��z�� P�  �@J�IsKeyboardFocused$
 true��. ��� # � 
 $ Border�� � ��P�  �@J�IsMouseOver$
 true��. ��� # �  $ Border�� � ��P�  � ��$
 true��. ��� # �  $ Border�� � ���� # �  $ Border�� � ��P�  � {�$ false��. ��� # �  $ Border�� � ���� # �  $ Border�� � ���� # �   � ���� "  n� Z� .���  � �� �� " n�.R�� $
 Grid��$��0,0,0,0q�.+��� $ 	Rectangle��$��Stretch=�$��Stretch=�$��5��$
��Auto��$ 2��$ 2��#���  $��0�� P�  �@J�	IsFocused$
 True��P�  �@J�IsMouseOver$
 True��P�  � {�$ False��P�  �@n�
IsDragging$
 True���� "  ��.���  � ��
 ���� ��  � ��
 ���� ��  � ��$ false����  � |�$ false����  � �� �� " ��.R�� .+��� 
������ 
������ #��z���� "  ��.���  �@��IsFlicksEnabled$ false����  � ��# � ����  � �� �� " ��.R�� $ GridRoot��#��� r�
������   �� $��18���� $��0.00001* ��� $��18��.+�]� $! 
PART_Track��$��1��$��Vertical=�$
��true��" n� #���  # �� $$ PageUp��#���  $��ScrollBar.PageDownCommand��% �� $$ PageDown��#���  $��ScrollBar.PageUpCommand�� P�  �@��Orientation$ 
Horizontal=�. ��� $ GridRoot�� �@�LayoutTransform �� $	& -90Q��� $ 
PART_Track�� � ��$ Vertical=��� $ PageDown�� � ��$ ScrollBar.PageLeftCommand���� $ PageUp�� � ��$  ScrollBar.PageRightCommand���� "  ��.���  �@��IsFlicksEnabled$ false����  � ��# � ����  � �� �� " ��.R�� $ GridRoot��#��� r�
������   �� $��18���� $��0.00001* ��� $��18��.+�]� $! 
PART_Track��$��1��$��Vertical=�$
��true��" n� #���  # �� $$ PageUp��#���  $��ScrollBar.PageDownCommand��% �� $$ PageDown��#���  $��ScrollBar.PageUpCommand�� P�  �@��Orientation$ 
Horizontal=�. ��� $ GridRoot�� �@�LayoutTransform �� $	& -90Q��� $ 
PART_Track�� � ��$ Vertical=��� $ PageDown�� � ��$ ScrollBar.PageLeftCommand���� $ PageUp�� � ��$  ScrollBar.PageRightCommand���� "  �� Z� .���  � �� �� " ��.R�� 
������ ' �� $��* ��� $
��Auto �  �� $��* ��� $
��Auto �.+��� $��0��$��0��#��z��#��z��#��z��#( z���� $) PART_HorizontalScrollBar��#y�z* $��0��$��1��#���  $+ 
Horizontal=�#- z, $. 0Q�#0 z/ 1 �� $2 HorizontalOffset�$3 OneWay=�4 �� *TemplatedParent+�� $) PART_VerticalScrollBar��#y�z5 $��1��$��0��#���  $+ Vertical=�#- z6 $. 0Q�#0 z7 1 �� $2 VerticalOffset�$3 OneWay=�4 �� *TemplatedParent+�� $ #e3e9ef���� $
 #fff���� $
 #888���� $
 #eee���� $
 #888���� " d�.R�� ' �� �� $��20 �.+��� $��Border��$��2��$8 0��$��0q�
������ �� $��ButtonBorder��$��1��$��
1, 1, 1, 1q�
������ $8 
0, 0, 0, 0��$��
0, 0, 0, 0q�
������ �� $��Arrow��$��1��$9 0.5, 0.5&�$��Center=�#���  $��Center=�#��z�� P�  �@d�	IsChecked$
 TrueY�. ��� $ Arrow�� � z� �� $	& 180Q�P�  � {�$ False��. ��� $ Arrow�� � z� �� $	& 180Q��� 8    � ���� $ Arrow�� � �� �#999�� "  ��.���  �@J�SnapsToDevicePixels$
 True����  �@�OverridesDefaultStyle$
 True����  � ��$
 Auto=���  � ��$
 Auto=���  � ��$
 True����  � ��
 �   ���  �@�FocusVisualStyle Z� ��  � �� �� " ��.R�� .+�d� $��ToggleButton��$��2��$: Press=�$|�False��8��  ; �� $2 IsDropDownOpen�4 �� *TemplatedParent+$3 TwoWay=��� $��ContentSite��$��5, 3, 23, 3q�$< False��$
��Left=�$��Center=�#��z= #��z> #��z���� $��PART_EditableTextBox��$��3, 3, 23, 3q�#@ z? $y�Hidden=�
������ $
��Left=�$��Center=�$
|�True��.������ " ��.R��� $��PART_ContentHost��$|�False��.��� $��Popup��$��Bottom=�$|�False��$
A True��#��zB $��Slide=�.��� $��DropDown��$
C True��#��zD #��zE .+��� $��DropDownBorder��
�������$��
0, 3, 0, 0q�$8 0��$��0q��� $
��Auto=�$��4q�$
C True��#���  .���� $F 	Contained=� P�  �@��HasItems$ False��. ��� $ DropDownBorder�� � ��$ 95��P�  � {�$ False��. ��� 8   � ��P�  �@��
IsGrouping$
 True��. ���  � ��$ False��P�  �@��
IsEditable$
 True��. ���  �@ 	IsTabStop$ False���� $ PART_EditableTextBox�� � y�$ Visible=��� $ ContentSite�� � y�$ Hidden=��� "   .���  �@ AutoGenerateColumns$ False����  �@ HeadersVisibility$ Column=���  � ��$ SemiBold"���  � ��$ 0q���  � ��
 ���� ��  �@ CanUserAddRows$ False����  �@ CanUserDeleteRows$ False����  �@ CanUserResizeColumns$ False����  �@ CanUserReorderColumns$ False����  �@ CanUserResizeRows$ False����  �@ GridLinesVisibility$
 None=���  �@ ColumnHeaderStyle �� "   .���  � ��
 ���� ��  � ��$ 	0 0 15 30q���  � ��$ 21(���  � ��$ SemiBold"���  � ��$ Center=���  � ��# �  �� "   .���  � ��
 ���� ��  � ��
 ���� ��  � ��$ 0q���  � ��$ 14(���  � ��$ Center=� P�  �@ 
IsSelected$
 True��. ���  � ��
 ���� ��  � ��
 ���� ��  � ��$ 0q���  � �� �� 4 �� *Self+$2 
Foreground��� "   .���  � ��
 ���� ��  � ��
 ���� ��  � ��$ 0q� P�  �@ 
IsSelected$
 True��. ���  � ��$ 0q���  � ��
 ���� ��  � ��
 ���� ��  � �� �� 4 �� *Self+$2 
Foreground�