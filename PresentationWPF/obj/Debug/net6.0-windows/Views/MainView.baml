   M S B A M L   `   `   `  ����J  FPresentationWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:PresentationWPF.ViewModelsPresentationWPF.ViewModels  5   	   =#clr-namespace:PresentationWPF.ViewsPresentationWPF.Views  5   	   O,clr-namespace:PresentationWPF.CustomControlsPresentationWPF.CustomControls  5   	   $    PresentationWPF.Views.MainView   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ?
viewModels(clr-namespace:PresentationWPF.ViewModels     5   	   5views#clr-namespace:PresentationWPF.Views     5   	   ?custom,clr-namespace:PresentationWPF.CustomControls     5   	   -   5          �Name$  Window��5   	    � WindowStyle$ SingleBorderWindow=�5
   	   
������ $	��550��5   	   $	��800��6   $	��550��5   	   $	��800��6   .��5      �� -   5        $��WindowBorder��5      $
}�True��5         
Background#���   5      ���#717171$��4q�5       � UseLayoutRounding$
 True��5       � 	Resources 5       �� DataType6   0PresentationWPF.ViewModels.GuestCatalogViewModel,   &PresentationWPF.Views.GuestCatalogView3   -PresentationWPF.ViewModels.GuestCartViewModel)   #PresentationWPF.Views.GuestCartView/   )PresentationWPF.ViewModels.LoginViewModel%   PresentationWPF.Views.LoginView0   *PresentationWPF.ViewModels.SignUpViewModel&    PresentationWPF.Views.SignUpView7	   1PresentationWPF.ViewModels.ClientCatalogViewModel-
   'PresentationWPF.Views.ClientCatalogView0   *PresentationWPF.ViewModels.FilterViewModel&    PresentationWPF.Views.FilterView9   3PresentationWPF.ViewModels.PersonalCabinetViewModel/   )PresentationWPF.Views.PersonalCabinetView7   1PresentationWPF.ViewModels.ClientCabinetViewModel-   'PresentationWPF.Views.ClientCabinetView4   .PresentationWPF.ViewModels.ClientCartViewModel*   $PresentationWPF.Views.ClientCartView/   )PresentationWPF.ViewModels.AdminViewModel%   PresentationWPF.Views.AdminView8   2PresentationWPF.ViewModels.AdminNewEntityViewModel)   #PresentationWPF.Views.NewEntityView6   0PresentationWPF.ViewModels.AdminProductViewModel*   $PresentationWPF.Views.NewProductView7   1PresentationWPF.ViewModels.AdminPharmacyViewModel+   %PresentationWPF.Views.NewPharmacyView9   3PresentationWPF.ViewModels.AdminPharmacistViewModel-   'PresentationWPF.Views.NewPharmacistView<   6PresentationWPF.ViewModels.PharmacistCheckoutViewModel2   ,PresentationWPF.Views.PharmacistCheckoutViewH   BPresentationWPF.ViewModels.PharmacistNewOrdersAndProductsViewModel>    8PresentationWPF.Views.PharmacistNewOrdersAndProductsView��%�  (��       5      *, +)(�� 2     5      *, +)(�� d     5#      *, +)(�� �     5'      *, +)(�� �     5+      *,	 +)(�� �     5/      *, +)(�� ,    54      *, +)(�� ^    58      *, +)(�� �    5<      *, +)(�� �    5@      *, +)(�� �    5D      *, +)(�� &    5H      *, +)(�� X    5L      *, +)(�� �    5P      *, +)(�� �    5T      *, +)(�� �    5X      *, +)�� 5      "  6   .R�5   
     5   	   �� 5      "  6   .R�5    
     5!   	   �� 5#      "  6   .R�5$   
     5%   	   �� 5'      "  6   .R�5(   
     5)   	   �� 5+      " 	 6   .R�5,   
   
  5-   	   �� 5/      "  6   .R�50   
     51   	   �� 54      "  6   .R�55   
     56   	   �� 58      "  6   .R�59   
     5:   	   �� 5<      "  6   .R�5=   
     5>   	   �� 5@      "  6   .R�5A   
     5B   	   �� 5D      "  6   .R�5E   
     5F   	   �� 5H      "  6   .R�5I   
     5J   	   �� 5L      "  6   .R�5M   
     5N   	   �� 5P      "  6   .R�5Q   
     5R   	   �� 5T      "  6   .R�5U   
     5V   	   �� 5X      "  6   .R�5Y   
      5Z   	   5i      ._�5k      �  � RowDefinitions 5l      .+�5y      *!   $PresentationWPF.CustomControls.Modal!  $��2��5z   	   $��0��5{   	    K� ZIndex$ 2��6   $��Center=�5|   	   $��Center=�5}   	   $��3q�5   	   ���#717171 !  IsOpen 5~   	   �� *IsPopupOpen+��5�   	   �� *CurrentPopupViewModel+� 5�      .+�5�   
   !  #���   5�      $ 6��5�       5�      �� *IsModalOpen+��5�      �� *CurrentModalViewModel+�� 5�   
   $��1��6    J� IsHitTestVisible 6&   �� *IsHitTestVisible+��6T   �� *CurrentViewModel+	 J� Effect	 5�      -"  'System.Windows.Media.Effects.BlurEffect"  5�      
 "  
KernelType$
 Gaussian=�67    "  Radius 6   �� *
BlurRadius+67   5�      5�   	   5�      5�      5�      