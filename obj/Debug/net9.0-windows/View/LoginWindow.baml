   M S B A M L   `   `   `  ����A  =drakek, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:drakekdrakek  5   	   "    drakek.ViewModel.LoginWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:drakek     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � WindowStyle$
 None=�5	   	    � AllowsTransparency$
 True��5
   	   
������  � Title$ Drakek��5   	   $	��500��6   $	��300��6%   .��5      � 
������ $��20q�6$    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� -   5        $��0��5      
������� �� CornerRadius$ 10, 10, 0, 0��65   ._�5      �� -   5         �� Orientation$ 
Horizontal=�5   1   $��Right=�6J   $��0, 10, 0, 0q�6f   .+�5      �� -   5        $��Minimize��5       � Cursor$
 Hand��6C   $��0, 0, 10, 0q�6Q   �� 5      -   5        $��Close��5      $
 Hand��6=   $��0, 0, 10, 0q�6K   5      5      �� 5   
   $��1��6   
�������$ 0, 0, 10, 10��65    �� Padding$ 20q�6Q   ._�5      �� $��Center=�6   $��Center=�66   .+�5      �� $��DRAKEK��6   $��24(�6*   $
��Bold"�68   $��0,0,0,20q�6J   $��Center=�6\   �� 5      -   5        	 ��Name$	 UsernameInput��5      $	��200��61   $��30��6=   $��0,0,0,10q�6I   � 5      $	��200��6   $��30��6#   $��0,0,0,20q�6/   .+�5      G� -   5        
 G�Name$
 PasswordInput��5   "   $	��200��69   $
��auto��6E   �� 5      -   5        $	 ShowedPasswordInput��5      $	��200��6;   $
��auto��6G   $y�Hidden=�6U   �� 5      -   5         ��Name$ ShowPasswordCheckBox��5      $��Right=�69   $��0,0,-20,-20q�6U   6�   5       �� 5!      -	   5        $ RememberMeCheckBox��5!      $��Remember Me��67   $��0,0,0,20q�6M   �� 5"      -
   5        $��Login��5"      $��80��6)   $��5q�64   6?   �� 5#      -   5         ��Name$ ForgotPassword��5#      $��12(�64   $��Forgot password?��6B   $
 Hand��6Z   $��Center=�6h   6�   5$      5%      �� 5&   
   $��2��6   
�������$��0,10,0,0q�65   $ 10, 10, 10, 10��6G   ._�5'      �� -   5         ��Name$ ForgotPasswordForm��5'   Q   $��Center=�6   $��Center=�66   $	��300��6m   .+�5(      �� $#��Resend password to your email��6   $��16(�6A   $��Center=�6O   �� 5)      -   5        $	 ForgotPasswordEmailInput��5)      $	��200��6<   $��30��6H   $��0,0,0,10q�6T   �� 5*      -   5        $��Resend Password��5*      $	��100��63   $��5q�6?   6J   �� 5+      -   5        $��Cancel��5+      $	��100��6*   $��5q�66   6A   5,      5-      5.      5/      