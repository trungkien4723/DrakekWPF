   M S B A M L   `   `   `  ����A  =drakek, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:drakekdrakek  5   	   !    drakek.ViewModel.MainWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:drakek     5   	   -   5          � WindowStartupLocation$  CenterScreen=�5   	    � WindowStyle$
 None=�5	   	    � AllowsTransparency$
 True��5
   	   
������  � Title$ Drakek��5   	   $	��450��6   $	��800��6%   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   � $��0��6   
������� � RowDefinitions 5      �� 5      $
��80px �6    �� 5      $��* �6    .+�5      �� -   5        $��0��5       �� Orientation$ 
Horizontal=�6&   $��Center=�6?   .+�5      �� -   5        $��ProfileImage��5      $��50��6,   $��50��6;   $��10q�6I   $
��Fill=�6U    � ContextMenu 5      �� 5      .@�5      m� -   5        $��View Profile��5   '   6=   m� 5      -   5        $��Settings��5   '   69   m� 5      -   5        $��Logout��5   '   67   5      5       �� 5!      $��Center=�6B   $��Center=�6_   $��16(�6z   $
��Bold"�6�    �� Padding$ 10q�6�   $��CharacterEllipsis=�6�   $	��100��6�   ��6   �� *currentUserFirstName+	 � ToolTip	 6�   �� *currentUser.name+5"      �� 5#      -   5        
 ��Name$
 mainMenu��5#   $   $��1��6   $
��Left=�66   $	��450��6Q   $��10q�6^   $	��Top=�6k   $	��200��6�   
������ .@�5$      �� -   5        $��menuDashboard��5$   3   $��	Dashboard��6   63   �� 5%      -	   5        $��menuProduct��5%   1   $��Product��6   61   �� 5&      -
   5        $��
menuPeople��5&   0   $��People��6   60   �� 5'      -   5        $��menuCustomer��5'   2   $��Customer��6   62   �� 5(      -   5        $��
menuPOrder��5(   /   $��Order��6   6/   �� 5)      -   5        $��menuStorage��5)   1   $��Storage��6   61   �� 5*      -   5        $��
menuCoupon��5*   0   $��Coupon��6   60   �� 5+      -   5        $��menuRole��5+   .   $
��Role��6   6.   �� 5,      -   5        $��	menuStock��5,   /   $��Stock��6   6/   5-      5.      � 5/   
   $��1��6   
������� 50      �� 51      $
��30px �6    �� 52      $��* �6    .+�54      � -   5        $��0��54      
�������.+�55      �� $ 
Horizontal=�6!   $��Right=�6:   $��0, 10, 0, 0q�6V   .+�56      �� -   5        $��Minimize��56   !    � Cursor$
 Hand��6K   $��0, 0, 10, 0q�6Y   �� 57      -   5        $��Close��57   !   $
 Hand��6E   $��0, 0, 10, 0q�6S   58      59      � 5:      $��1��6   .+�5;      �� $
��Auto=�6#   .��5<      �� -   5         ��Name$ renderPages��5<   P   $ 
Horizontal=�6!   $��5, 20, 5, 10q�6:   6P   5=      5>      5?      5@      5A      